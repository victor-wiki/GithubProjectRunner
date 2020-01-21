﻿using System;
using System.IO;

namespace GithubProjectHandler
{
    public class LogHelper
    {
        public static string DefaultLogFileName => DateTime.Now.ToString("yyyyMMdd") + ".txt";

        public static string DefaultLogFilePath => Path.Combine(Utility.CurrentFolder, "log", DefaultLogFileName);

        public static bool EnableDebug { get; set; }
        private static object obj = new object();

        public static void LogInfo(string filePath, string info)
        {
            string folder = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string content = $"{DateTime.Now.ToString("yyyyMMdd HH.mm.ss")}:{info}";

            lock (obj)
            {
                File.AppendAllLines(filePath, new string[] { content });
            }

            if (EnableDebug)
            {
                Console.WriteLine(info);
            }
        }
    }
}
