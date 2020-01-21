using CefSharp;
using GithubProjectHandler;
using System;
using System.Collections.Generic;
using System.IO;

namespace GithubProjectRunner
{
    public delegate void CefFileDownloadHandler(ProjectInfo projectInfo);

    public class CefDownloadHandler : IDownloadHandler
    {
        public event EventHandler<DownloadItem> OnBeforeDownloadFired;

        public event EventHandler<DownloadItem> OnDownloadUpdatedFired;
        public event CefFileDownloadHandler OnFileDownloaded;

        private IObserver<FeedbackInfo> observer;
        private List<string> downloadPercents = new List<string>();
        private ProjectInfo projectInfo;

        public void OnBeforeDownload(IWebBrowser webBrowser, IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
        {
            var handler = OnBeforeDownloadFired;

            if (handler != null)
            {
                handler(this, downloadItem);
            }

            if (!callback.IsDisposed)
            {
                using (callback)
                {
                    if (this.projectInfo == null && !string.IsNullOrEmpty(downloadItem.Url))
                    {
                        this.projectInfo = new ProjectParser().SimpleParse(downloadItem.Url);
                        this.projectInfo.FileSize = Utility.GetFileSizeByUrl(downloadItem.OriginalUrl);
                        this.projectInfo.FriendlyFileSize = Utility.GetFriendlyFileSize(this.projectInfo.FileSize);
                    }

                    string downloadFolder = SettingManager.GetSetting().DownloadFolder;

                    if (string.IsNullOrEmpty(downloadFolder))
                    {
                        downloadFolder = SettingManager.DefaultDownloadFolder;
                    }

                    if (!Directory.Exists(downloadFolder))
                    {
                        Directory.CreateDirectory(downloadFolder);
                    }

                    string fileName = downloadItem.SuggestedFileName;

                    if (this.projectInfo != null && !fileName.ToLower().Contains(this.projectInfo.Name.ToLower()))
                    {
                        fileName = this.projectInfo.Name + "-" + fileName;
                    }

                    string filePath = Path.Combine(downloadFolder, fileName);

                    callback.Continue(filePath, showDialog: false);
                }
            }
        }

        public void OnDownloadUpdated(IWebBrowser webBrowser, IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
            if (downloadItem.IsValid && downloadItem.IsInProgress && this.projectInfo != null && this.projectInfo.FileSize > 0)
            {
                double totalBytes = this.projectInfo.FileSize;
                double percent = (downloadItem.ReceivedBytes * 1.0 / totalBytes);

                if (percent * 100 - (int)(percent * 100) <= 0.02)
                {
                    string strPercent = $"Download percent:{percent.ToString("p0")}";
                    string msg = $"{strPercent},({Utility.GetFriendlyFileSize(downloadItem.ReceivedBytes)}/{this.projectInfo.FriendlyFileSize})";

                    if (!this.downloadPercents.Contains(strPercent))
                    {
                        this.downloadPercents.Add(strPercent);
                        this.Feedback(this.projectInfo, msg, FeedbackInfoType.Info, false);
                    }

                    if (this.downloadPercents.Count >= 51)
                    {
                        this.downloadPercents.RemoveRange(0, 50);
                    }
                }
            }

            var handler = OnDownloadUpdatedFired;
            if (handler != null)
            {
                handler(this, downloadItem);
            }

            if (downloadItem.IsComplete)
            {
                //this.Feedback(this.projectInfo, "The zip file has been downloaded, you can run it now.", FeedbackInfoType.Info, false);
                if(this.OnFileDownloaded != null)
                {
                    this.OnFileDownloaded(this.projectInfo);
                }
            }
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
