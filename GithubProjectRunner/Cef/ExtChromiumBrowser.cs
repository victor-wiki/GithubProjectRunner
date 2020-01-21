using CefSharp;
using CefSharp.WinForms;
using System;

namespace GithubProjectRunner
{
    public class ExtChromiumWebBrowser: ChromiumWebBrowser
    {
        public ExtChromiumWebBrowser(): base()
        {
            this.LifeSpanHandler = new CefLifeSpanHandler();
            this.DownloadHandler = new CefDownloadHandler();
        }

        public ExtChromiumWebBrowser(string url) : base(url)
        {
            this.LifeSpanHandler = new CefLifeSpanHandler();
        }

        public event EventHandler<NewWindowEventArgs> StartNewWindow;

        public void OnNewWindow(NewWindowEventArgs e)
        {
            if (StartNewWindow != null)
            {
                StartNewWindow(this, e);
            }
        }
    }

    public class NewWindowEventArgs : EventArgs
    {
        private IWindowInfo _windowInfo;
        public IWindowInfo WindowInfo
        {
            get { return _windowInfo; }
            set { value = _windowInfo; }
        }
        public string url { get; set; }
        public NewWindowEventArgs(IWindowInfo windowInfo, string url)
        {
            _windowInfo = windowInfo;
            this.url = url;
        }
    }
}
