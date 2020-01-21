using System.Collections.Generic;
using System.IO;

namespace GithubProjectHandler
{
    public class ProjectInfo
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public long FileSize { get; set; }
        public string FriendlyFileSize { get; set; }
        public string Language { get; set; }
        public string DownloadUrl { get; set; }
        public string GitUrl { get; set; }
        public string DownloadedFilePath { get; set; }
        public string WorkDirectory { get; set; }
        public List<FileInfo> ExecutableFiles { get; set; } = new List<FileInfo>();
        public List<WebsiteInfo> Websites { get; set; } = new List<WebsiteInfo>();
        public bool HasError { get; set; }
    }   
}
