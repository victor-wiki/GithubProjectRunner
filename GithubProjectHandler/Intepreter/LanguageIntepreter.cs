using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubProjectHandler
{
    public abstract class LanguageIntepreter
    {
        private IObserver<FeedbackInfo> observer;
        public ProjectInfo ProjectInfo { get; set; }
        public Setting Setting = new Setting();
        public abstract string Language { get; }
        public abstract string SolutionFileExtension { get; }
        public abstract string ProjectFileExtension { get; }
        public abstract string ExecutableFileExtension { get; }

        public IEnumerable<FileInfo> SolutionFiles { get; protected set; }
        public IEnumerable<FileInfo> ProjectFiles { get; protected set; }

        public LanguageSetting LanguageSetting => this.Setting.LanguageSettings.FirstOrDefault(item => item.Language == this.Language);

        public void Subscribe(IObserver<FeedbackInfo> observer)
        {
            this.observer = observer;
        }

        public virtual async Task Handle()
        {
            if (Directory.Exists(this.ProjectInfo.WorkDirectory))
            {
                string workDirectory = this.ProjectInfo.WorkDirectory;

                DirectoryInfo di = new DirectoryInfo(workDirectory);

                this.SolutionFiles = string.IsNullOrEmpty(this.SolutionFileExtension) ? Enumerable.Empty<FileInfo>() : di.GetFiles("*" + this.SolutionFileExtension, SearchOption.AllDirectories);
                int slnFileCount = this.SolutionFiles.Count();
                this.ProjectFiles = string.IsNullOrEmpty(this.ProjectFileExtension) ? Enumerable.Empty<FileInfo>() : di.GetFiles("*" + this.ProjectFileExtension, SearchOption.AllDirectories);
                var projFileCount = this.ProjectFiles.Count();

                string afterAction = this.LanguageSetting.AfterAction;

                switch (afterAction)
                {
                    case nameof(AfterActionType.BuildAndRun):
                        this.Feedback(this.ProjectInfo, "It begins to build project.");
                        if (slnFileCount > 0)
                        {
                            foreach (var slnFile in this.SolutionFiles)
                            {
                                this.Feedback(this.ProjectInfo, $"Build using file \"{slnFile.FullName}\".");
                                await this.Build(slnFile);
                            }
                        }
                        else if (projFileCount == 1)
                        {
                            await this.Build(this.ProjectFiles.First());
                        }
                        else
                        {
                            Utility.OpenFolder(workDirectory);
                            return;
                        }

                        if (!this.ProjectInfo.HasError)
                        {
                            await this.Run();
                        }

                        break;
                    case nameof(AfterActionType.OpenSolutionOrProject):
                        this.Feedback(this.ProjectInfo, "It begins to open solution or project.");
                        if (slnFileCount > 0)
                        {
                            if (slnFileCount == 1)
                            {
                                this.OpenSolutionOrProject(this.SolutionFiles.First().FullName);
                            }
                            else
                            {
                                Utility.OpenFolder(workDirectory);
                            }
                        }
                        else if (projFileCount == 1)
                        {
                            this.OpenSolutionOrProject(this.ProjectFiles.First().FullName);
                        }
                        else
                        {
                            this.OpenSolutionOrProject(workDirectory);
                        }
                        break;
                    case nameof(AfterActionType.OpenInExplorer):
                        this.Feedback(this.ProjectInfo, "It begins to open solution or project in explorer.");
                        Utility.OpenFolder(workDirectory);
                        break;
                    default:
                        this.Feedback(this.ProjectInfo, $"You haven't specified the action for language {this.ProjectInfo.Language}.", FeedbackInfoType.Warnning);
                        Utility.OpenFolder(workDirectory);
                        break;
                }
            }
        }

        private void OpenSolutionOrProject(string filePath)
        {
            var languageSetting = this.LanguageSetting;
            string args = languageSetting?.OpenToolArgs;

            string pathPlaceholder = "$path$";

            string arguments = args?.Replace(pathPlaceholder, $"\"{filePath}\"");

            ProcessStartInfo processStartInfo = new ProcessStartInfo();

            if (!string.IsNullOrEmpty(languageSetting.OpenToolPath) && File.Exists(languageSetting.OpenToolPath))
            {
                processStartInfo.FileName = languageSetting.OpenToolPath;
                if (!string.IsNullOrEmpty(args) && args.Contains(pathPlaceholder))
                {
                    processStartInfo.Arguments = arguments;
                }
                else
                {
                    processStartInfo.Arguments = filePath + (string.IsNullOrEmpty(arguments) ? "" : " " + arguments);
                }                
            }
            else if (File.Exists(filePath))
            {
                processStartInfo.FileName = filePath + (string.IsNullOrEmpty(arguments) ? "" : " " + arguments);
            }
            else if (Directory.Exists(filePath))
            {
                Utility.OpenFolder(filePath);
                return;
            }

            if (languageSetting?.OpenAsAdmin == true)
            {
                processStartInfo.Verb = "runas";
            }

            try
            {
                Process.Start(processStartInfo);
            }
            catch (Exception ex)
            {
                this.Feedback(this.ProjectInfo, $"Failed to open file \"{filePath}\":{ex.Message}", FeedbackInfoType.Error);
            }
        }

        public virtual async Task Build(FileInfo file)
        {
            this.Feedback(this.ProjectInfo, $"Build using file:{file.FullName}");
        }

        public virtual async Task Run()
        {
            this.Feedback(this.ProjectInfo, $"Begin to run project:{this.ProjectInfo.Name}");
        }

        public void Feedback(ProjectInfo info, string content, FeedbackInfoType infoType = FeedbackInfoType.Info, bool enableLog = true)
        {
            FeedbackHelper.Feedback(this.observer, new FeedbackInfo() { InfoType = infoType, Content = content, Source = info }, LogHelper.DefaultLogFilePath, enableLog);
        }
    }
}
