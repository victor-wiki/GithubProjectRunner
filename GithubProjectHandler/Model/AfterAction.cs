namespace GithubProjectHandler
{
    public class AfterAction
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public enum AfterActionType
    {
        BuildAndRun=0,
        OpenSolutionOrProject=1,
        OpenInExplorer=2     
    }
}
