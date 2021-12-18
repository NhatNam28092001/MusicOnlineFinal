using OnlineMusic.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineMusic.DAO
{
    public class BLOG_DAO
    {
        OnlineMusicDB db = null;
        public BLOG_DAO()
        {
            db = new OnlineMusicDB();
        }
        public IEnumerable<NEWS> ListAllPaging(int page, int pageSize)
        {

            return db.NEWS.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }
        public bool Insert(NEWS entity)
        {
            db.NEWS.Add(entity);
            db.SaveChanges();
            return true;
        }
        public List<NEWS> ListAll()
        {
            return db.NEWS.OrderBy(x => x.ID).ToList();
        }
        public NEWS ViewDetail(int id)
        {
            return db.NEWS.Find(id);
        }
    }
}