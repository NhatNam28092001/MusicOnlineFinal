using OnlineMusic.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineMusic.Controllers
{
    public class Slide1Controller : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult Slider()
        {
            var model = new SLIDE_DAO().ListAll();
            return PartialView(model);
        }
    }
}