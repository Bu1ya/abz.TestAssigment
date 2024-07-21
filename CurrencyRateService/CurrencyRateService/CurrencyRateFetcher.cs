using CurrencyRateService.Models;
using CurrrencyRatesService;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CurrencyRateService
{
    public class CurrencyRateFetcher
    {
        public async Task FetchAndSaveCurrencyRates(string apiUrl, Config config)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    string xmlContent = await response.Content.ReadAsStringAsync();
                    
                    Logger.Instance().LogInfo($"Recived http response from {apiUrl}");

                    List<CurrencyRate> currencyRates = new List<CurrencyRate>();

                    XDocument xmlDoc = XDocument.Parse(xmlContent);
                    foreach (XElement currency in xmlDoc.Descendants("currency"))
                    {
                        CurrencyRate currencyRate = new CurrencyRate();

                        currencyRate._exchangeDate = currency.Element("exchangedate").Value;
                        currencyRate._currencyCode = currency.Element("r030").Value;
                        currencyRate._currencyRate = currency.Element("rate").Value;
                        currencyRate._nameUA = currency.Element("txt").Value;
                        currencyRate._name = currency.Element("cc").Value;
                        
                        currencyRates.Add(currencyRate);
                    }

                    try
                    {
                        IResultFileGenerator generator = GetResultFileGenerator(config);
                        generator.GenerateResult(currencyRates);
                    }
                    catch(Exception e)
                    {
                        Logger.Instance().LogError(e.Message);
                        return;
                    }

                    Logger.Instance().SaveLog();
                }
                catch (HttpRequestException e)
                {
                    Logger.Instance().LogError($"Error during http request: {e.Message}");
                }
                catch (Exception e)
                {
                    Logger.Instance().LogError($"Unexpected error: {e.Message}");
                }
            }
        }

        public IResultFileGenerator GetResultFileGenerator(Config config)
        {
            switch (config.OutputFormat)
            {
                case "csv":
                    return new CSVFileGenerator(config);
                case "json":
                    return new JSONFileGenerator(config);
                case "xml":
                    return new XMLFileGenerator(config);
                default:
                    throw new Exception("Invalid format. Please change format using custom commands or restart service with valid value.");
            }
        }
    }
}
