using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class MsBuildStringLogger : Logger
    {
        public string Message { get; private set; }
        private StringBuilder sb = new StringBuilder();
        public override void Initialize(IEventSource eventSource)
        {
            eventSource.ProjectStarted += new ProjectStartedEventHandler(eventSource_ProjectStarted);
            eventSource.TaskStarted += new TaskStartedEventHandler(eventSource_TaskStarted);
            eventSource.MessageRaised += new BuildMessageEventHandler(eventSource_MessageRaised);
            eventSource.WarningRaised += new BuildWarningEventHandler(eventSource_WarningRaised);
            eventSource.ErrorRaised += new BuildErrorEventHandler(eventSource_ErrorRaised);
            eventSource.ProjectFinished += new ProjectFinishedEventHandler(eventSource_ProjectFinished);
        }

        void eventSource_ErrorRaised(object sender, BuildErrorEventArgs e)
        {
            string line = String.Format(": ERROR {0}({1},{2}): ", e.File, e.LineNumber, e.ColumnNumber);
            WriteLineWithSenderAndMessage(line, e);
        }

        void eventSource_WarningRaised(object sender, BuildWarningEventArgs e)
        {           
            string line = String.Format(": Warning {0}({1},{2}): ", e.File, e.LineNumber, e.ColumnNumber);
            WriteLineWithSenderAndMessage(line, e);
        }

        void eventSource_MessageRaised(object sender, BuildMessageEventArgs e)
        {
            if ((e.Importance == MessageImportance.High && IsVerbosityAtLeast(LoggerVerbosity.Minimal))
                || (e.Importance == MessageImportance.Normal && IsVerbosityAtLeast(LoggerVerbosity.Normal))
                || (e.Importance == MessageImportance.Low && IsVerbosityAtLeast(LoggerVerbosity.Detailed))
                )
            {
                WriteLineWithSenderAndMessage(String.Empty, e);
            }
        }

        void eventSource_TaskStarted(object sender, TaskStartedEventArgs e)
        {
            
        }

        void eventSource_ProjectStarted(object sender, ProjectStartedEventArgs e)
        {            
            WriteLine(String.Empty, e);            
        }

        void eventSource_ProjectFinished(object sender, ProjectFinishedEventArgs e)
        {          
            WriteLine(String.Empty, e);
        }

        private void WriteLineWithSenderAndMessage(string line, BuildEventArgs e)
        {
            if (0 == String.Compare(e.SenderName, "MSBuild", true /*ignore case*/))
            {                
                WriteLine(line, e);
            }
            else
            {
                WriteLine(e.SenderName + ": " + line, e);
            }
        }

        private void WriteLine(string line, BuildEventArgs e)
        {           
            sb.AppendLine(line + e.Message);
        }

        public override void Shutdown()
        {
            this.Message = sb.ToString();
        }
    }
}
