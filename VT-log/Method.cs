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
        public static void CreeUnNouveauxTXT()
        {
            string logDir = $"{Plugin.Config.LogDir}\\{Server.Get.Port}\\";
            string date = DateTime.Now.ToString("yyyy_MM_dd at HH_mm_ss");
            logFile = $"{logDir}\\Log_{date}.txt";

            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }

            System.IO.File.CreateText(logFile);
        }

        /// <summary>
        /// write to the log file
        /// </summary>
        public static void EcrirTXT(string Message)
        {
            using (StreamWriter sw = File.AppendText(logFile))
            {
                sw.WriteLine($"{DateTime.Now} : {Message}");
                sw.Close();
            }
        }
    }
}
