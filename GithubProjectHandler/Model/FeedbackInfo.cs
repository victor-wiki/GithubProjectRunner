namespace GithubProjectHandler
{
    public class FeedbackInfo
    {        
        public FeedbackInfoType InfoType { get; set; }
        public string Content { get; set; }
        public object Source { get; set; }       
    }

    public enum FeedbackInfoType
    {
        Info = 0,
        Warnning = 1,
        Error = 2     
    }
}
