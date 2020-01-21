using GithubProjectHandler;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string website = @"E:\exercise\m3u8Test\WebApplication2";
            Utility.RunDonetWebsite(website);           
           
            Console.Read();
        }

        static void Download()
        {
            string url = "https://github.com/victor-wiki/VideoConverter/archive/master.zip";
            //WebClient wc = new WebClient();

            //wc.DownloadProgressChanged += Client_DownloadProgressChanged;
            //wc.DownloadFileCompleted += Wc_DownloadFileCompleted;
            //wc.DownloadFileTaskAsync(url, "test2.zip");

            long length = Utility.GetFileSizeByUrl(url);

            Console.WriteLine(length);
        }

        private static void Wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            
        }

        static void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            WebClient wc = sender as WebClient;          
            long fileSize = 0;
            
            if (long.TryParse(wc.ResponseHeaders["Content-Length"], out fileSize))
            {
                Console.WriteLine(fileSize);
            }
        }
        static void Test2()
        {
            string projectFileName = @"E:\projects\git\GithubProjectRunner\GithubProjectRunner\bin\x64\Debug\download\AsposeWordsHelper-master\AsposeWordsHelper.Sample\AsposeWordsHelper.Sample.csproj";
                 // @"E:\projects\git\GithubProjectRunner\GithubProjectRunner\bin\x64\Debug\download\OrgChart-master\OrgChart.sln";
                
                //@"E:\projects\git\GithubProjectRunner\GithubProjectRunner\bin\x64\Debug\download\AsposeWordsHelper-master\AsposeWordsHelper.sln";

            //ProjectCollection projectCollection = new ProjectCollection();
            //BuildParameters buildParamters = new BuildParameters(projectCollection);
            //MsBuildStringLogger logger = new MsBuildStringLogger() { Parameters= "log.txt"};
            //buildParamters.Loggers = new List<Microsoft.Build.Framework.ILogger>() { logger };
            //Dictionary<string,string> globalProperty = new Dictionary<String, String>();            
            //BuildManager.DefaultBuildManager.ResetCaches();
            //BuildRequestData buildRequest = new BuildRequestData(projectFileName, globalProperty, null, new String[] { "Build" }, null);
            //var buildResult = BuildManager.DefaultBuildManager.Build(buildParamters, buildRequest);

            //string message = logger.Message;

            //if (buildResult.OverallResult == BuildResultCode.Failure)
            //{
            //    Console.WriteLine("Failed:" + message);
            //}
            //else
            //{
            //    Console.WriteLine(message);
            //}    

            CsharpIntepreter cs = new CsharpIntepreter();
            cs.Build(new FileInfo(projectFileName));
        }

        static void Test1()
        {
            Process p = new Process();
            string filePath = @"E:\projects\git\GithubProjectRunner\GithubProjectRunner\bin\x64\Debug\download\OrgChart-master\OrgChart.sln";
            string buildToolPath = @"D:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\MSBuild.exe";
            string cmd = $"-v:normal {filePath}";

            p.StartInfo.FileName = buildToolPath;// "cmd.exe";
            p.StartInfo.Arguments = cmd;// filePath;
            p.OutputDataReceived += P_OutputDataReceived;
            //p.ErrorDataReceived += P_ErrorDataReceived;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();        

            p.BeginOutputReadLine();
            //p.BeginErrorReadLine();

            p.StandardInput.AutoFlush = true;
            p.StandardInput.Flush();
            p.StandardInput.Close();
            p.WaitForExit();
            p.Close();
            p.Dispose();

            Console.WriteLine("OK");
        }

        private static void P_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                Console.WriteLine(e.Data);
            }
        }

        private static void P_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                Console.WriteLine(e.Data);
            }
        }
    }
}
