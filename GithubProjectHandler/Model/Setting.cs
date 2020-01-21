﻿using System.Collections.Generic;

namespace GithubProjectHandler
{
    public class Setting
    {
        public string HomePage { get; set; }
        public string DefaultLanguage { get; set; }      
        public string DownloadFolder { get; set; }
        public string GitWorkFolder { get; set; }
        public bool EnableLog { get; set; } = true;
        public bool EnableDebug { get; set; } = true;
        public bool AlwaysGetlatestVersion { get; set; } = true;

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
    }
}
