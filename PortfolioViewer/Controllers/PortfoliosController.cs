using Elmah.Contrib.WebApi;
using PortfolioViewer.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PortfolioViewer.Controllers
{
    public class PortfoliosController : ApiController
    {
        private IRepository _repo;

        public PortfoliosController(IRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Returns a list of portfolios and their securities
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Get()
        {
            // Originally had Authorize tag, but it needed to be exposed as a separate call outside of the interface for the sake of this exercise
            try
            {
                if (string.IsNullOrEmpty(this.RequestContext.Principal.Identity.Name))
                    return Request.CreateResponse(HttpStatusCode.OK, _repo.GetAllPortfoliosWithSecurities());
                else
                    return Request.CreateResponse(HttpStatusCode.OK, _repo.GetAllPortfoliosWithSecurities().Where(o => o.UserName == this.RequestContext.Principal.Identity.Name));
            }
            catch(Exception ex)
            {
                // Log actual Exception
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                // Return User Friendly Error
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed to load Portfolios with Securities");
            }
        }
    }
}
