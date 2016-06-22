using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortfolioViewer.Models
{
    public class PortfolioModel
    {
        public PortfolioModel()
        {
            Securities = new List<SecurityModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        //Navigation
        public virtual ICollection<SecurityModel> Securities { get; set; }
    }
}