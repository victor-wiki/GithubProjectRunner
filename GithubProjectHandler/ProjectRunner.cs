using ICSharpCode.SharpZipLib.Zip;
using LibGit2Sharp;
using Microsoft.Alm.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace GithubProjectHandler
{
    public class ProjectRunner
    {
        private IObserver<FeedbackInfo> observer;
        private ProjectInfo projectInfo;
        private List<string> downloadPercents = new List<string>();
        public ProjectInfo ProjectInfo
        {
            get { return this.projectInfo; }
            set { this.projectInfo = value; }
        }

        public Setting Setting { get; set; } = new Setting();

        public string Url { get; set; }

        public string Html { get; set; }
        public ProjectRunner(string url)
        {
            this.Url = url;
        }

        public ProjectRunner(string url, string html)
        {
            this.Url = url;
            this.Html = html;
        }

        public bool CheckUrl()
        {
            Uri url = new Uri(this.Url);
            if (url.Segments.Length == 3)
            {
                return true;
            }
            return false;
        }

        public async Task Run(RunOption option)
        {
            if(!this.CheckUrl())
            {
                this.Feedback(null, $"Can't handle for the url: {this.Url}", FeedbackInfoType.Error);
                return;
            }

            if (this.projectInfo == null)
            {
                if (this.Url == null && string.IsNullOrEmpty(this.Html))
                {
                    this.Feedback(this.projectInfo, "Url can't be empty.");
                    return;
                }

                ProjectParser projectParser = new ProjectParser();
                this.projectInfo = projectParser.Parse(this.Url, this.Html);
            }

            if (this.projectInfo != null && string.IsNullOrEmpty(this.projectInfo.Language))
            {
                this.projectInfo.HasError = true;
                this.Feedback(this.projectInfo, "The project's language is null.", FeedbackInfoType.Error);
                return;
            }

            if (string.IsNullOrEmpty(this.projectInfo.DownloadUrl))
            {
                this.Feedback(this.projectInfo, "It can't get project zip file url.");
                return;
            }

            if (!option.GitClone)
            {
                string downloadFolder = this.Setting.DownloadFolder;

                if (!Directory.Exists(downloadFolder))
                {
                    Directory.CreateDirectory(downloadFolder);
                }

                string fileName = Path.GetFileName(new Uri(this.projectInfo.DownloadUrl).LocalPath);

                if (!fileName.ToLower().Contains(this.projectInfo.Name.ToLower()))
                {
                    fileName = this.projectInfo.Name + "-" + fileName;
                }

                this.projectInfo.WorkDirectory = Path.Combine(downloadFolder, Path.GetFileNameWithoutExtension(fileName));

                string filePath = Path.Combine(downloadFolder, fileName);

                this.projectInfo.DownloadedFilePath = filePath;

                bool existed = File.Exists(filePath);
                bool valid = true;
                if (existed)
                {
                    valid = Utility.IsValidZipFile(filePath);
                }

                if (this.Setting.AlwaysGetlatestVersion || !File.Exists(filePath) || !valid)
                {
                    using (WebClient wc = new WebClient())
                    {
                        this.Feedback(this.projectInfo, $"Begin to download zip file \"{this.projectInfo.DownloadUrl}\".");

                        wc.DownloadProgressChanged += Client_DownloadProgressChanged;
                        wc.DownloadFileCompleted += Wc_DownloadFileCompleted;

                        await wc.DownloadFileTaskAsync(this.projectInfo.DownloadUrl, filePath);
                    }
                }
                else
                {
                    this.UnzipFile();
                    await this.Execute();
                }
            }
            else
            {
                string gitWorkFolder = this.Setting.GitWorkFolder;

                if (!Directory.Exists(gitWorkFolder))
                {
                    Directory.CreateDirectory(gitWorkFolder);
                }

                this.projectInfo.WorkDirectory = Path.Combine(gitWorkFolder, this.projectInfo.Name);

                var secrets = new SecretStore("git");
                var auth = new BasicAuthentication(secrets);
                var creds = auth.GetCredentials(new TargetUri("https://github.com"));

                var options = new CloneOptions
                {
                    CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials
                    {
                        Username = creds.Username,
                        Password = creds.Password
                    },
                };

                this.Feedback(this.projectInfo, $"It begins to git clone \"{this.projectInfo.GitUrl}\".");

                Repository.Clone(this.projectInfo.GitUrl, this.projectInfo.WorkDirectory, options);

                this.Feedback(this.projectInfo, $"It has git cloned \"{this.projectInfo.Name}\".");

                await this.Execute();
            }
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            WebClient wc = sender as WebClient;
            long fileSize = 0;
            if (this.projectInfo.FileSize <= 0)
            {
                if (long.TryParse(wc.ResponseHeaders["Content-Length"], out fileSize))
                {
                    this.projectInfo.FileSize = fileSize;
                    this.projectInfo.FriendlyFileSize = Utility.GetFriendlyFileSize(fileSize);
                }
            }

            if (this.projectInfo.FileSize <= 0)
            {
                return;
            }

            double percent = (e.BytesReceived * 1.0 / this.projectInfo.FileSize);

            if (percent * 100 - (int)(percent * 100) <= 0.02)
            {
                string strPercent = $"Download percent:{percent.ToString("p0")}";
                string msg = $"{strPercent},({Utility.GetFriendlyFileSize(e.BytesReceived)}/{this.projectInfo.FriendlyFileSize})";

                if (!this.downloadPercents.Contains(strPercent))
                {
                    this.downloadPercents.Add(strPercent);
                    this.Feedback(this.projectInfo, msg);
                }

                if (this.downloadPercents.Count >= 51)
                {
                    this.downloadPercents.RemoveRange(0, 50);
                }
            }
        }

        private void Wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                return;
            }
            else if (e.Error != null)
            {
                this.HandleError(sender, e);
                return;
            }

            this.Feedback(this.projectInfo, "The zip file has been downloaded.");

            this.UnzipFile();
            this.Execute();
        }

        private string UnzipFile()
        {
            if (File.Exists(this.projectInfo.DownloadedFilePath))
            {
                this.Feedback(this.projectInfo, $"Unzip file \"{this.projectInfo.DownloadedFilePath}\".");
                FileInfo file = new FileInfo(this.projectInfo.DownloadedFilePath);
                string extractFolder = file.DirectoryName;
                (new FastZip()).ExtractZip(this.projectInfo.DownloadedFilePath, extractFolder, "");
                return extractFolder;
            }
            return string.Empty;
        }

        private async Task Execute()
        {
            if (Directory.Exists(this.projectInfo.WorkDirectory))
            {
                LanguageIntepreter intepreter = LanguageInterpreterHelper.GetInterpreter(this.projectInfo);

                if (intepreter != null)
                {
                    this.Feedback(this.projectInfo, $"The project language is {intepreter.Language}, it begins to handle.");

                    intepreter.Setting = this.Setting;

                    intepreter.Subscribe(this.observer);

                    await intepreter.Handle();
                }
                else
                {
                    this.Feedback(this.projectInfo, $"It can't handle the project which language is {this.projectInfo.Language} currently.", FeedbackInfoType.Error);
                    Utility.OpenFolder(this.projectInfo.WorkDirectory);
                }
            }
        }

        private void HandleError(object sender, AsyncCompletedEventArgs e)
        {
            string errMsg = e.Error.Message;
            if (e.Error.InnerException != null)
            {
                errMsg += "," + e.Error.InnerException.Message;
            }
            this.Feedback(this.projectInfo, $"Failed to download file \"{this.projectInfo.DownloadUrl}\":{errMsg}");
        }

        public void Subscribe(IObserver<FeedbackInfo> observer)
        {
            this.observer = observer;
        }

        public void Feedback(ProjectInfo info, string content, FeedbackInfoType infoType = FeedbackInfoType.Info, bool enableLog = true)
        {
            FeedbackHelper.Feedback(this.observer, new FeedbackInfo() { InfoType = infoType, Content = content, Source = info }, LogHelper.DefaultLogFilePath, enableLog);
        }
    }
}
