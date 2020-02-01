using System;
using System.IO;
using System.Threading.Tasks;

namespace GithubProjectHandler
{
    public class CInterpreter : LanguageInterpreter
    {
        public override string Language => "C";

        public override string SolutionFileExtension => null;

        public override string ProjectFileExtension => ".c";

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
