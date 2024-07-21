using System.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using CurrencyRateService;

namespace CurrrencyRatesService
{
    public class Config
    {
        private string _outputFilePath;
        private string _outputFormat;
        private string _fetchInterval;

        public string OutputFilePath
        {
            get
            {
                return _outputFilePath;
            }
            set
            {
                _outputFilePath = value;
            }
        }

        public string OutputFormat
        {
            get
            {
                return _outputFormat;
            }
            set
            {
                _outputFormat = value;
            }
        }

        public string FetchInterval
        {
            get
            {
                return _fetchInterval;
            }
            set
            {
                _fetchInterval = value;
            }
        }
        
        public Config(string pathToConfig)
        {
            GetDataFromConfig(pathToConfig);
        }

        public void GetDataFromConfig(string pathToConfig)
        {
            XDocument xmlDoc = XDocument.Load(pathToConfig);

            XElement config = xmlDoc.Element("Config");

            FetchInterval = config.Element("FetchInterval").Value;
            OutputFilePath = config.Element("OutputFilePath").Value;
            OutputFormat = config.Element("OutputFormat").Value;
        }

        public static void RevriteConfig(string outputFormat, string outputFilePath, string pathToConfig)
        {
            XDocument xmlDoc = XDocument.Load(pathToConfig);

            XElement config = xmlDoc.Element("Config");

            if (!string.IsNullOrEmpty(outputFilePath))
            {
                config.Element("OutputFilePath").Value = outputFilePath;
                Logger.Instance().LogInfo($"Output file path was changed to: {outputFilePath}");
            }
            if (!string.IsNullOrEmpty(outputFormat))
            {
                config.Element("OutputFormat").Value = outputFormat;
                Logger.Instance().LogInfo($"Output format was changed to: {outputFormat}");
            }

            xmlDoc.Save(pathToConfig);

            Logger.Instance().LogInfo("Saved succesully");
        }
    }
}
