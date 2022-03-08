using OnlineMusic.EF;
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
        public IEnumerable<SANPHAM> ListAllPaging(int page, int pageSize)
        {
            IQueryable<SANPHAM> model = db.SANPHAMs.OrderBy(x => x.ID);

            /* return db.SANPHAMs.OrderBy(x => x.ID).ToPagedList(page, pageSize);*/
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
    }
}