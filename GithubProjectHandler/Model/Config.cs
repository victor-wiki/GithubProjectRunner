using System.Collections.Generic;

namespace GithubProjectHandler
{
    public class Config
    {
        public List<string> Languages { get; set; } = new List<string>();
        public List<AfterAction> AfterActions { get; set; } = new List<AfterAction>();
    }    
}
