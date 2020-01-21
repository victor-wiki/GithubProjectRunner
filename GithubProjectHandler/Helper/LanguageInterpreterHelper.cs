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
        public static LanguageIntepreter GetInterpreter(ProjectInfo projectInfo)
        {
            var assembly = Assembly.GetExecutingAssembly();         
            var typeArray = assembly.ExportedTypes;

            var types = (from type in typeArray
                         where type.IsSubclassOf(typeof(LanguageIntepreter))
                         select type).ToList();

            foreach (var type in types)
            {
                LanguageIntepreter interpreter  = (LanguageIntepreter)Activator.CreateInstance(type);

                if (interpreter.Language == projectInfo.Language)
                {
                    interpreter.ProjectInfo = projectInfo;
                    return interpreter;
                }
            }
            
            return null;
        }
    }
}
