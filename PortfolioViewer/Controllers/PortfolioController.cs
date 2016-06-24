using PortfolioViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortfolioViewer.Controllers
{
    public class PortfolioController : ApiController
    {
        private IRepository _repo;

        public PortfolioController(IRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Returns a list of portfolios and their securities
        /// </summary>
        /// <returns></returns>
        public IQueryable<PortfolioModel> Get()
        {
            // Originally had Authorize tag, but it needed to be exposed as a separate call outside of the interface for the sake of this exercise

            if (string.IsNullOrEmpty(this.RequestContext.Principal.Identity.Name))
                return _repo.GetAllPortfoliosWithSecurities();
            else
                return _repo.GetAllPortfoliosWithSecurities().Where(o => o.UserName == this.RequestContext.Principal.Identity.Name);
        }
    }
}
