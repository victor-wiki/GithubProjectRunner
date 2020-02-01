namespace GithubProjectHandler
{
    public class HtmlInterpreter : LanguageInterpreter
    {
        public override string Language => "HTML";

        protected override string solutionFileExtension => null;

        protected override string projectFileExtension => null;

        protected override string executableFileExtension => null;
        
    }
}
