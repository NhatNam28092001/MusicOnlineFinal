using OnlineMusic.DAO;
using OnlineMusic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OnlineMusic.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Cart()
        {
            var cart = Session[CartSession];
            var list = new List<CartItems>();
            if (cart != null)
            {
                list = (List<CartItems>)cart;
            }
            return View(list);
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItems>>(cartModel);
            var sessCart = (List<CartItems>)Session[CartSession];

            foreach(var item in sessCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if(jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = sessCart;
            return Json(new
            {
                status = true
            });
        }
        public ActionResult AddItem(long productID, int quantity)
        {
            var product = new PRODUCT_DAO().ViewDetail(productID);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItems>)cart;
                if (list.Exists(x => x.Product.ID == productID))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ID == productID)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItems();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
            }
            else
            {
                var item = new CartItems();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItems>();
                list.Add(item);

                Session[CartSession] = list;
            }
            return RedirectToAction("Cart");
        }
    }
}