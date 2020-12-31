using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QuanLyHoiNghi.Model;

namespace QuanLyHoiNghi.ViewModels
{
    class DangNhapViewModel
    {
        public static USER User = null;

        public bool kiemTraUser(string username, string password)
        {
            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {
                USER user = null;
                user = (from us in db.USERs
                        where us.USERNAME == username
                        select us).First();
                if (user == null)
                    return false;

                if (user.PASSWORD != password)
                    return false;

                User = user;
                return true;
            }
        }
    }
}
