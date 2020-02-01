using System;
using System.IO;
using System.Threading.Tasks;

namespace GithubProjectHandler
{
    public class PythonInterpreter : LanguageInterpreter
    {
        public override string Language => "Python";

        protected override string solutionFileExtension => null;

        protected override string projectFileExtension => null;

        protected override string executableFileExtension => null;

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
