namespace PortfolioViewer.Models
{
    public class FundModel : StockModel
    {
        #region Properties

        public override double Dividend { get; set; }

        public override double CurrentValue
        {
            get
            {
                return (Quantity * CurrentPrice) + ((Dividend / CurrentPrice) * 4 * Quantity);
            }
        }

        #region Override Properties

        public override string SecurityType
        {
            get
            {
                return "Fund";
            }
        }

        #endregion

        #endregion
    }
}