using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malshinon1.logger
{
    using System;
    using System.IO;

    public static class Logger
    {
        private const int MaxLinesPerFile = 100;
        private const string LogDirectory = "logs";
        private const string LogFilePrefix = "log_";
        private const string LogFileExtension = ".log";

        static Logger()
        {
            Directory.CreateDirectory(LogDirectory);
        }

        public static void Log(string message)
        {
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
            string logFilePath = GetAvailableLogFilePath();

            using (var writer = new StreamWriter(logFilePath, append: true))
            {
                writer.WriteLine(logMessage);
            }
        }

        private static string GetAvailableLogFilePath()
        {
            int index = 1;
            string path;

            do
            {
                path = Path.Combine(LogDirectory, $"{LogFilePrefix}{index}{LogFileExtension}");
                if (!File.Exists(path))
                    return path;

                int lineCount = File.ReadAllLines(path).Length;
                if (lineCount < MaxLinesPerFile)
                    return path;

                index++;
            }
            while (true);
        }

        public static void Info(string message) => Log("[INFO] " + message);
        public static void Error(string message) => Log("[ERROR] " + message);
    }

}
