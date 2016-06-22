using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PortfolioViewer.Models
{
    public class SecurityModel
    {
        public int Id { get; set; }

        public string Symbol { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SecurityType Type { get; set; }

        public int Quantity { get; set;  }
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
}