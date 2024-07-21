using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CurrencyRateService.Models;
using CurrrencyRatesService;
using System;
using System.Text;

namespace CurrencyRateService
{
    public class CSVFileGenerator : IResultFileGenerator
    {
        public CSVFileGenerator(Config config) : base(config) { }

        public override void GenerateResult(List<CurrencyRate> currencyRates)
        {
            var csv = new StringBuilder();

            csv.AppendLine("Currency  ,  Name  ,  CurrencyCode  ,  Rate  ,  ExchangeDate");

            foreach (var item in currencyRates)
            {
                var line = $"{EscapeCsv(item._nameUA)} , {EscapeCsv(item._name)} , {EscapeCsv(item._currencyCode)} , {EscapeCsv(item._currencyRate)} , {EscapeCsv(item._exchangeDate)}";
                csv.AppendLine(line);
            }

            File.WriteAllText($"{_config.OutputFilePath}.{_config.OutputFormat}", csv.ToString());

            Logger.Instance().LogInfo($"CSV file with currency rate was saved in folder {_config.OutputFilePath}.{_config.OutputFormat}");
        }
        private static string EscapeCsv(string value)
        {
            if (value.Contains(",") || value.Contains("\""))
            {
                value = $"\"{value.Replace("\"", "\"\"")}\"";
            }
            return value;
        }
    }
}
