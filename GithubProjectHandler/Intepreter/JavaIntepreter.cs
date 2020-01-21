using System;

namespace GithubProjectHandler.Intepreter
{
    public class JavaIntepreter : LanguageIntepreter
    {
        public override string Language => "Java";

        public override string SolutionFileExtension => null;

        public override string ProjectFileExtension => null;

        public override string ExecutableFileExtension => ".exe";
    }
}
