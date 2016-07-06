using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace PortfolioViewer.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(string error = "")
        {
            ViewBag.Message = error;
            return View();
        }
    }
}
