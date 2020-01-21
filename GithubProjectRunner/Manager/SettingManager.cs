using GithubProjectHandler;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace GithubProjectRunner
{
    public class SettingManager
    {
        public const string DomainName = "github.com";
        private const string settingFileName = "setting.json";
        private static Setting _setting;

        public static readonly string DefaultDownloadFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "download");

        public static Setting Setting
        {
            get
            {
                if(_setting==null)
                {
                    return GetSetting();
                }
                return _setting;
            }

        }

        public static Setting GetSetting()
        {
            if(File.Exists(settingFileName))
            {
                string content = File.ReadAllText(settingFileName);
                Setting setting = (Setting) JsonConvert.DeserializeObject(content, typeof(Setting));
                return setting;
            }
            else
            {
                return new Setting();
            }
        }

        public static void SaveSetting(Setting setting)
        {
            _setting = setting;
            string content = JsonConvert.SerializeObject(setting, Formatting.Indented);
            File.WriteAllText(settingFileName, content);
        }
    }
}
