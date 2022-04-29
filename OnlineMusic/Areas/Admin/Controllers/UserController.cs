using OnlineMusic.DAO;
using OnlineMusic.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineMusic.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        // GET: Admin/User/Sua/5
        public ActionResult Sua(string userName)
        {
            using (OnlineMusicDB dbModel = new OnlineMusicDB())
            {
                var kq = dbModel.USERs.Where(x => x.UserName == userName).FirstOrDefault();
                return View(kq);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Sua(string userName, USER user)
        {
            try
            {
                using (OnlineMusicDB dbModel = new OnlineMusicDB())
                {
                    dbModel.Entry(user).State = EntityState.Modified;
                    dbModel.SaveChanges();
                }
                return RedirectToAction("Danhsach");
            }
            catch
            {
                return View();
            }

        }
        public ActionResult Them(USER user)
        {
            if (ModelState.IsValid)
            {
                var dao = new USER_DAO();
                var id = dao.Insert(user);
                if (id == true)
                {
                    return RedirectToAction("Danhsach", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Them user ko thanh cong");
                }
            }
            return View("Index");
        }
        public ActionResult Danhsach(int page = 1, int pageSize = 4)
        {
            var dao = new USER_DAO();
            var model = dao.ListAllPaging(page, pageSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult Xoa(string userName)
        {
            using (OnlineMusicDB dbModel = new OnlineMusicDB())
            {
                return View(dbModel.USERs.Where(x => x.UserName == userName).FirstOrDefault());
            }
        }


        [HttpPost]
        public ActionResult Xoa(string userName, USER user)
        {
            try
            {
                using (OnlineMusicDB dbModel = new OnlineMusicDB())
                {
                    user = dbModel.USERs.Where(x => x.UserName == userName).FirstOrDefault();
                    dbModel.Entry(user).State = EntityState.Modified;
                    dbModel.USERs.Remove(user);
                    dbModel.SaveChanges();

                }
                return RedirectToAction("Danhsach");
            }
            catch
            {
                return View();
            }
        }
        public void Export()
        {
            StringWriter strw = new StringWriter();
            strw.WriteLine("\"ID\",\"UserName\",\"Password\",\"Name\",\"Address\",\"Email\",\"Phone\",\"Status\"");
            Response.ClearContent();
            Response.AddHeader("content-disposition"
                                , string.Format("attachment;filename=NewsTest_{0}.csv", DateTime.Now));
            Response.ContentType= "text/csv";
            using (OnlineMusicDB dbModel = new OnlineMusicDB())
            {
                var list = dbModel.USERs.OrderBy(x => x.ID);
                foreach(var news in list){
                    strw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\"",
                        news.ID, news.UserName,news.Password,news.Name,news.Address,news.Email,news.Phone,news.Status));
                }
            }
            Response.Write(strw.ToString());
            Response.End();
        }
    }
}