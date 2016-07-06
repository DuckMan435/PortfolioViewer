using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PortfolioViewer.Models
{
    public class PortfolioViewerContextInitializer : DropCreateDatabaseIfModelChanges<PortfolioViewerContext>
    {
        protected override void Seed(PortfolioViewerContext context)
        {
            var portfolios = new List<PortfolioModel>()
            {
                new PortfolioModel()
                {
                    Name = "Client 1",
                    UserName = "Client 1",
                    Securities = new List<SecurityModel>()
                            {
                                new StockModel() { Symbol = "AAPL", PurchasePrice = 105.35, Quantity = 500, PurchaseDate = new DateTime(2016, 1, 4) },
                                new FundModel() { Symbol = "VFSTX", PurchasePrice = 10.56, Quantity = 1000, PurchaseDate = new DateTime(2016, 1, 4), Dividend = .25 },
                                new BondModel() { Name = "U.S. $1000 10 Year", PurchaseDate = new DateTime(2016, 1, 4), MaturityDate = new DateTime(2026, 1, 4),
                                                      FaceValue = 1000, BondInterestRate = .10, MarketInterestRate = .02 },
                            }
                },
                new PortfolioModel()
                {
                    Name = "Client 2",
                    UserName = "Client 2",
                    Securities = new List<SecurityModel>()
                            {
                                new StockModel() { Symbol = "GOOG", PurchasePrice = 741.84, Quantity = 500, PurchaseDate = new DateTime(2016, 1, 4) },
                                new FundModel() { Symbol = "VGSTX", PurchasePrice = 23.05, Quantity = 1000, PurchaseDate = new DateTime(2016, 1, 4), Dividend = .50 },
                                new BondModel() { Name = "U.S. $1000 20 Year", PurchaseDate = new DateTime(2016, 1, 4), MaturityDate = new DateTime(2036, 1, 4),
                                                      FaceValue = 1000, BondInterestRate = .05, MarketInterestRate = .02 },
                            }
                },
                new PortfolioModel()
                {
                    Name = "Client 3",
                    UserName = "Client 3",
                    Securities = new List<SecurityModel>()
                            {
                                new StockModel() { Symbol = "MSFT", PurchasePrice = 54.80,  Quantity = 1500, PurchaseDate = new DateTime(2016, 1, 4) },
                                new FundModel() { Symbol = "VTHRX", PurchasePrice = 27.39, Quantity = 2000, PurchaseDate = new DateTime(2016, 1, 4), Dividend = .75 },
                                new BondModel() { Name = "U.S. $1000 30 Year", PurchaseDate = new DateTime(2016, 1, 4), MaturityDate = new DateTime(2046, 1, 4),
                                                      FaceValue = 1000, BondInterestRate = .15, MarketInterestRate = .02 },
                            }
                }
            };

            context.Portfolios.AddRange(portfolios);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}