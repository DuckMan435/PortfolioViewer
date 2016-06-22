using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortfolioViewer.Service.Models
{
    public class Security
    {
        public int Id { get; set; }

        public string Symbol { get; set; }

        public string Name { get; set; }

        public SecurityType Type { get; set; }

        public int Quantity { get; set; }
    }

    public enum SecurityType
    {
        Fund,
        Stock,
        Bond
    }
}