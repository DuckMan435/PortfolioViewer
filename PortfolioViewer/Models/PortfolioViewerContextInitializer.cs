using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

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
                                new SecurityModel() { Symbol = "AAPL", Quantity = 500, Type = SecurityType.Stock },
                                new SecurityModel() { Symbol = "VFSTX", Quantity = 1000, Type = SecurityType.Fund },
                                new SecurityModel() { Symbol = "VGSTX", Quantity = 1500, Type = SecurityType.Fund },
                                new SecurityModel() { Symbol = "VTHRX", Quantity = 2000, Type = SecurityType.Fund },
                            }
                },
                new PortfolioModel()
                {
                    Name = "Client 2",
                    UserName = "Client 2",
                    Securities = new List<SecurityModel>()
                            {
                                new SecurityModel() { Symbol = "GOOG", Quantity = 500, Type = SecurityType.Stock },
                                new SecurityModel() { Symbol = "VFSTX", Quantity = 1000, Type = SecurityType.Fund },
                                new SecurityModel() { Symbol = "VGSTX", Quantity = 1500, Type = SecurityType.Fund },
                                new SecurityModel() { Symbol = "VTHRX", Quantity = 2000, Type = SecurityType.Fund },
                            }
                },
                new PortfolioModel()
                {
                    Name = "Client 3",
                    UserName = "Client 3",
                    Securities = new List<SecurityModel>()
                            {
                                new SecurityModel() { Symbol = "MSFT", Quantity = 1500, Type = SecurityType.Stock },
                                new SecurityModel() { Symbol = "VFSTX", Quantity = 1000, Type = SecurityType.Fund },
                                new SecurityModel() { Symbol = "VGSTX", Quantity = 1500, Type = SecurityType.Fund },
                                new SecurityModel() { Symbol = "VTHRX", Quantity = 2000, Type = SecurityType.Fund },
                            }
                }
            };

            context.Portfolios.AddRange(portfolios);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}