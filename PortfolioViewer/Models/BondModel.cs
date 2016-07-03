using System;

namespace PortfolioViewer.Models
{
    public class BondModel : SecurityModel
    {
        public string Name { get; set; }

        public DateTime? MaturityDate { get; set; }

        public double FaceValue { get; set; }

        public double BondInterestRate { get; set; }

        public double NumberOfPeriods
        {
            get
            {
                if (PurchaseDate != null && MaturityDate != null)
                    return MaturityDate.Value.Year - PurchaseDate.Value.Year;

                return 0;
            }
        }

        public double MarketInterestRate { get; set; }
    }
}