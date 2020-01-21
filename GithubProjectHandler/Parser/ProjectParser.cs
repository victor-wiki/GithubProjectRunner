using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GithubProjectHandler
{
    public class ProjectParser
    {
        public ProjectInfo SimpleParse(string url)
        {
            Uri uri = new Uri(url);

            var segments = uri.Segments;
            string projectUrl = $"{uri.AbsoluteUri.Replace(uri.PathAndQuery, "")}{string.Join("", segments.Take(3))}".TrimEnd('/');

            ProjectInfo projectInfo = new ProjectInfo() { Name = this.GetProjectNameFromUrl(uri), Url = projectUrl };

            return projectInfo;
        }

        private string GetProjectNameFromUrl(Uri uri)
        {
            string name = uri.Segments.Skip(2).First().Trim('/');
            return name;
        }

        public ProjectInfo Parse(string url, string html)
        {
            Uri uri = new Uri(url);

            ProjectInfo projectInfo = new ProjectInfo() { Name = this.GetProjectNameFromUrl(uri), Url = url };

            string content = html;

            if (string.IsNullOrEmpty(content))
            {
                using (WebClient wc = new WebClient())
                {
                    content = wc.DownloadString(url);
                }
            }

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(content);

            var gitLink = doc.DocumentNode.Descendants("input").Where(item => item.HasClass("input-sm") && item.Attributes["value"] != null && item.Attributes["value"].Value.EndsWith(".git")).FirstOrDefault();
            var downloadLink = doc.DocumentNode.Descendants("a").Where(item => item.HasClass("get-repo-btn") && item.Attributes["data-ga-click"] != null).FirstOrDefault();
            var languageSpan = doc.DocumentNode.Descendants("span").Where(item => (item.HasClass("lang") || item.HasClass("language-color")) && item.InnerText.Trim() != "").FirstOrDefault();

            if (gitLink != null)
            {
                projectInfo.GitUrl = gitLink.Attributes["value"].Value;
            }

            if (downloadLink != null)
            {
                string href = downloadLink.Attributes["href"]?.Value;
                if (UrlHelper.IsValidUrl(projectInfo.DownloadUrl))
                {
                    projectInfo.DownloadUrl = href;
                }
                else
                {
                    projectInfo.DownloadUrl = $"{uri.AbsoluteUri.Replace(uri.LocalPath,"")}{href}";
                }
            }

            if (languageSpan != null)
            {
                projectInfo.Language = languageSpan.InnerText.Trim();
            }

            return projectInfo;
        }
    }
}
