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
        public IEnumerable<SLIDE> ListAllPaging(int page, int pageSize)
        {
            return db.SLIDEs.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }
    }
}