using QuanLyHoiNghi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyHoiNghi.ViewModels
{
    class DangKyTaiKhoanViewModel
    {
        public void themUser(string ten, string tenDangNhap, string matKhau, string email, string loaiUser)
        {
            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {
                var list = from us in db.USERs
                           orderby us.IDUSER descending
                           select us;
                int idMax = list.ToList().First().IDUSER;

                USER user = new USER();
                user.IDUSER = idMax + 1;
                user.TENUSER = ten;
                user.USERNAME = tenDangNhap;
                user.PASSWORD = matKhau;
                user.EMAIL = email;
                user.LOAIUSER = loaiUser;
                user.HINHANH = "Images/user_example.png";

                db.USERs.Add(user);

                db.SaveChanges();

            }
        }

        public List<string> layTatCaUserName()
        {
            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {
                var list = from user in db.USERs
                           select user.USERNAME;
                return list.ToList();
            }
        }
    }
}
