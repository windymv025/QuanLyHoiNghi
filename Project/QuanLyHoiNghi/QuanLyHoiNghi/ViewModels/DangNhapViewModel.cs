using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        public void chinhSuaHoTen(string newName)
        {
            User.TENUSER = newName;
            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {
                foreach(var us in db.USERs)
                {
                    if(us.IDUSER == User.IDUSER)
                    {
                        us.TENUSER = newName;
                        break;
                    }    
                }
                db.SaveChanges();
            }
        }
        public void chinhSuaMatKhau(string pass)
        {
            User.PASSWORD = pass;
            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {
                foreach (var us in db.USERs)
                {
                    if (us.IDUSER == User.IDUSER)
                    {
                        us.PASSWORD = pass;
                        break;
                    }
                }
                db.SaveChanges();
            }
        }
        public void chinhSuaEmailUser(string newEmail)
        {
            User.EMAIL = newEmail;
            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {
                foreach (var us in db.USERs)
                {
                    if (us.IDUSER == User.IDUSER)
                    {
                        us.EMAIL = newEmail;
                        break;
                    }
                }
                db.SaveChanges();
            }
        }
        public void chinhSuaHinhAnh(string newPathImage)
        {
            User.HINHANH = newPathImage;
            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {
                foreach (var us in db.USERs)
                {
                    if (us.IDUSER == User.IDUSER)
                    {
                        us.HINHANH = newPathImage;
                        break;
                    }
                }
                db.SaveChanges();
            }
        }
    }
}
