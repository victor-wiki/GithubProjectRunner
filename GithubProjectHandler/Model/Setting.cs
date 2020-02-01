using System.Collections.Generic;

namespace GithubProjectHandler
{
    public class Setting
    {
        public string HomePage { get; set; }        
        public string DownloadFolder { get; set; }
        public string GitWorkFolder { get; set; }
        public string PreferredLanguage { get; set; }
        public bool EnableLog { get; set; } = true;
        public bool EnableDebug { get; set; } = true;
        public bool AlwaysGetlatestVersion { get; set; } = true;
        public bool OpenExplorerBeforeOpeningSolutionOrProject { get; set; }
        public bool OpenSolutionOrProjectBeforeBuildingAndRun { get; set; }

        public List<LanguageSetting> LanguageSettings { get; set; } = new List<LanguageSetting>();
    }

    public class LanguageSetting
    {
        public string Language { get; set; }
        public string AfterAction { get; set; }
        public string BuildToolPath { get; set; }
        public string BuildToolArgs { get; set; }
        public string OpenToolPath { get; set; }
        public string OpenToolArgs { get; set; }
        public bool OpenAsAdmin { get; set; }
        public CustomActionType CustomActionType { get; set; }
        public string CustomActionContent { get; set; }
        public string SolutionFileExtension { get; set; }
        public string ProjectFileExtension { get; set; }
        public string ExecutableFileExtension { get; set; }
    }

    public enum CustomActionType
    {
        None=0,
        File=1,
        Text=2
    }
}
