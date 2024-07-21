using CurrencyRateService.Models;
using CurrrencyRatesService;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
