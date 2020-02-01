using System;
using System.IO;
using System.Threading.Tasks;

namespace GithubProjectHandler
{
    public class CplusInterpreter : LanguageInterpreter
    {
        public override string Language => "C++";

        protected override string solutionFileExtension => null;

        protected override string projectFileExtension => ".cpp";

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
