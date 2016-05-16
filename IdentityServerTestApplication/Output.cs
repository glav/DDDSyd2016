using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerTestApplication
{
    public static class Output
    {
        public static System.ConsoleColor OriginalForegroundColor = ConsoleColor.Gray;
        static string NEWLINE = System.Environment.NewLine;
        static bool _isStarted = false;
        private static Object _lockObject = new object();

        public static string LogFile { get { return "Output.log"; } }

        private static void StartNewSession()
        {
            Write($"{NEWLINE}>>>>> Start Session at {TimeStampInfo()} <<<<<",InfoType.General,true,false);
        }

        public static void FinishOutputSession()
        {
            Write($"{NEWLINE}>>>>> End Session at {TimeStampInfo()} <<<<<{NEWLINE}",InfoType.General, true, false);
            _isStarted = false;
        }

        public static void ClearLogFile()
        {
            System.IO.File.Delete(LogFile);
        }

        private static string TimeStampInfo(bool includeNewline = false)
        {
            var timeInfo = $"[{DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss")}] "; 
            if (includeNewline)
            {
                return $"{NEWLINE}{timeInfo}";
            }
            return timeInfo;
        }

        public static void Write(string message, InfoType infoType = InfoType.General, bool writeToFile = true, bool includeTimePrefix = true)
        {
            if (!_isStarted)
            {
                _isStarted = true;
                StartNewSession();
            }
            var color = GetForegroundColor(infoType);
            Console.ForegroundColor = color;
            string msg = string.Empty;
            if (includeTimePrefix)
            {
                msg = TimeStampInfo(true);
            }
            msg += $" {message}";
            Console.WriteLine(msg);
            Console.ForegroundColor = OriginalForegroundColor;
            WriteMessageToFileIfRequire(msg, infoType, writeToFile);
        }

        public static void Display(string message, InfoType infoType = InfoType.General)
        {
            var color = GetForegroundColor(infoType);
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ForegroundColor = OriginalForegroundColor;

        }

        private static void WriteMessageToFileIfRequire(string message, InfoType infoType, bool writeToFile)
        {
            if (writeToFile)
            {
                lock(_lockObject)
                {
                    System.IO.File.AppendAllText(LogFile, message);
                }
            }
        }

        private static System.ConsoleColor GetForegroundColor(InfoType infoType)
        {
            switch (infoType)
            {
                case InfoType.Error:
                    return ConsoleColor.Red;
                case InfoType.Warning:
                    return ConsoleColor.Yellow;
                case InfoType.Emphasis:
                    return ConsoleColor.White;
            }

            return ConsoleColor.Gray;
        }
    }

    public enum InfoType
    {
        General,
        Emphasis,
        Warning,
        Error
    }
}
