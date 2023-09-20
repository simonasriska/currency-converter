namespace currency_converter_api.Models
{
    public class ConversionResult
    {
        public double Amount { get; set; }
        public double ConvertedAmout { get; set; }
        public ExchangeRate AppliedRate { get; set; }
    }
}
