using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using currency_converter_api.Models;

public class CurrencyService
{
    private readonly HttpClient _httpClient;

    public CurrencyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ExchangeRate>> FetchExchangeRatesAsync()
    {
        var response = await _httpClient.GetAsync("https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml?9d955f88fb61cb5fde41c9f33a370224");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to fetch exchange rates."); // Adjust error handling as needed.
        }

        var content = await response.Content.ReadAsStringAsync();
        return ParseEcbData(content);
    }

    private IEnumerable<ExchangeRate> ParseEcbData(string content)
    {
        XNamespace ns = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref";
        var xDocument = XDocument.Parse(content);

        var rateElements = xDocument.Descendants(ns + "Cube")
                                    .Where(e => e.Attribute("currency") != null);

        var exchangeRates = new List<ExchangeRate>();

        foreach (var rateElement in rateElements)
        {
            var currencyCode = rateElement.Attribute("currency").Value;
            var rate = double.Parse(rateElement.Attribute("rate").Value, System.Globalization.CultureInfo.InvariantCulture);
            var date = DateTime.Parse(xDocument.Descendants(ns + "Cube")
                                               .Where(e => e.Attribute("time") != null)
                                               .FirstOrDefault()
                                               .Attribute("time").Value);

            exchangeRates.Add(new ExchangeRate
            {
                TargetCurrency = new Currency { Code = currencyCode },
                Rate = rate,
                Date = date
            });
        }

        return exchangeRates;
    }
}