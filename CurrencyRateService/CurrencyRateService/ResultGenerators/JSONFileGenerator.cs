using System.Collections.Generic;
using System.IO;
using CurrencyRateService.Models;
using CurrrencyRatesService;
using Newtonsoft.Json;

namespace CurrencyRateService
{
    public class JSONFileGenerator : IResultFileGenerator
    {
        public JSONFileGenerator(Config config) : base(config) { }

        public override void GenerateResult(List<CurrencyRate> currencyRates)
        {
            var formattedData = new List<Dictionary<string, string>>();

            foreach (var currency in currencyRates)
            {
                var data = new Dictionary<string, string>
                {
                    { "Currency", currency._nameUA },
                    { "Name", currency._name },
                    { "CurrencyCode", currency._currencyCode },
                    { "Rate", currency._currencyRate },
                    { "ExchangeDate", currency._exchangeDate }
                };

                formattedData.Add(data);
            }

            var json = JsonConvert.SerializeObject(formattedData, Formatting.Indented);
            File.WriteAllText($"{_config.OutputFilePath}.{_config.OutputFormat}", json);
            Logger.Instance().LogInfo($"JSON file with currency rate was saved in folder {_config.OutputFilePath}.{_config.OutputFormat}");
        }
    }
}
