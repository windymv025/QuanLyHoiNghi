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
                var hasuser = from us in db.USERs
                        where us.USERNAME == username
                        select us;

                if (hasuser.Count() <= 0)
                    return false;

                user = hasuser.First();

                if (user.PASSWORD != password)
                    return false;

                User = user;
                User.HINHANH = System.IO.Path.GetFullPath(User.HINHANH);
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
            string hashPass = this.maHoaMatKhauMD5(pass);
            User.PASSWORD = hashPass;
            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {
                foreach (var us in db.USERs)
                {
                    if (us.IDUSER == User.IDUSER)
                    {
                        us.PASSWORD = hashPass;
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
        private string maHoaMatKhauMD5(string password)
        {
            //Tạo MD5 
            MD5 mh = MD5.Create();
            //Chuyển kiểu chuổi thành kiểu byte
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
            //mã hóa chuỗi đã chuyển
            byte[] hash = mh.ComputeHash(inputBytes);
            //tạo đối tượng StringBuilder (làm việc với kiểu dữ liệu lớn)
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
