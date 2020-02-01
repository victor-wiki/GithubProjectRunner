using System;
using System.IO;
using System.Threading.Tasks;

namespace GithubProjectHandler
{
    public class ObjectiveCInterpreter : LanguageInterpreter
    {
        public override string Language => "Objective-C";

        public override string SolutionFileExtension => null;

        public override string ProjectFileExtension => ".m";

        public override string ExecutableFileExtension => ".exe";

        public override async Task Build(FileInfo file)
        {
            await base.Build(file);

            throw new NotImplementedException();
        }

        public override Task Run()
        {
            throw new NotImplementedException();
        }
    }
}
