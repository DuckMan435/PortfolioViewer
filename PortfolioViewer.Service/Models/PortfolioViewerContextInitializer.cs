using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PortfolioViewer.Service.Models
{
    public class PortfolioViewerContextInitializer : DropCreateDatabaseIfModelChanges<PortfolioViewerContext>
    {
        protected override void Seed(PortfolioViewerContext context)
        {
            var portfolios = new List<Portfolio>()
            {
                new Portfolio()
                {
                    UserName = "Client 1",
                    Securities = new List<Security>()
                            {
                                new Security() { Symbol = "AAPL", Quantity = 500, Type = SecurityType.Stock },
                                new Security() { Symbol = "VFSTX", Quantity = 1000, Type = SecurityType.Fund },
                                new Security() { Symbol = "VGSTX", Quantity = 1500, Type = SecurityType.Fund },
                                new Security() { Symbol = "VTHRX", Quantity = 2000, Type = SecurityType.Fund },
                            }
                },
                new Portfolio()
                {
                    UserName = "Client 2",
                    Securities = new List<Security>()
                            {
                                new Security() { Symbol = "GOOG", Quantity = 500, Type = SecurityType.Stock },
                                new Security() { Symbol = "VFSTX", Quantity = 1000, Type = SecurityType.Fund },
                                new Security() { Symbol = "VGSTX", Quantity = 1500, Type = SecurityType.Fund },
                                new Security() { Symbol = "VTHRX", Quantity = 2000, Type = SecurityType.Fund },
                            }
                },
                new Portfolio()
                {
                    UserName = "Client 3",
                    Securities = new List<Security>()
                            {
                                new Security() { Symbol = "MSFT", Quantity = 1500, Type = SecurityType.Stock },
                                new Security() { Symbol = "VFSTX", Quantity = 1000, Type = SecurityType.Fund },
                                new Security() { Symbol = "VGSTX", Quantity = 1500, Type = SecurityType.Fund },
                                new Security() { Symbol = "VTHRX", Quantity = 2000, Type = SecurityType.Fund },
                            }
                }
            };

            context.Portfolios.AddRange(portfolios);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}