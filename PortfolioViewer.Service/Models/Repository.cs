using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortfolioViewer.Service.Models
{
    public class Repository : IRepository
    {
        private PortfolioViewerContext db;

        public Repository(PortfolioViewerContext db)
        {
            this.db = db;
        }

        public IQueryable<Portfolio> GetAllPortfolios()
        {
            return db.Portfolios;
        }

        public IQueryable<Portfolio> GetAllPortfoliosWithSecurities()
        {
            return db.Portfolios.Include("Securities");
        }

        public Portfolio GetPortfolio(int id)
        {
            return db.Portfolios.Include("Securities").FirstOrDefault(o => o.Id == id);
        }
    }
}