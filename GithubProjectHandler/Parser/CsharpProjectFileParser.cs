using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GithubProjectHandler
{
    public class CsharpProjectFileParser
    {
        public string FilePath { get; set; }
        public CsharpProjectFileParser(string filePath)
        {
            this.FilePath = filePath;
        }

        public CsharpProjectFileInfo Parse()
        {
            CsharpProjectFileInfo info = new CsharpProjectFileInfo();

            XDocument doc = XDocument.Load(this.FilePath);
            var elements = doc.Root.Elements().Where(item => item.Name.LocalName == "PropertyGroup");

            var basicElement = elements.FirstOrDefault(item => item.Elements().Any(t => t.Name.LocalName == "Configuration"));

            if (basicElement != null)
            {
                info.Configuration = this.GetChildElementValue(basicElement, "Configuration");
                info.OutputType = this.GetChildElementValue(basicElement, "OutputType"); 
                info.AssemblyName = this.GetChildElementValue(basicElement, "AssemblyName");
                info.IsServiceProject = !string.IsNullOrEmpty(this.GetChildElementValue(basicElement, "WcfConfigValidationEnabled"));

                var configurationElement = elements.FirstOrDefault(item => item.Attribute("Condition") != null && item.Attribute("Condition").Value.Contains(info.Configuration));

                if (configurationElement != null)
                {
                    info.OutputPath = this.GetChildElementValue(configurationElement, "OutputPath"); 
                }
            }

            return info;
        }

        private string GetChildElementValue(XElement element, string childName)
        {
            return element.Elements().FirstOrDefault(item => item.Name.LocalName == childName)?.Value;
        }
    }
}
