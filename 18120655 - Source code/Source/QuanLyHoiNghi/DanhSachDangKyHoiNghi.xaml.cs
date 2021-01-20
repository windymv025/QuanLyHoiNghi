using QuanLyHoiNghi.Model;
using QuanLyHoiNghi.ViewModels;
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

namespace QuanLyHoiNghi
{
    /// <summary>
    /// Interaction logic for DanhSachDangKyHoiNghi.xaml
    /// </summary>
    public partial class DanhSachDangKyHoiNghi : Window
    {
        const int NUMBER_USER_PER_PAGE = 4;
        DanhSachDangKyHoiNghiViewModel viewModel;
        public DanhSachDangKyHoiNghi(HOINGHI hoiNghi)
        {
            InitializeComponent();
            viewModel = new DanhSachDangKyHoiNghiViewModel(hoiNghi);
            int total = viewModel.ListDangKyUser.Count;
            viewModel.PagingInfo = new PagingInfo(NUMBER_USER_PER_PAGE, total);
            loadPage(1);
        }

        private void loadPage(int page)
        {
            var result = viewModel.loadPage(page, viewModel.PagingInfo.ItemInPerPage);
            lvDanhSachDangKy.ItemsSource = result;
            if (viewModel.PagingInfo.TotalPage == 0)
            {
                viewModel.PagingInfo.CurrentPage = 0;
            }
            trangHienTaiTxt.Text = $"{viewModel.PagingInfo.CurrentPage}/{viewModel.PagingInfo.TotalPage}";
        }

        private void btnTrangChu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Main = new MainWindow();
            Main.Show();
            this.Close();
        }

        private void btnHoiNghiDangKY_Click(object sender, RoutedEventArgs e)
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

        private void btnQuanLyHoiNghi_Click(object sender, RoutedEventArgs e)
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

        private void btnTaiKhoan_Click(object sender, RoutedEventArgs e)
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

        private void prevBtn_Click(object sender, RoutedEventArgs e)
        {
            loadPage(viewModel.PagingInfo.CurrentPage - 1);
        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            loadPage(viewModel.PagingInfo.CurrentPage + 1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            loadPage(viewModel.PagingInfo.CurrentPage);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loaiSapXep.SelectedIndex == 0)
            {
                if (hinhThucSapXepCB.SelectedIndex == 0)
                {
                    viewModel.sapXepUserTangDanTheoTen();
                    loadPage(1);
                }
                else
                {
                    viewModel.sapXepUserTangDanTheoEmail();
                    loadPage(1);
                }
            }
            else
            {
                if (hinhThucSapXepCB.SelectedIndex == 0)
                {
                    viewModel.sapXepUserGiamDanTheoTen();
                    loadPage(1);
                }
                else
                {
                    viewModel.sapXepUserGiamDanTheoEmail();
                    loadPage(1);
                }
            }
        }

        private void timKiemBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sapXep_grid.Visibility == Visibility.Visible)
            {
                sapXep_grid.Visibility = Visibility.Collapsed;
                timKiem_grid.Visibility = Visibility.Visible;
            }
            else
            {
                sapXep_grid.Visibility = Visibility.Visible;
                timKiem_grid.Visibility = Visibility.Collapsed;

                tenNguoiDungTextBox.Text = "";
                emailTextBox.Text = "";
            }
        }

        private void emailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string email = emailTextBox.Text.Trim();
            string name = tenNguoiDungTextBox.Text.Trim();
            if (email != "" || name != "") 
            {
                viewModel.LoadDataByNameAndEmail(name, email);
                int total = viewModel.ListDangKyUser.Count;
                viewModel.PagingInfo = new PagingInfo(NUMBER_USER_PER_PAGE, total);
                loadPage(1);
            }
            else
            {
                viewModel.LoadData();
                int total = viewModel.ListDangKyUser.Count;
                viewModel.PagingInfo = new PagingInfo(NUMBER_USER_PER_PAGE, total);
                loadPage(1);
            }
        }

        private void tenNguoiDungTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string email = emailTextBox.Text.Trim();
            string name = tenNguoiDungTextBox.Text.Trim();
            if (email != "" || name != "")
            {
                viewModel.LoadDataByNameAndEmail(name, email);
                int total = viewModel.ListDangKyUser.Count;
                viewModel.PagingInfo = new PagingInfo(NUMBER_USER_PER_PAGE, total);
                loadPage(1);
            }
            else
            {
                viewModel.LoadData();
                int total = viewModel.ListDangKyUser.Count;
                viewModel.PagingInfo = new PagingInfo(NUMBER_USER_PER_PAGE, total);
                loadPage(1);
            }
        }
    }
}
