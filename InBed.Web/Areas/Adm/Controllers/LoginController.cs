using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InBed.Web.Areas.Adm.Controllers
{
    public class LoginController : AdmBaseController
    {
        // GET: Adm/Login
        public ActionResult Index()
        {
            return View();
        }
    }
}