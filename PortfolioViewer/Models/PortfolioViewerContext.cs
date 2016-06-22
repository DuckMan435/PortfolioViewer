using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace PortfolioViewer.Models
{
    public class PortfolioViewerContext : DbContext
    {
        static PortfolioViewerContext()
        {
            Database.SetInitializer(new PortfolioViewerContextInitializer());
        }

        public PortfolioViewerContext() : base("name=PortfolioViewerContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public DbSet<SecurityModel> Securities { get; set; }
        public DbSet<PortfolioModel> Portfolios { get; set; }
    }
}
