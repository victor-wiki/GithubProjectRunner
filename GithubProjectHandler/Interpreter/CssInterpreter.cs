namespace GithubProjectHandler
{
    public class CssInterpreter : LanguageInterpreter
    {
        public override string Language => "CSS";

        protected override string solutionFileExtension => null;

        protected override string projectFileExtension => null;

        protected override string executableFileExtension => null;
        
    }
}
