namespace GithubProjectHandler
{
    public class AfterAction
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public enum AfterActionType
    {
        BuildAndRun=2,
        OpenSolutionOrProject=4,
        OpenInExplorer=8,
        Custom=10
    }
}
