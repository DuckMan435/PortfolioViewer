namespace PortfolioViewer.Service.Models
{
    public class PortfolioViewerContext : IdentityDbContext<ApplicationUser>
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

        public DbSet<Security> Securities { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
    }
}
