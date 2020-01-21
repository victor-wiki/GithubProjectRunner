using CefSharp;
using System;

namespace GithubProjectRunner
{
    public class CefLifeSpanHandler : ILifeSpanHandler
    {
        public bool DoClose(IWebBrowser browserControl, IBrowser browser)
        {
            if (browser.IsDisposed || browser.IsPopup)
            {
                return false;
            }

            return true;

        }

        public void OnAfterCreated(IWebBrowser browserControl, IBrowser browser)
        {

        }

        public void OnBeforeClose(IWebBrowser browserControl, IBrowser browser)
        {

        }

        public bool OnBeforePopup(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl,
                                  string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures,
                                  IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            var chromiumWebBrowser = (ExtChromiumWebBrowser)browserControl;

            chromiumWebBrowser.Invoke(new Action(() =>
            {
                NewWindowEventArgs e = new NewWindowEventArgs(windowInfo, targetUrl);
                chromiumWebBrowser.OnNewWindow(e);
            }));

            newBrowser = null;
            return true;
        }
    }
}
