using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineMusic.DAO;

namespace OnlineMusic.Controllers
{
    public class CaSi1Controller : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult CaSi()
        {
            var model = new CASI_DAO().ListAll();
            return PartialView(model);
        }
    }
}