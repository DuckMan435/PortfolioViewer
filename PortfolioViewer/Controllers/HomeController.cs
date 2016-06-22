using PortfolioViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace PortfolioViewer.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repo;

        public HomeController(IRepository repo)
        {
            _repo = repo;
        }

        public ActionResult Index()
        {
            //var listOfPortfolios = new PortfolioController(_repo).Get().First();

            return View();
        }
    }
}
