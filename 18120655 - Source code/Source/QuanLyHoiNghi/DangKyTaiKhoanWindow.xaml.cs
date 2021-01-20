using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Security.Cryptography;

using QuanLyHoiNghi.ViewModels;

namespace QuanLyHoiNghi
{
    /// <summary>
    /// Interaction logic for DangKyTaiKhoanWindow.xaml
    /// </summary>
    public partial class DangKyTaiKhoanWindow : Window
    {
        DangKyTaiKhoanViewModel viewModel;
        MD5 md5;
        public DangKyTaiKhoanWindow()
        {
            InitializeComponent();
            viewModel = new DangKyTaiKhoanViewModel();
            md5 = MD5.Create();

        }

        private void dangKyBT_Click(object sender, RoutedEventArgs e)
        {
            if(tenTB.Text == "" || tenDangNhapTB.Text == "" || matKhauTB.Password == "" ||
                nhapLaiMKTB.Password == "" || emailTB.Text == "" || (taiKhoanThuongRB.IsChecked == false && taiKhoanAdminRB.IsChecked == false))
            {
                if(loiTB.Visibility == Visibility.Hidden)
                {
                    loiTB.Visibility = Visibility.Visible;
                    loiTB.Text = "Bạn cần nhập đủ thông tin.";

                }
                else
                {
                    loiTB.Text = "Bạn cần nhập đủ thông tin.";
                }
                return;
            }

            if(matKhauTB.Password != nhapLaiMKTB.Password)
            {
                if (loiTB.Visibility == Visibility.Hidden)
                {
                    loiTB.Visibility = Visibility.Visible;
                    loiTB.Text = "Nhập lại mật khẩu không khớp.";

                }
                else
                {
                    loiTB.Text = "Nhập lại mật khẩu không khớp.";
                }
                return;
            }

            if(emailTB.Text.ToString().IndexOf("@") < 0)
            {
                if (loiTB.Visibility == Visibility.Hidden)
                {
                    loiTB.Visibility = Visibility.Visible;
                    loiTB.Text = "Email không hợp lệ.";
                }
                else
                {
                    loiTB.Text = "Email không hợp lệ.";
                }
                return;
            }

            List<string> username = viewModel.layTatCaUserName();
            for (int i = 0; i < username.Count; i++)
            {
                if (String.Compare(username[i], tenDangNhapTB.Text, true) == 0)
                {
                    if (loiTB.Visibility == Visibility.Hidden)
                    {
                        loiTB.Visibility = Visibility.Visible;
                        loiTB.Text = "Tên đăng nhập đã tồn tại.";
                    }
                    else
                    {
                        loiTB.Text = "Tên đăng nhập đã tồn tại.";
                    }

                    return;
                }
            }

           
            byte[] pass = System.Text.Encoding.ASCII.GetBytes(matKhauTB.Password);
            byte[] hash = md5.ComputeHash(pass);
            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            string matKhau = sb.ToString();

            string loaiTk;
            if (taiKhoanThuongRB.IsChecked == true)
            {
                loaiTk = "0";
            }
            else
                loaiTk = "1";

            viewModel.themUser(tenTB.Text, tenDangNhapTB.Text, matKhau, emailTB.Text, loaiTk);

            DangNhapWindow DN = new DangNhapWindow();
            DN.Show();
            this.Close();
            return;
        }

        private void trangChuBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Main = new MainWindow();
            Main.Show();
            this.Close();
        }

        private void hoiNghiDangKyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DangNhapViewModel.User != null)
            {
                HoiNghiDaDangKyWindow HNDDangKy = new HoiNghiDaDangKyWindow();
                HNDDangKy.Show();
                this.Close();
            }
            else
            {
                LoiXemHNDDKWindow LoiHNDDangKy = new LoiXemHNDDKWindow();
                LoiHNDDangKy.Show();
                this.Close();
            }
        }

        private void quanLyHoiNghiBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DangNhapViewModel.User != null && DangNhapViewModel.User.LOAIUSER == "1")
            {
                QuanLyHoiNghiWindow HNQLy = new QuanLyHoiNghiWindow();
                HNQLy.Show();
                this.Close();
            }
            else
            {
                LoiXemHNQLWindow LoiHNQL = new LoiXemHNQLWindow();
                LoiHNQL.Show();
                this.Close();
            }
        }

        private void taiKhoanBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DangNhapViewModel.User == null)
            {
                DangNhapWindow DN = new DangNhapWindow();
                DN.Show();
                this.Close();
            }
            else
            {
                XemVaSuaTaiKhoanWindow TK = new XemVaSuaTaiKhoanWindow();
                TK.Show();
                this.Close();
            }
        }

        private void dangNhapBT_Click(object sender, RoutedEventArgs e)
        {
            DangNhapWindow DN = new DangNhapWindow();
            DN.Show();
            this.Close();
        }

        
    }
}
