using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Threading;

namespace GithubProjectHandler
{
    public class Utility
    {
        public static readonly string CurrentFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static void OpenInExplorer(string filePath)
        {
            string cmd = "explorer.exe";
            string arg = "/select," + filePath;
            Process.Start(cmd, arg);
        }

        public static void OpenFolder(string folder)
        {
            Process.Start(folder);
        }

        public static long GetFileSizeByUrl(string url)
        {
            long length = 0;

            try
            {
                string key = "Content-Length";
                WebRequest req = HttpWebRequest.Create(url);
                req.Method = "HEAD";
                WebResponse resp = req.GetResponse();

                if (resp.Headers[key] != null)
                {
                    length = long.Parse(resp.Headers[key]);
                }

                if (length <= 0)
                {
                    using (WebClient webClient = new WebClient())
                    {
                        var stream = webClient.OpenRead(url);

                        if (webClient.ResponseHeaders[key] != null)
                        {
                            length = long.Parse(webClient.ResponseHeaders[key]);
                        }

                        stream.Close();
                        stream.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return length;
        }

        public static string GetFriendlyFileSize(long length)
        {
            double result = 0;
            string unit = "";
            if (length >= 1073741824)
            {
                result = length * 1.0 / 1073741824;
                unit = "G";
            }
            else if (length >= 1048576)
            {
                result = length * 1.0 / 1048576;
                unit = "M";
            }
            else
            {
                result = length * 1.0 / 1024;
                unit = "K";
            }

            string strSize = result.ToString("0.00");

            if ((int)result == result)
            {
                strSize = ((int)result).ToString();
            }

            return strSize + unit;
        }

        public static bool IsValidZipFile(string filePath)
        {
            bool valid = true;
            try
            {
                ZipFile zipFile = new ZipFile(filePath);
                valid = zipFile.TestArchive(true, TestStrategy.FindFirstError, null);
            }
            catch (Exception ex)
            {
                valid = false;
            }

            return valid;
        }

        public static int GetAvailableTCPPort(int startPort)
        {
            int port = startPort;
            bool isAvailable = true;

            var mutex = new Mutex(false, string.Concat("Global/", "8875BD8E-4D5B-11DE-B2F4-691756D89593"));

            mutex.WaitOne();

            try
            {
                IPGlobalProperties ipGlobalProperties =
                    IPGlobalProperties.GetIPGlobalProperties();
                IPEndPoint[] endPoints =
                    ipGlobalProperties.GetActiveTcpListeners();

                do
                {
                    if (!isAvailable)
                    {
                        port++;
                        isAvailable = true;
                    }

                    foreach (IPEndPoint endPoint in endPoints)
                    {
                        if (endPoint.Port != port) continue;
                        isAvailable = false;
                        break;
                    }

                } while (!isAvailable && port < IPEndPoint.MaxPort);

                if (!isAvailable)
                    throw new ApplicationException("Not able to find a free TCP port.");

                return port;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        public static void RunDonetWebsite(string path, Action<string> feedback)
        {
            string sysDrive = Path.GetPathRoot(Environment.SystemDirectory);
            List<string> iisExpressFolders = new List<string>() { @"Program Files\IIS Express", @"Program Files (x86)\IIS Express" };
            bool found = false;          

            foreach (string iisExpFolder in iisExpressFolders)
            {
                string iisExpPath = Path.Combine(sysDrive, iisExpFolder, "iisexpress.exe");
                if (File.Exists(iisExpPath))
                {
                    found = true;

                    int port = GetAvailableTCPPort(10000);
                    string args = $" /path:\"{path}\" /port:{port}";

                    ProcessHelper.StartFile(iisExpPath, args, (sender, e) =>
                    {
                        if (e.Data != null)
                        {
                            if (feedback != null)
                            {
                                feedback(e.Data);
                            }

                            if (e.Data.Contains("is running"))
                            {
                                OpenUrl($"http://localhost:{port}");
                            }
                        }

                    }, null);

                    break;
                }
            }

            if (!found)
            {
                CassiniDev.Server server = new CassiniDev.Server(path);
                server.Start();
                OpenUrl(server.RootUrl);
            }
        }

        public static void OpenUrl(string url)
        {
            string browser = GetSystemDefaultBrowser();

            if(!string.IsNullOrEmpty(browser))
            {
                Process.Start(browser, url);
            }
            else
            {
                Process.Start(url);
            }
        }

        public static string GetSystemDefaultBrowser()
        {
            string name = string.Empty;
            RegistryKey regKey = null;

            try
            {
                var regDefault = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\.htm\\UserChoice", false);
                var stringDefault = regDefault.GetValue("ProgId");

                regKey = Registry.ClassesRoot.OpenSubKey(stringDefault + "\\shell\\open\\command", false);
                name = regKey.GetValue(null).ToString().ToLower().Replace("" + (char)34, "");

                if (!name.EndsWith("exe"))
                {
                    name = name.Substring(0, name.LastIndexOf(".exe") + 4);
                }
            }
            catch (Exception ex)
            {                
            }
            finally
            {
                if (regKey != null)
                {
                    regKey.Close();
                }                 
            }          

            return name;
        }
    }
}
