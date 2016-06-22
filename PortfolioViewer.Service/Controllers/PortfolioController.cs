using PortfolioViewer.Models;
using PortfolioViewer.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortfolioViewer.Service.Controllers
{
    public class PortfolioController : ApiController
    {
        Portfolio[] portfolios = new Portfolio[]
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

        [HttpGet]
        public IEnumerable<Portfolio> Get()
        {
            return portfolios;
        }
    }
}
