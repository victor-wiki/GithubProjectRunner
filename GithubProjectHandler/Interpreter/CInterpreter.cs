using System;
using System.IO;
using System.Threading.Tasks;

namespace GithubProjectHandler
{
    public class CInterpreter : LanguageInterpreter
    {
        public override string Language => "C";

        protected override string solutionFileExtension => null;

        protected override string projectFileExtension => ".c";

        protected override string executableFileExtension => ".exe";

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
