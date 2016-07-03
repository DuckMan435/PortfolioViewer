using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel;

namespace PortfolioViewer.Models
{
    public class SecurityModel
    {
        public int Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SecurityType Type { get; set; }

        public DateTime? PurchaseDate { get; set; }
    }

    public enum SecurityType
    {
        [Description("Bond")]
        Bond,
        [Description("Fund")]
        Fund,
        [Description("Stock")]
        Stock
    }

    public static class BondCalculatorHelper
    {
        public static double CalculateBondValue(double faceValue, double bondInterestRate, double marketInterestRate, int numberOfPeriods)
        {
            return CalculateRedemptionValue(faceValue, marketInterestRate, numberOfPeriods) + CalculateInterestPaymentValue(faceValue, bondInterestRate, marketInterestRate, numberOfPeriods);
        }

        private static double CalculateRedemptionValue(double faceValue, double marketInterestRate, int numberOfPeriods)
        {
            return faceValue / Math.Pow(1 + marketInterestRate, numberOfPeriods);
        }

        private static double CalculateInterestPaymentValue(double faceValue, double bondInterestRate, double marketInterestRate, int numberOfPeriods)
        {
            return bondInterestRate * faceValue * ((1 - Math.Pow((1 + marketInterestRate), -numberOfPeriods)) / marketInterestRate);
        }
    }
}