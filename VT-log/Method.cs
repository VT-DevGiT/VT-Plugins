using Synapse;
using System;
using System.IO;

namespace VTLog
{
    public class Method
    {
        private static string logFile;
        /// <summary>
        /// add log file
        /// </summary>
        public static void CreateNewTXT()
        {
            string logDir = Path.Combine(Server.Get.Files.SynapseDirectory, "log");
            string subLogDir = Path.Combine(logDir, $"{Server.Get.Port}");
            string date = DateTime.Now.ToString("yyyy_MM_dd at HH_mm_ss");
            logFile = Path.Combine(subLogDir, $"Log_{date}.txt");

            if (!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);
            if (!Directory.Exists(subLogDir))
                Directory.CreateDirectory(subLogDir);

            File.CreateText(logFile);
        }

        /// <summary>
        /// write to the log file
        /// </summary>
        public static void WriteTXT(string Message)
        {
            using (StreamWriter sw = File.AppendText(logFile))
            {
                sw.WriteLine($"[{DateTime.Now}] : {Message}");
                sw.Close();
            }
        }
    }
}
