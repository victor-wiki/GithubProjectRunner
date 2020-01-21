using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubProjectHandler
{
    public class ProcessHelper
    {
        public static void StartFile(string fileName, string args, DataReceivedEventHandler outputEventHandler, DataReceivedEventHandler errorEventHandler)
        {
            Process p = new Process();

            p.StartInfo.FileName = fileName;
            p.StartInfo.Arguments = args;

            if (outputEventHandler != null)
            {
                p.OutputDataReceived += outputEventHandler;
            }

            if (errorEventHandler != null)
            {
                p.ErrorDataReceived += errorEventHandler;
            }

            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();

            if (outputEventHandler != null)
            {
                p.BeginOutputReadLine();
            }

            if (errorEventHandler != null)
            {
                p.BeginErrorReadLine();
            }

            p.StandardInput.AutoFlush = true;
            p.StandardInput.Flush();
            p.StandardInput.Close();
            p.WaitForExit();
            p.Close();
            p.Dispose();
        }

        public static void StartFileByCmd(string filePath, string args = null)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = args;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();

            string folder = Path.GetDirectoryName(filePath);
            p.StandardInput.WriteLine(Path.GetPathRoot(folder).Trim('\\'));
            p.StandardInput.WriteLine($"cd {folder}");
            p.StandardInput.WriteLine(filePath);

            p.StandardInput.AutoFlush = true;
            p.StandardInput.Flush();
            p.StandardInput.Close();
            p.WaitForExit();
            p.Close();
            p.Dispose();
        }
    }
}
