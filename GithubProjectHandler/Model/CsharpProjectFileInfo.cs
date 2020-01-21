using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubProjectHandler
{
    public class CsharpProjectFileInfo
    {
        public string Configuration { get; set; }
        public string AssemblyName { get; set; }
        public string OutputPath { get; set; }
        public string OutputType { get; set; }
        public bool IsServiceProject { get; set; }
    }
}
