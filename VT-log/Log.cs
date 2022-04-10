using Synapse;
using Synapse.Api;
using System;
using System.IO;

namespace VTLog
{
    public class Log 
    {
        private static string File { get; set; }

        /// <summary>
        /// add log file
        /// </summary>
        public static void CreateNew()
        {
            string logDir = Path.Combine(Server.Get.Files.SynapseDirectory, "log");
            string subLogDir = Path.Combine(logDir, $"server-{Server.Get.Port}");
            string date = DateTime.Now.ToString("yyyy_MM_dd at HH_mm_ss");
            File = Path.Combine(subLogDir, $"Log_{date}.txt");

            if (!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);
            if (!Directory.Exists(subLogDir))
                Directory.CreateDirectory(subLogDir);

        }

        /// <summary>
        /// write to the log file
        /// </summary>
        public static void Write(string text)
        {
            using (var tw = new StreamWriter(File, true))
            {
                tw.WriteLine($"[{DateTime.Now}] : {text}");
            }
        }
    }
}
