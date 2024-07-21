using CurrrencyRatesService;
using System;
using System.ComponentModel;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;

namespace CurrencyRateService
{
    [RunInstaller(true)]
    partial class CurrencyRateService : ServiceBase
    {
        private Thread _worker = null;
        private CurrencyRateFetcher _fetcher;
        private Config _config;
        private string _pathToConfig;
        private static string _apiUrl = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?date=";

        public CurrencyRateService()
        {
            ServiceName = "CurrencyRateService";
            CanHandlePowerEvent = true;
            CanPauseAndContinue = true;
            CanShutdown = true;
            CanStop = true;
        }

        protected override void OnStart(string[] args)
        {
            Logger.ClearLog();
            Logger.Instance().LogInfo("Logging started.");
            Logger.Instance().LogInfo("CurrencyRateService launched.");
            _pathToConfig = Assembly.GetExecutingAssembly().Location.Replace("CurrencyRateService.exe","AppSettings.xml");
            try
            {
                _config = new Config(_pathToConfig);
                _fetcher = new CurrencyRateFetcher();
                ThreadStart start = new ThreadStart(Working);
                _worker = new Thread(start);
                _worker.Start();
            }
            catch (Exception e)
            {
                Logger.Instance().LogError($"Unexpected error: {e.Message}");
            }
        }

        public void OnDebug()
        {
            OnStart(null);
        }
        public async void Working()
        {
            try
            {
                while (true)
                {
                    Logger.Instance().LogInfo("Obtaining exchange rate data from the NBU");
                    await _fetcher.FetchAndSaveCurrencyRates($"{_apiUrl}{DateTime.Today.ToString("yyyyMMdd")}", _config);
                    Thread.Sleep(int.Parse(_config.FetchInterval) * 1000);
                }
            }
            catch (Exception e)
            {
                Logger.Instance().LogError($"Unexpected error: {e.Message}");
                throw;
            }
        }

        protected override void OnStop()
        {
            if (_worker != null && _worker.IsAlive)
            {
                Logger.Instance().SaveLog();
                _worker.Abort();
            }

        }

        protected override void OnCustomCommand(int command)
        {
            base.OnCustomCommand(command);
            if (command >= 128 && command <= 131)
            {
                switch (command)
                {
                    case 128:
                        Logger.Instance().LogInfo($"Recived command to change output file path to {_config.OutputFilePath}.");
                        break;
                    case 129:
                        Logger.Instance().LogInfo("Recived command to change output format to xml.");
                        Config.RevriteConfig("xml", null, _pathToConfig);
                        break;
                    case 130:
                        Logger.Instance().LogInfo("Recived command to change output format to csv.");
                        Config.RevriteConfig("csv", null, _pathToConfig);
                        break;
                    case 131:
                        Logger.Instance().LogInfo("Recived command to change output format to json.");
                        Config.RevriteConfig("json", null, _pathToConfig);
                        break;
                }
                _config = new Config(_pathToConfig);
            }
            else
            {
                Logger.Instance().LogWarning("Unknown command");
            }
        }
    }
}
