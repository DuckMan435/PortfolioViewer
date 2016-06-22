using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortfolioViewer.Models
{
    public class Repository : IRepository
    {
        private PortfolioViewerContext db;

        public Repository(PortfolioViewerContext db)
        {
            this.db = db;
        }

        public IQueryable<PortfolioModel> GetAllPortfolios()
        {
            return db.Portfolios;
        }

        public IQueryable<PortfolioModel> GetAllPortfoliosWithSecurities()
        {
            return db.Portfolios.Include("Securities");
        }

        public PortfolioModel GetPortfolio(int id)
        {
            return db.Portfolios.Include("Securities").FirstOrDefault(o => o.Id == id);
        }
    }
}