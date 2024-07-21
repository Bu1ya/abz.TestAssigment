using CurrencyRateService.Models;
using CurrrencyRatesService;
using System.Collections.Generic;
using System.Xml;

namespace CurrencyRateService
{
    public class XMLFileGenerator : IResultFileGenerator
    {
        public XMLFileGenerator(Config config) : base(config) { }

        public override void GenerateResult(List<CurrencyRate> currencyRates)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("CurrencyRates");
            xmlDoc.AppendChild(root);
            
            foreach (CurrencyRate currency in currencyRates)
            {
                XmlElement xmlCurrency = xmlDoc.CreateElement("Currency");

                XmlElement xmlCurrencyName = xmlDoc.CreateElement("Name");
                XmlElement xmlCurrencyCode = xmlDoc.CreateElement("CurrencyCode");
                XmlElement xmlCurrencyRate = xmlDoc.CreateElement("Rate");
                XmlElement xmlCurrencyDateExchange = xmlDoc.CreateElement("ExchangeDate");

                xmlCurrencyName.InnerText = currency._name;
                xmlCurrencyCode.InnerText = currency._currencyCode;
                xmlCurrencyRate.InnerText = currency._currencyRate;
                xmlCurrencyDateExchange.InnerText = currency._exchangeDate;

                xmlCurrency.AppendChild(xmlCurrencyName);
                xmlCurrency.AppendChild(xmlCurrencyCode);
                xmlCurrency.AppendChild(xmlCurrencyRate);
                xmlCurrency.AppendChild(xmlCurrencyDateExchange);

                root.AppendChild(xmlCurrency);
            }

            xmlDoc.Save($"{_config.OutputFilePath}.{_config.OutputFormat}");
            Logger.Instance().LogInfo($"XML file with currency rate was saved in folder {_config.OutputFilePath}.{_config.OutputFormat}");
        }
    }
}
