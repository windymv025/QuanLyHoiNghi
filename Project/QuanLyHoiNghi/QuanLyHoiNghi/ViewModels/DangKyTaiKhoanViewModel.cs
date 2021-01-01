using QuanLyHoiNghi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
                user.PASSWORD = this.maHoaMatKhau(matKhau);
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
        
        public string maHoaMatKhau(string matKhau)
        {
            //Tạo MD5 
            MD5 mh = MD5.Create();
            //Chuyển kiểu chuổi thành kiểu byte
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(matKhau);
            //mã hóa chuỗi đã chuyển
            byte[] hash = mh.ComputeHash(inputBytes);
            //tạo đối tượng StringBuilder (làm việc với kiểu dữ liệu lớn)
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            //nếu bạn muốn các chữ cái in thường thay vì in hoa thì bạn thay chữ "X" in hoa trong "X2" thành "x"
            return sb.ToString();
        }
    }
}
