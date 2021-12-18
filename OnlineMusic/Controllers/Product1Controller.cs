using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineMusic.DAO;

namespace OnlineMusic.Controllers
{
    public class Product1Controller : Controller
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
        public ActionResult DanhMuc(long id)
        {
            return View();
        }
        public ActionResult Product()
        {
            var productdao = new PRODUCT_DAO();
            ViewBag.SANPHAM = productdao.ListSanPham(1);
            return View();
        }

        public ActionResult Product_Detail(long id)
        {
            var product = new PRODUCT_DAO().ViewDetail(id);
            ViewBag.ProductCategory = new PRODUCTCATEGORY_DAO().ViewDetail(product.CategoryID.Value);
            ViewBag.RelatedProduct = new PRODUCT_DAO().ListRelatedSanPham(id);
            return View(product);
        }
    }
}