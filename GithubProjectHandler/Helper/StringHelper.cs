namespace GithubProjectHandler
{
    public class StringHelper
    {
        public static string GetNonEmptyValue(params string[] values)
        {
            if (values != null)
            {
                foreach(string value in values)
                {
                    if(!string.IsNullOrEmpty(value))
                    {
                        return value;
                    }
                }
            }
            return null;
        }
    }
}
