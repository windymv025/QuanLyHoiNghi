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
    /// Interaction logic for DangNhapWindow.xaml
    /// </summary>
    public partial class DangNhapWindow : Window
    {
        DangNhapViewModel viewModel;
        MD5 md5;
        public DangNhapWindow()
        {
            InitializeComponent();
            viewModel = new DangNhapViewModel();
            md5 = MD5.Create();
        }

        private void dangNhapBT_Click(object sender, RoutedEventArgs e)
        {
            if(tenDangNhapTB.Text == "" || matKhauTB.Password == "")
            {
                if (loiTB.Visibility == Visibility.Hidden)
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

            byte[] pass = System.Text.Encoding.ASCII.GetBytes(matKhauTB.Password);
            byte[] hash = md5.ComputeHash(pass);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            string matKhau = sb.ToString();

            bool result = viewModel.kiemTraUser(tenDangNhapTB.Text, matKhau);
            if(result == false)
            {
                if (loiTB.Visibility == Visibility.Hidden)
                {
                    loiTB.Visibility = Visibility.Visible;
                    loiTB.Text = "Đăng nhập không thành công.";

                }
                else
                {
                    loiTB.Text = "Đăng nhập không thành công.";
                }
                return;
            }
            else
            {
                if (loiTB.Visibility == Visibility.Visible)
                {
                    loiTB.Visibility = Visibility.Hidden;
                }
                return;
            } 
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

        private void dangKyBT_Click(object sender, RoutedEventArgs e)
        {
            DangKyTaiKhoanWindow DK = new DangKyTaiKhoanWindow();
            DK.Show();
            this.Close();

        }
    }
}
