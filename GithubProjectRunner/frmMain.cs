using CefSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GithubProjectHandler;
using System.Web;
using CefSharp.WinForms;
using System.IO;

namespace GithubProjectRunner
{
    public partial class frmMain : Form, IObserver<FeedbackInfo>
    {
        private ExtChromiumWebBrowser browser = new ExtChromiumWebBrowser();

        private Config config = ConfigManager.GetConfig();
        private Setting setting = SettingManager.GetSetting();

        private string homePage = "https://github.com/";
        private readonly string searchUrlFormat = "https://github.com/search?l={0}&q={1}&type=Repositories";

        private string BrowserHtml
        {
            get
            {
                try
                {
                    var frame = browser.GetBrowser().MainFrame;
                    if (!string.IsNullOrEmpty(frame.Url))
                    {
                        var visitor = new TaskStringVisitor();
                        frame.GetSource(visitor);
                        return visitor.Task.Result;
                    }

                    return string.Empty;

                }
                catch (Exception ex)
                {
                    return string.Empty;
                }

            }
        }

        public frmMain()
        {
            InitializeComponent();
            TextBox.CheckForIllegalCrossThreadCalls = false;
            RichTextBox.CheckForIllegalCrossThreadCalls = false;           
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.setting.HomePage))
            {
                this.homePage = setting.HomePage;
            }

