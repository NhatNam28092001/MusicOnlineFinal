using OnlineMusic.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineMusic.Controllers
{
    public class Blog1Controller : Controller
    {
        // GET: Blog1
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult Blog()
        {
            var model = new BLOG_DAO().ListAll();
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult Blog_Detail(int id)
        {
            var model = new BLOG_DAO().ViewDetail(id);
            return PartialView(model);
        }
    }
}