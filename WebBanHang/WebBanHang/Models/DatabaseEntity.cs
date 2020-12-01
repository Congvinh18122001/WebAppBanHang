using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHang.Models
{
    public class DatabaseEntity
    {
        private DBBanHangEntities1 DB;
        public DBBanHangEntities1 GetData()
        {
            if (DB == null)
            {
                return new DBBanHangEntities1();
            }
            return DB;
        }
    }
}