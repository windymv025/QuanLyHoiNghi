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
    /// Interaction logic for XemVaSuaTaiKhoanWindow.xaml
    /// </summary>
    public partial class XemVaSuaTaiKhoanWindow : Window
    {
        DangNhapViewModel viewModel = new DangNhapViewModel();
        public XemVaSuaTaiKhoanWindow()
        {
            InitializeComponent();
            if(DangNhapViewModel.User != null)
            {
                String stringPath = DangNhapViewModel.User.HINHANH;
                Uri imageUri = new Uri(stringPath, UriKind.RelativeOrAbsolute);
                BitmapImage imageBitmap = new BitmapImage(imageUri);
                
                hinhAnh.Source = imageBitmap;
               
                tenTB.Text = DangNhapViewModel.User.TENUSER;
                tenDangNhapTB.Text = DangNhapViewModel.User.USERNAME;
                matKhauTB.Password = DangNhapViewModel.User.PASSWORD;
                emailTB.Text = DangNhapViewModel.User.EMAIL;
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

        private void suaTen_Click(object sender, RoutedEventArgs e)
        {
            if (tenTB.IsEnabled)
            {
                suaTenBtn.Content = "Sửa";
                tenTB.IsEnabled = false;
                viewModel.chinhSuaHoTen(tenTB.Text.Trim());
            }
            else
            {
                suaTenBtn.Content = "Lưu";
                tenTB.IsEnabled = true;
            }
        }

        private void suaMatKhauBtn_Click(object sender, RoutedEventArgs e)
        {
            if (matKhauTB.IsEnabled)
            {
                suaMatKhauBtn.Content = "Sửa";
                matKhauTB.IsEnabled = false;
            }
            else
            {
                suaMatKhauBtn.Content = "Lưu";
                matKhauTB.IsEnabled = true;
            }
        }

        private void suaEmailBtn_Click(object sender, RoutedEventArgs e)
        {
            if (emailTB.IsEnabled)
            {
                suaEmailBtn.Content = "Sửa";
                emailTB.IsEnabled = false;
            }
            else
            {
                suaEmailBtn.Content = "Lưu";
                emailTB.IsEnabled = true;
            }
        }

        private void suaHinhAnhBtn_Click(object sender, RoutedEventArgs e)
        {
            if(suaHinhAnhBtn.Content.ToString().Equals("Sửa"))
            {
                suaHinhAnhBtn.Content = "Lưu";
            }    
            else
            {
                suaHinhAnhBtn.Content = "Sửa";
            }    
        }
    }
}
