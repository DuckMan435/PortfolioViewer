using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PortfolioViewer.Controllers
{
    public class FuncController : ApiController
    {
        /// <summary>
        /// Calculates Current Stock Gain
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="currentPrice"></param>
        /// <param name="stockDividend"></param>
        /// <returns></returns>
        [HttpGet]
        public double CalculateCurrentStockGain(int quantity, double currentPrice, double stockDividend)
        {
            return (quantity * currentPrice) + ((stockDividend / currentPrice) * quantity);
        }

        /// <summary>
        /// Calculates Current Fund Gain
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="currentPrice"></param>
        /// <param name="fundDividend"></param>
        /// <returns></returns>
        [HttpGet]
        public double CalculateCurrentFundGain(int quantity, double currentPrice, double fundDividend)
        {
            return (quantity * currentPrice) + ((fundDividend / currentPrice) * 4 * quantity);
        }

        /// <summary>
        /// Calculates Current Bond Price
        /// </summary>
        /// <param name="faceValue"></param>
        /// <param name="bondInterestRate"></param>
        /// <param name="marketInterestRate"></param>
        /// <param name="numberOfPeriods"></param>
        /// <returns></returns>
        [HttpGet]
        public double CalculateCurrentBondPrice(double faceValue, double bondInterestRate, double marketInterestRate, double numberOfPeriods)
        {
            var redemptionValue = faceValue / Math.Pow(1 + marketInterestRate, numberOfPeriods);
            var interestPaymentValue = bondInterestRate * faceValue * ((1 - Math.Pow((1 + marketInterestRate), -numberOfPeriods)) / marketInterestRate);

            return redemptionValue + interestPaymentValue;
        }
    }
}
