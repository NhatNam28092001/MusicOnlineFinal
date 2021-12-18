using OnlineMusic.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineMusic.Controllers
{
    public class Product1Controller : Controller
    {
        // GET: Product1
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult Product()
        {
            var model = new PRODUCT_DAO().ListAll();
            return PartialView(model);
        }

        public ActionResult Product_Detail(int id)
        {
            var product = new PRODUCT_DAO().ViewDetail(id);
            ViewBag.ProductCategory = new PRODUCTCATEGORY_DAO().ViewDetail(product.ID); 
            return View(product);
        }
    }
}