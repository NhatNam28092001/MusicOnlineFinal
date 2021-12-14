using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineMusic.DAO;

namespace OnlineMusic.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new PRODUCTCATEGORY_DAO().ListAll();
            return PartialView(model);
        }
    }
}