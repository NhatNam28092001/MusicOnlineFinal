using OnlineMusic.EF;
using OnlineMusic.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineMusic.DAO
{
    public class PRODUCT_DAO
    {
        OnlineMusicDB db = null;
        public PRODUCT_DAO()
        {
            db = new OnlineMusicDB();
        }
        public List<SANPHAM> ListAll()
        {
            return db.SANPHAMs.Where(x => x.Status == true).OrderBy(x => x.ID).ToList();
        }
        public bool Insert(SANPHAM entity)
        {
            db.SANPHAMs.Add(entity);
            db.SaveChanges();
            return true;
        }
        public IEnumerable<SANPHAM> ListAllPaging(string searching, int page, int pageSize)
        {
            IQueryable<SANPHAM> model = db.SANPHAMs.OrderByDescending(x => x.ID);
            if (!string.IsNullOrEmpty(searching))
            {
                model = model.Where(x => x.Name.StartsWith(searching) || x.Name.Contains(searching));
            }
            return db.SANPHAMs.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }
        public List<SANPHAM> ListSanPham(int id)
        {
            
            return db.SANPHAMs.OrderBy(x => x.ID).ToList();
        }
        public List<SANPHAM> ListRelatedSanPham(long productid)
        {
            var product = db.SANPHAMs.Find(productid);

            return db.SANPHAMs.Where(x => x.ID != productid && x.CategoryID == product.CategoryID).ToList();
        }
        public SANPHAM ViewDetail(long id)
        {
            return db.SANPHAMs.Find(id);
        }
        public List<string> ListName(string keyword)
        {
            return db.SANPHAMs.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }
        /*public List<ProductModel> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.Name == keyword).Count();
            var model = (from a in db.Products
                         join b in db.ProductCategories
                         on a.CategoryID equals b.ID
                         where a.Name.Contains(keyword)
                         select new
                         {
                             CateMetaTitle = b.MetaTitle,
                             CateName = b.Name,
                             CreatedDate = a.CreatedDate,
                             ID = a.ID,
                             Images = a.Image,
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Price = a.Price
                         }).AsEnumerable().Select(x => new ProductViewModel()
                         {
                             CateMetaTitle = x.MetaTitle,
                             CateName = x.Name,
                             CreatedDate = x.CreatedDate,
                             ID = x.ID,
                             Images = x.Images,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price
                         });
            model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();*/

    }
}