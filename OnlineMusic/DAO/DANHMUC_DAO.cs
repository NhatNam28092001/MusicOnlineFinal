using OnlineMusic.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineMusic.DAO
{
    public class DANHMUC_DAO
    {
        OnlineMusicDB db = null;
        public DANHMUC_DAO()
        {
            db = new OnlineMusicDB();
        }
        
        public List<DANHMUC> ListAll()
        {
            return db.DANHMUCs.Where(x => x.Status == true).ToList();
        }
        public DANHMUCSANPHAM ViewDetail(long id)
        {
            return db.DANHMUCSANPHAMs.Find(id);
        }
    }
}