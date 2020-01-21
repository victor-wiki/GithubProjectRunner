namespace GithubProjectHandler
{
    public class UrlHelper
    {
        public static bool IsValidUrl(string url)
        {
            return !string.IsNullOrEmpty(url) && (url.StartsWith("www") || url.StartsWith("http"));
        }        
    }
}
