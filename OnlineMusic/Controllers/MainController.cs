using OnlineMusic.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineMusic.Controllers
{
    public class MainController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var productdao = new PRODUCT_DAO();
            ViewBag.SANPHAM = productdao.ListSanPham(1);
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Blog()
        {
            var blogdao = new BLOG_DAO();
            ViewBag.NEWS = blogdao.ListBlog(1);
            return View();
        }
        
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Product()
        {
            return View();
        }
        public ActionResult Product_Detail()
        {
            return View();
        }
        public ActionResult Shopping_Cart()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult CaSi()
        {
            var model = new CASI_DAO().ListAll();
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult Slider()
        {
            var model = new SLIDE_DAO().ListAll();
            return PartialView(model);
        }
    }
}