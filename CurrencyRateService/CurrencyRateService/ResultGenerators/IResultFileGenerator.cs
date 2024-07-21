using CurrencyRateService.Models;
using CurrrencyRatesService;
using System.Collections.Generic;

namespace CurrencyRateService
{
    public abstract class IResultFileGenerator
    {
        protected Config _config;

        protected IResultFileGenerator(Config config)
        {
            _config = config;
        }

        public abstract void GenerateResult(List<CurrencyRate> currencyRates);
    }
}
