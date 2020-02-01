using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GithubProjectHandler;

namespace GithubProjectRunner
{
    public partial class frmSetting : Form
    {
        private Setting setting;
        private List<LanguageSetting> languageSettings = new List<LanguageSetting>();

        public frmSetting()
        {
            InitializeComponent();
        }       

        private void frmSetting_Load(object sender, EventArgs e)
        {
            this.setting = SettingManager.GetSetting();           
            this.txtHomepage.Text = setting.HomePage;
            this.txtDownloadFolder.Text = setting.DownloadFolder;
            this.txtGitWorkFolder.Text = setting.GitWorkFolder;
            this.chkAlwaysGetLatestVersion.Checked = setting.AlwaysGetlatestVersion;
            this.chkEnableLog.Checked = setting.EnableLog;
            this.chkEnableDebug.Checked = setting.EnableDebug;
            this.languageSettings = setting.LanguageSettings;
            this.chkOpenExplorerBeforeOpeningProject.Checked = setting.OpenExplorerBeforeOpeningSolutionOrProject;
            this.chkOpenProjectBeforeBuildingAndRun.Checked = setting.OpenSolutionOrProjectBeforeBuildingAndRun;

            if(string.IsNullOrEmpty(setting.DownloadFolder))
            {
                this.txtDownloadFolder.Text = SettingManager.DefaultDownloadFolder;
            }

            Config config = ConfigManager.GetConfig();
            this.cboLanguage.Items.Add("");
            foreach (string language in config.Languages)
            {
                this.cboLanguage.Items.Add(language);
            }
            this.cboLanguage.Text = setting.PreferredLanguage;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string homepage = this.txtHomepage.Text;

            if(!string.IsNullOrEmpty(homepage) && !homepage.Contains(SettingManager.DomainName))
            {
                MessageBox.Show($"The startup page must be one page of {SettingManager.DomainName}.");
                return;
            }

            Setting setting = new Setting();
            setting.HomePage = this.txtHomepage.Text;
            setting.DownloadFolder = this.txtDownloadFolder.Text;
            setting.GitWorkFolder = this.txtGitWorkFolder.Text;
            setting.AlwaysGetlatestVersion = this.chkAlwaysGetLatestVersion.Checked;
            setting.EnableLog = this.chkEnableLog.Checked;
            setting.EnableDebug = this.chkEnableDebug.Checked;           
            setting.LanguageSettings = this.languageSettings;
            setting.PreferredLanguage = this.cboLanguage.Text;
            setting.OpenExplorerBeforeOpeningSolutionOrProject = this.chkOpenExplorerBeforeOpeningProject.Checked;
            setting.OpenSolutionOrProjectBeforeBuildingAndRun = this.chkOpenProjectBeforeBuildingAndRun.Checked;

            SettingManager.SaveSetting(setting);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOpenDownloadFolder_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(this.txtDownloadFolder.Text))
            {
                this.dlgDownloadFileSaveFlolder.SelectedPath = this.txtDownloadFolder.Text;
            }

            DialogResult result = this.dlgDownloadFileSaveFlolder.ShowDialog();
            if(result==DialogResult.OK)
            {
                this.txtDownloadFolder.Text = this.dlgDownloadFileSaveFlolder.SelectedPath;
            }
        }

        private void btnBuildRunTool_Click(object sender, EventArgs e)
        {
            frmLanguageSetting frmLanguageSetting = new frmLanguageSetting();
            frmLanguageSetting.Setting = this.setting;
            DialogResult result = frmLanguageSetting.ShowDialog();
            if(result==DialogResult.OK)
            {
                this.languageSettings = frmLanguageSetting.LanguageSettings;
            }
        }

        private void btnOpenGitWorkFolder_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtGitWorkFolder.Text))
            {
                this.dlgGitProjectWorkFolder.SelectedPath = this.txtGitWorkFolder.Text;
            }

            DialogResult result = this.dlgGitProjectWorkFolder.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.txtGitWorkFolder.Text = this.dlgGitProjectWorkFolder.SelectedPath;
            }
        }
    }
}
