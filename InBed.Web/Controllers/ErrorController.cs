using System.Web.Mvc;

namespace InBed.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: NotFound
        public ActionResult NotFound()
        {
            return View();
        }
    }
}