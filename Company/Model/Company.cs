namespace Company.Model
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StockTicker { get; set; }
        public string Exchange { get; set; }
        public string Isin { get; set; }
        public string WebsiteUrl { get; set; }
    }
}
