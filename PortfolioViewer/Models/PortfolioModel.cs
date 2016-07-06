using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped]
        public double PortfolioValue
        {
            get
            {
                double value = 0;

                foreach(SecurityModel security in Securities)
                {
                    StockModel stockModel = security as StockModel;
                    BondModel bondModel = security as BondModel;

                    if (stockModel != null)
                        value += stockModel.CurrentValue;
                    else if (bondModel != null)
                        value += bondModel.CurrentBondPrice;
                }

                return value;
            }
        }
    }
}