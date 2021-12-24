using OnlineMusic.DAO;
using OnlineMusic.EF;
using OnlineMusic.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public ActionResult SmallCart()
        {
            var cart = Session[CartSession];
            var list = new List<CartItems>();
            if (cart != null)
            {
                list = (List<CartItems>)cart;
            }
            return PartialView(list);
        }
        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItems>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItems>>(cartModel);
            var sessCart = (List<CartItems>)Session[CartSession];

            foreach (var item in sessCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null && jsonItem.Quantity > 0)
                {
                    item.Quantity = jsonItem.Quantity;
                }
                else
                {
                    Session[CartSession] = sessCart;
                    return Json(new
                    {
                        status = false
                    }); ;
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
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItems>();
            if (cart != null)
            {
                list = (List<CartItems>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Payment(string txtshipName, string txtmobile, string txtaddress, string txtemail)
        {
            var order = new ORDER();
            order.CreateDate = DateTime.Now.ToString();
            order.ShipAddress = txtaddress;
            order.ShipEmail = txtemail;
            order.ShipName = txtshipName;
            order.ShipPhone = txtmobile;

            try
            {
                var id = new ORDER_DAO().Insert(order);
                var cart = (List<CartItems>)Session[CartSession];
                var detailDao = new ORDERDETAIL_DAO();
                decimal total = 0;
                foreach (var item in cart)
                {
                    var orderDetail = new ORDERDETAIL();
                    orderDetail.ProductID = item.Product.ID;
                    orderDetail.OrderID = id;
                    orderDetail.Price = item.Product.Price;
                    orderDetail.Quantity = item.Quantity;
                    detailDao.Insert(orderDetail);

                    total += (item.Product.Price.GetValueOrDefault(0) * item.Quantity);
                }
                /*string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/neworder.html"));

                content = content.Replace("{{CustomerName}}", shipName);
                content = content.Replace("{{Phone}}", mobile);
                content = content.Replace("{{Email}}", email);
                content = content.Replace("{{Address}}", address);
                content = content.Replace("{{Total}}", total.ToString("N0"));
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
*//*
                new MailHelper().SendMail(email, "Đơn hàng mới từ Hãng Đĩa Thời Đại| Time Records", content);
                new MailHelper().SendMail(toEmail, "Đơn hàng mới từ Hãng Đĩa Thời Đại| Time Records", content);*/
            }
            catch (Exception ex)
            {
                return Redirect("/loi-thanh-toan");
            }
            return Redirect("/hoan-thanh");
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}