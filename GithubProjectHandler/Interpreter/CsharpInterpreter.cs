using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GithubProjectHandler
{
    public class CsharpInterpreter : LanguageInterpreter
    {
        private Regex errorRegex = new Regex("\\b(error|invalid)\\b");

        public override string Language => "C#";
        protected override string solutionFileExtension => ".sln";
        protected override string projectFileExtension => ".csproj";
        protected override string executableFileExtension => ".exe";

        public override async Task Build(FileInfo file)
        {
            await base.Build(file);

            string filePath = file.FullName;
            LanguageSetting languageSetting = this.LanguageSetting;

            string buildToolPath = languageSetting?.BuildToolPath;

            this.Prepare(file);

            if (!string.IsNullOrEmpty(buildToolPath) && File.Exists(buildToolPath))
            {
                this.Feedback(this.ProjectInfo, $"It uses tool \"{buildToolPath}\" to build.");

                string buildArgs = string.IsNullOrEmpty(languageSetting.BuildToolArgs) ? "-v:normal" : languageSetting.BuildToolArgs;

                string args = $"{buildArgs} {filePath}";

                Action exec = () =>
                  {
                      ProcessHelper.StartFile(buildToolPath, args, this.Msbuild_OutputDataReceived, null);
                  };

                await Task.Factory.StartNew(exec);
            }
            else
            {
                this.ProjectInfo.HasError = true;
                this.Feedback(this.ProjectInfo, $"It hasn't been specified build tool for language {this.Language}", FeedbackInfoType.Error);
            }
        }

        private void Prepare(FileInfo file)
        {
            var console = new NuGet.CommandLine.Console();

            var packageFiles = file.Directory.GetFiles("packages.config", SearchOption.AllDirectories);
            if (packageFiles.Count() > 0)
            {
                this.Feedback(this.ProjectInfo, "Exist \"packages.config\", it uses NuGet.exe to restore the packages.");
                ProcessHelper.StartFile("NuGet.exe", "restore " + file.FullName, this.Nuget_OutputDataReceived, null);
                this.Feedback(this.ProjectInfo, "Packages have been restored.");
            }
        }

        private void Msbuild_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                MatchCollection matches = errorRegex.Matches(e.Data.ToLower());
                if (matches.Count > 0)
                {
                    this.ProjectInfo.HasError = true;
                    this.Feedback(this.ProjectInfo, "Build error:" + e.Data, FeedbackInfoType.Error);
                }
                else
                {
                    if (this.Setting.EnableDebug)
                    {
                        Console.WriteLine(e.Data);
                    }
                }
            }
        }

        private void Nuget_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                if (e.Data.Contains("error"))
                {
                    this.Feedback(this.ProjectInfo, "Nuget error:" + e.Data, FeedbackInfoType.Error);
                }
            }
        }

        private void Logger_OnFeedback(string message)
        {
            this.Feedback(this.ProjectInfo, "Build message:" + message);
        }

        public override async Task Run()
        {
            foreach (var projFile in this.ProjectFiles)
            {
                CsharpProjectFileParser parser = new CsharpProjectFileParser(projFile.FullName);
                CsharpProjectFileInfo info = parser.Parse();

                if (projFile.Directory.GetFiles("web.config").Length > 0)
                {
                    this.ProjectInfo.Websites.Add(new WebsiteInfo() { DirectoryInfo = projFile.Directory, IsWebservice = info.IsServiceProject });
                }
                else if (!string.IsNullOrEmpty(this.ExecutableFileExtension))
                {
                    foreach (string ext in this.ExecutableFileExtension.Split(';'))
                    {
                        if (info.OutputType.ToLower().EndsWith(ext.Trim('.')))
                        {
                            string exeFilePath = Path.Combine(projFile.DirectoryName, info.OutputPath, info.AssemblyName + ext);

                            if (File.Exists(exeFilePath))
                            {
                                this.ProjectInfo.ExecutableFiles.Add(new FileInfo(exeFilePath));
                            }
                        }
                    }
                }
            }

            if (this.ProjectInfo.ExecutableFiles.Count == 1)
            {
                Action exec = () =>
                  {
                      string filePath = this.ProjectInfo.ExecutableFiles.First().FullName;
                      this.Feedback(this.ProjectInfo, $"Execute file \"{filePath}\"");
                      ProcessHelper.StartFileByCmd(filePath, null);
                  };

                await Task.Factory.StartNew(exec);
            }
            else if (this.ProjectInfo.ExecutableFiles.Count > 1)
            {
                this.Feedback(this.ProjectInfo, "There are more than one exe files.");
            }
            else if (this.ProjectInfo.ExecutableFiles.Count == 0)
            {
                if (this.ProjectInfo.Websites.Count == 0)
                {
                    this.Feedback(this.ProjectInfo, "There isn't any exe file.");
                }
                else if (this.ProjectInfo.Websites.Count >= 1)
                {
                    foreach (var website in this.ProjectInfo.Websites.Where(item => item.IsWebservice))
                    {
                        this.RunWebsite(website);
                    }

                    foreach (var website in this.ProjectInfo.Websites.Where(item => !item.IsWebservice))
                    {
                        this.RunWebsite(website);
                    }
                }
            }
        }

        private void RunWebsite(WebsiteInfo website)
        {
            this.Feedback(this.ProjectInfo, $"It begins to start {(website.IsWebservice ? "service site" : "website")} \"{website.DirectoryInfo.Name}\".");
            Utility.RunDonetWebsite(website.DirectoryInfo.Name, (data) =>
            {
                this.Feedback(this.ProjectInfo, data, FeedbackInfoType.Info);
            });
        }
    }
}
