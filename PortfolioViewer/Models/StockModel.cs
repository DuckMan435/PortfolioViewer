namespace PortfolioViewer.Models
{
    public class StockModel : SecurityModel
    {
        public string Symbol { get; set; }

        public int Quantity { get; set; }

        public double PurchasePrice { get; set; }
    }
}