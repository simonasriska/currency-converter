namespace currency_converter_api.Models
{
    public class ExchangeRate
    {
        public Currency BaseCurrency { get; set; }
        public Currency TargetCurrency { get; set; }
        public double Rate { get; set; }
        public DateTime Date { get; set; }
    }
}
