namespace WebApp.Models
{
    public class Rate
    {
        public string currency;
        public string code;
        public double bid;
        public double ask;
    }

    public class Root
    {
        public string table;
        public string no;
        public string effectiveDate;
        public string tradingDate;

        public ICollection<Rate> rates;
    }
}
