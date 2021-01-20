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
using QuanLyHoiNghi.Model;
using Microsoft.Win32;
using System.IO;

namespace QuanLyHoiNghi
{
    /// <summary>
    /// Interaction logic for XemVaSuaTaiKhoanWindow.xaml
    /// </summary>
    public partial class XemVaSuaTaiKhoanWindow : Window
    {
        DangNhapViewModel viewModel;
        USER user;
        public XemVaSuaTaiKhoanWindow()
        {
            InitializeComponent();
            viewModel = new DangNhapViewModel();
            if (DangNhapViewModel.User != null)
            {
                user = new USER()
                {
                    IDUSER = DangNhapViewModel.User.IDUSER,
                    TENUSER = DangNhapViewModel.User.TENUSER,
                    PASSWORD = DangNhapViewModel.User.PASSWORD,
                    USERNAME = DangNhapViewModel.User.USERNAME,
                    EMAIL = DangNhapViewModel.User.EMAIL,
                    HINHANH = DangNhapViewModel.User.HINHANH,
                    LOAIUSER = DangNhapViewModel.User.LOAIUSER
                };
                this.DataContext = user;
                matKhauTB.Password = DangNhapViewModel.User.PASSWORD;
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
                huySuaTenBtn.Visibility = Visibility.Collapsed;
            }
            else
            {
                suaTenBtn.Content = "Lưu";
                tenTB.IsEnabled = true;
                huySuaTenBtn.Visibility = Visibility.Visible;
            }
        }

        private void suaMatKhauBtn_Click(object sender, RoutedEventArgs e)
        {
            if (matKhauTB.IsEnabled)
            {
                suaMatKhauBtn.Content = "Sửa";
                matKhauTB.IsEnabled = false;
                huySuaMatKhauBtn.Visibility = Visibility.Collapsed;
                viewModel.chinhSuaMatKhau(matKhauTB.Password);
            }
            else
            {
                suaMatKhauBtn.Content = "Lưu";
                matKhauTB.IsEnabled = true;
                huySuaMatKhauBtn.Visibility = Visibility.Visible;
            }
        }

        private void suaEmailBtn_Click(object sender, RoutedEventArgs e)
        {
            if (emailTB.IsEnabled)
            {
                suaEmailBtn.Content = "Sửa";
                emailTB.IsEnabled = false;
                huySuaEmailBtn.Visibility = Visibility.Collapsed;
                viewModel.chinhSuaEmailUser(emailTB.Text.Trim());
            }
            else
            {
                suaEmailBtn.Content = "Lưu";
                emailTB.IsEnabled = true;
                huySuaEmailBtn.Visibility = Visibility.Visible;
            }
        }

        private void suaHinhAnhBtn_Click(object sender, RoutedEventArgs e)
        {
            if(suaHinhAnhBtn.Content.ToString().Equals("Sửa"))
            {
                suaHinhAnhBtn.Content = "Lưu";
                huySuaHinhAnhBtn.Visibility = Visibility.Visible;
                chooseImageBorder.Visibility = Visibility.Visible;
            }
            else
            {
                suaHinhAnhBtn.Content = "Sửa";
                chooseImageBorder.Visibility = Visibility.Collapsed;
                huySuaHinhAnhBtn.Visibility = Visibility.Collapsed;
                saveImage();
            }    
        }

        private void saveImage()
        {
            var currentFolder = AppDomain.CurrentDomain.BaseDirectory.ToString();
            string uriImage = currentFolder.ToString();
            string file = hinhAnh.Source.ToString().Substring(8);


            //Lấy file ảnh copy vào Images của project
            var info = new FileInfo(file);
            var newName = $"{Guid.NewGuid()}{info.Extension}";
            File.Copy(file, $"{uriImage}Images\\{newName}");

            viewModel.chinhSuaHinhAnh($"Images/{newName}");
        }

        private void dangXuatBT_Click(object sender, RoutedEventArgs e)
        {
            DangNhapViewModel.User = null;
            DangNhapWindow DN = new DangNhapWindow();
            DN.Show();
            this.Close();
        }

        private void huySuaTenBtn_Click(object sender, RoutedEventArgs e)
        {
            tenTB.Text = DangNhapViewModel.User.TENUSER;
            suaTenBtn.Content = "Sửa";
            tenTB.IsEnabled = false;
            huySuaTenBtn.Visibility = Visibility.Collapsed;
        }

        private void huySuaMatKhauBtn_Click(object sender, RoutedEventArgs e)
        {
            matKhauTB.Password = DangNhapViewModel.User.PASSWORD;
            suaMatKhauBtn.Content = "Sửa";
            matKhauTB.IsEnabled = false;
            huySuaMatKhauBtn.Visibility = Visibility.Collapsed;
        }

        private void huySuaEmailBtn_Click(object sender, RoutedEventArgs e)
        {
            emailTB.Text = DangNhapViewModel.User.EMAIL;
            suaEmailBtn.Content = "Sửa";
            emailTB.IsEnabled = false;
            huySuaEmailBtn.Visibility = Visibility.Collapsed;
        }

        private void huySuaHinhAnhBtn_Click(object sender, RoutedEventArgs e)
        {
            suaHinhAnhBtn.Content = "Sửa";
            huySuaHinhAnhBtn.Visibility = Visibility.Collapsed;
            chooseImage.Visibility = Visibility.Collapsed;

            String stringPath = DangNhapViewModel.User.HINHANH;
            Uri imageUri = new Uri(stringPath, UriKind.RelativeOrAbsolute);
            BitmapImage imageBitmap = new BitmapImage(imageUri);

            hinhAnh.Source = imageBitmap;
        }

        private void chooseImageBorder_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = false;
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";

            if (open.ShowDialog() == true)
            {
                var img = open.FileNames;
                string uriImage = img[0].ToString();
                ImageSource imgsource = new BitmapImage(new Uri(uriImage));
                hinhAnh.Source = imgsource;
            }
        }
    }
}
