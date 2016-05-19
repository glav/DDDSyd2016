using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DDDSyd2016.Controllers
{
    public class SecuredController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Title = "Secured Page";

            return View();
        }
    }
}
