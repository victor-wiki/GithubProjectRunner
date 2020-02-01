using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GithubProjectHandler
{
    public class LanguageInterpreterHelper
    {
        public static LanguageInterpreter GetInterpreter(ProjectInfo projectInfo)
        {
            LanguageInterpreter interpreter = GetInterpreter(projectInfo.Language);
            if (interpreter != null)
            {
                interpreter.ProjectInfo = projectInfo;
                return interpreter;
            }

            return null;
        }

        public static LanguageInterpreter GetInterpreter(string language)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var typeArray = assembly.ExportedTypes;

            var types = (from type in typeArray
                         where type.IsSubclassOf(typeof(LanguageInterpreter))
                         select type).ToList();

            foreach (var type in types)
            {
                LanguageInterpreter interpreter = (LanguageInterpreter)Activator.CreateInstance(type);

                if (interpreter.Language == language)
                {
                    return interpreter;
                }
            }
            return null;
        }
    }
}
