using PortfolioViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortfolioViewer.Controllers
{
    [Authorize]
    public class PortfolioController : ApiController
    {
        private IRepository _repo;

        public PortfolioController(IRepository repo)
        {
            _repo = repo;
        }

        public IQueryable<PortfolioModel> Get()
        {
            return _repo.GetAllPortfoliosWithSecurities().Where(o => o.UserName == this.RequestContext.Principal.Identity.Name);
        }
    }
}
