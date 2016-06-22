using System.Linq;

namespace PortfolioViewer.Models
{
    public interface IRepository
    {
        IQueryable<PortfolioModel> GetAllPortfolios();
        IQueryable<PortfolioModel> GetAllPortfoliosWithSecurities();
        PortfolioModel GetPortfolio(int id);
    }
}