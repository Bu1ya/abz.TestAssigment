using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRateService
{
    public class Logger
    {

        private static Logger _instance;

        private string _logFilePath;
        private string _logFileDirectory;
        private string _logFileHistoryDirectory;
        private string _startLoggingTime;


        private Logger()
        {
            // Clear the log file if it exists, otherwise create it.
            _logFilePath = Assembly.GetExecutingAssembly().Location.Replace("CurrencyRateService.exe", "Logs\\log.log");
            _logFileDirectory = Assembly.GetExecutingAssembly().Location.Replace("CurrencyRateService.exe", "Logs");
            _logFileHistoryDirectory = Assembly.GetExecutingAssembly().Location.Replace("CurrencyRateService.exe", "Logs\\History");
            Directory.CreateDirectory(_logFileDirectory);
            if (!File.Exists(_logFilePath))
            {
                using (File.Create(_logFilePath)) { };       
            }
            if (!Directory.Exists($"{_logFileHistoryDirectory}"))
            {
                Directory.CreateDirectory($"{_logFileHistoryDirectory}");
            }
        }

        public static Logger Instance()
        {
            // Check if an instance exists
            if (_instance == null)
            {
                _instance = new Logger();
                _instance._startLoggingTime = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
            }

            // Return the existing instance
            return _instance;
        }

        public void Log(string message)
        {
            string logMessage = $"{DateTime.Now}: {message}";
            using (StreamWriter writer = new StreamWriter(_logFilePath, true))
            {
                writer.WriteLine(logMessage);
            }
        }

        public void LogError(string errorMessage)
        {
            string errorLogMessage = $"{DateTime.Now} ERROR: {errorMessage}";
            using (StreamWriter writer = new StreamWriter(_logFilePath, true))
            {
                writer.WriteLine(errorLogMessage);
            }
        }

        public void LogInfo(string infoMessage)
        {
            string infoLogMessage = $"{DateTime.Now} INFO: {infoMessage}";
            using (StreamWriter writer = new StreamWriter(_logFilePath, true))
            {
                writer.WriteLine(infoLogMessage);
            }
        }

        public void LogWarning(string warningMessage)
        {
            string warningLogMessage = $"{DateTime.Now} WARNING: {warningMessage}";
            using (StreamWriter writer = new StreamWriter(_logFilePath, true))
            {
                writer.WriteLine(warningLogMessage);
            }
        }
        public void SaveLog()
        {
            File.Copy(_logFilePath, $"{_logFileHistoryDirectory}\\{_startLoggingTime}.log", overwrite: true);
            LogInfo("Log saved");
        }

        public static void ClearLog()
        {
            using (File.Create(Logger.Instance()._logFilePath)) { };
        }

    }
}
