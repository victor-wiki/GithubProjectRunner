using System;
using System.IO;
using System.Threading.Tasks;

namespace GithubProjectHandler.Intepreter
{
    public class PythonIntepreter : LanguageIntepreter
    {
        public override string Language => "Python";

        public override string SolutionFileExtension => null;

        public override string ProjectFileExtension => null;

        public override string ExecutableFileExtension => null;

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
