namespace GithubProjectHandler
{
    public class JavaScriptInterpreter : LanguageInterpreter
    {
        public override string Language => "JavaScript";

        protected override string solutionFileExtension => null;

        protected override string projectFileExtension => null;

        protected override string executableFileExtension => null;
        
    }
}
