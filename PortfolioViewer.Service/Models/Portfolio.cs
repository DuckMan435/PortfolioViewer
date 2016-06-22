using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortfolioViewer.Service.Models
{
    public class Portfolio
    {
        public Portfolio()
        {
            Securities = new List<Security>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        //Navigation
        public virtual ICollection<Security> Securities { get; set; }
    }
}