using System;

namespace PortfolioViewer.Models
{
    public class BondModel : SecurityModel
    {
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

        public double CurrentBondPrice
        {
            get
            {
                var redemptionValue = FaceValue / Math.Pow(1 + MarketInterestRate, NumberOfPeriods);
                var interestPaymentValue = BondInterestRate * FaceValue * ((1 - Math.Pow((1 + MarketInterestRate), -NumberOfPeriods)) / MarketInterestRate);

                return redemptionValue + interestPaymentValue;
            }
        }

        public override string SecurityType
        {
            get
            {
                return "Bond";
            }
        }
    }
}