            this.InitControls();
        }

        private void InitControls()
        {
            this.InitBrowser();

            foreach (string language in config.Languages)
            {
                this.cboLanguage.Items.Add(language);
            }

            this.cboLanguage.Items.Add("");
            if (!string.IsNullOrEmpty(setting.PreferredLanguage))
            {
                this.cboLanguage.Text = setting.PreferredLanguage;
            }
        }

        private void InitBrowser()
        {
            string cacheFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\cef";

            if (!Directory.Exists(cacheFolder))
            {
                Directory.CreateDirectory(cacheFolder);
            }

            CefSettings settings = new CefSettings();
            settings.CachePath = cacheFolder;
            CefSharp.Cef.Initialize(settings);

            this.browser.IsBrowserInitializedChanged += Browser_IsBrowserInitializedChanged;
            this.browser.StartNewWindow += Browser_StartNewWindow;
            this.browser.LoadingStateChanged += Browser_LoadingStateChanged;
            this.browser.AddressChanged += Browser_AddressChanged;

            var downloaderHandler = this.browser.DownloadHandler as CefDownloadHandler;
            downloaderHandler.Subscribe(this);

            downloaderHandler.OnFileDownloaded += DownloaderHandler_OnFileDownloaded;

            this.browser.Dock = DockStyle.Fill;
            this.panelBrowser.Controls.Add(this.browser);
        }

        private void DownloaderHandler_OnFileDownloaded(ProjectInfo projectInfo)
        {
            if (projectInfo != null)
            {
                DialogResult result = MessageBox.Show("The file has been downloaded, would you like to run the project?", "Confirm", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    this.Run(true);
                }
            }
        }

        private void Browser_IsBrowserInitializedChanged(object sender, EventArgs e)
        {
            this.browser.Load(this.homePage);
        }

        private void Browser_StartNewWindow(object sender, NewWindowEventArgs e)
        {
            if (e.url == "about:blank")
            {
                MessageBox.Show("Can't open the page.");
                return;
            }
            this.browser.Load(e.url);
        }

        private void Browser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading == false)
            {
               
            }
        }

        private void Browser_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            this.txtUrl.Text = e.Address;
        }

        private void txtKeyword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Search();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.Search();
        }

        private void Search()
        {
            string keyword = this.txtKeyword.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("The keyword can't be empty.");
                return;
            }

            string url = string.Format(this.searchUrlFormat, this.UrlEncode(this.cboLanguage.Text), this.UrlEncode(keyword));

            this.browser.Load(url);
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSetting frmSetting = new frmSetting();
            DialogResult result = frmSetting.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.setting = SettingManager.GetSetting();
            }
        }

        private string UrlEncode(string value)
        {
            return HttpUtility.UrlEncode(value);
        }

        private void txtUrl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.LoadPage();
            }
        }

        private void LoadPage()
        {
            this.browser.Load(this.txtUrl.Text);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            this.LoadPage();
        }

        private void btnOpenInBrowser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtUrl.Text.Trim()))
            {
                MessageBox.Show("Url can't be empty.");
                return;
            }
            Process.Start(this.txtUrl.Text);
        }

        private async void btnRun_Click(object sender, EventArgs e)
        {
            await this.Execute();
        }

        private async Task Run(bool onlyUseUrl = false, bool gitClone = false)
        {
            if (string.IsNullOrEmpty(this.txtUrl.Text))
            {
                MessageBox.Show("Url can't be empty.");
                return;
            }

            ProjectRunner projectRunner;
            string html = onlyUseUrl ? null : this.BrowserHtml;

            Setting setting = this.setting;

            FeedbackHelper.EnableLog = setting.EnableLog;
            LogHelper.EnableDebug = setting.EnableDebug;

            if (string.IsNullOrEmpty(setting.DownloadFolder))
            {
                setting.DownloadFolder = SettingManager.DefaultDownloadFolder;
            }

            projectRunner = new ProjectRunner(this.txtUrl.Text, html) { Setting = this.setting };

            projectRunner.Subscribe(this);

            RunOption option = new RunOption() { GitClone = gitClone };

            await projectRunner.Run(option);

            ProjectInfo projectInfo = projectRunner.ProjectInfo;
            if (projectInfo != null && projectInfo.ExecutableFiles.Count > 1)
            {
                frmExetuableFiles frmExetuableFiles = new frmExetuableFiles(projectInfo.ExecutableFiles);
                frmExetuableFiles.ShowDialog();
            }
        }

        private async Task Execute(bool gitClone = false)
        {
            try
            {
                this.SetControlState(false);
                await this.Run(false, gitClone);
                this.SetControlState(true);
            }
            catch (Exception ex)
            {
                this.SetControlState(true);
                if (this.setting.EnableLog)
                {
                    LogHelper.LogInfo(LogHelper.DefaultLogFilePath, ex.Message);
                }

                this.Feedback(new FeedbackInfo() { InfoType = FeedbackInfoType.Error, Content = ex.Message });
                MessageBox.Show(ex.Message);
            }
        }

        private void SetControlState(bool enabled)
        {
            this.btnRun.Enabled = enabled;
            this.btnCloneAndRun.Enabled = enabled;
        }

        private async void btnCloneAndRun_Click(object sender, EventArgs e)
        {
            await this.Execute(true);
        }

        #region IObserver<FeedbackInfo>
        void IObserver<FeedbackInfo>.OnCompleted()
        {
        }
        void IObserver<FeedbackInfo>.OnError(Exception error)
        {
        }
        void IObserver<FeedbackInfo>.OnNext(FeedbackInfo info)
        {
            this.Feedback(info);
        }
        #endregion

        private void Feedback(FeedbackInfo info)
        {
            this.Invoke(new Action(() =>
            {
                if (info.InfoType != FeedbackInfoType.Info)
                {
                    this.AppendSpecialMessage(info);
                }
                else
                {
                    this.txtMessage.Text += (this.txtMessage.Text.Length > 0 ? Environment.NewLine : "") + info.Content;
                }

                this.txtMessage.SelectionStart = this.txtMessage.TextLength;
                this.txtMessage.ScrollToCaret();
            }));
        }

        private void AppendSpecialMessage(FeedbackInfo info)
        {
            int start = this.txtMessage.Text.Length;
            this.txtMessage.Text += (this.txtMessage.Text.Length > 0 ? Environment.NewLine : "") + info.Content;

            this.txtMessage.Select(start, info.Content.Length + 1);
            Color color = Color.Black;
            if (info.InfoType == FeedbackInfoType.Warnning)
            {
                color = Color.DarkOrange;
            }
            else if (info.InfoType == FeedbackInfoType.Error)
            {
                color = Color.Red;
            }
            this.txtMessage.SelectionColor = color;
        }
    }
}
