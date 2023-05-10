using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcmMobileShop.Areas.OneTechAdmin.Controllers
{
    public class HomeAdminController : Controller
    {
        //[Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/Home
        public ActionResult Index()
        {
            return View();
        }


    }
}