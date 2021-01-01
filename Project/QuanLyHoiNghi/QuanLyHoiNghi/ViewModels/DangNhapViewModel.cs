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
        public  USER User { get; set; }

        public bool kiemTraUser(string username, string password)
        {
            string hashPass = this.maHoaMatKhau(password);
            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {
                USER user = null;
                user = (from us in db.USERs
                        where us.USERNAME == username
                        select us).First();
                if (user == null)
                    return false;

                if (user.PASSWORD != hashPass)
                    return false;

                User = user;
                return true;
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
