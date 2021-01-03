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

using QuanLyHoiNghi.Model;

namespace QuanLyHoiNghi
{
    /// <summary>
    /// Interaction logic for HoiNghiDaDangKyWindow.xaml
    /// </summary>
    public partial class HoiNghiDaDangKyWindow : Window
    {
        HoiNghiDaDangKyViewModel viewModel;
        public HoiNghiDaDangKyWindow()
        {
            InitializeComponent();
            viewModel = new HoiNghiDaDangKyViewModel();
            viewModel.ListHoiNghi = HoiNghiDAO.GetAllHoiNghiDaDangKy();
            int total = viewModel.ListHoiNghi.Count;
            viewModel.PagingInfo = new PagingInfoOfHNDDK(4, total);

            loadPageHoiNghiDaDangKy(1);
        }

        private void loadPageHoiNghiDaDangKy(int page)
        {
            var result = viewModel.loadPage(page, viewModel.PagingInfo.ItemInPerPage);
            lvDanhSachHoiNghi.ItemsSource = result;
            if (viewModel.PagingInfo.TotalPage == 0)
            {
                viewModel.PagingInfo.CurrentPage = 0;
            }
            trangHienTaiTxt.Text = $"{viewModel.PagingInfo.CurrentPage}/{viewModel.PagingInfo.TotalPage}";
        }
        private void trangChuBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Main = new MainWindow();
            Main.Show();
            this.Close();
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
        private void timKiemBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (timKiem_grid.Visibility == Visibility.Collapsed)
            {
                timKiem_grid.Visibility = Visibility.Visible;
                sapXep_grid.Visibility = Visibility.Collapsed;
            }
            else
            {
                timKiem_grid.Visibility = Visibility.Collapsed;
                sapXep_grid.Visibility = Visibility.Visible;

                tenHoiNghiTextBox.Text = "";
                diadiemTextBox.Text = "";

                viewModel.ListHoiNghi = HoiNghiDAO.GetAllHoiNghiDaDangKy();
                viewModel.PagingInfo = new PagingInfoOfHNDDK(4, viewModel.ListHoiNghi.Count);
                loadPageHoiNghiDaDangKy(1);
            }
        }

        private void prevBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            loadPageHoiNghiDaDangKy(viewModel.PagingInfo.CurrentPage - 1);
        }

        private void nextBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            loadPageHoiNghiDaDangKy(viewModel.PagingInfo.CurrentPage + 1);
        }

        private void loaiSapXepCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loaiSapXepCB.SelectedIndex == 0)
            {
                if (hinhThucSapXepCB.SelectedIndex == 0)
                {
                    viewModel.sapXepHoiNghiTangDanTheoTen();
                    loadPageHoiNghiDaDangKy(1);
                }
                else
                {
                    viewModel.sapXepHoiNghiTangDanTheoThoiGian();
                    loadPageHoiNghiDaDangKy(1);
                }
            }
            else
            {
                if (hinhThucSapXepCB.SelectedIndex == 0)
                {
                    viewModel.sapXepHoiNghiGiamDanTheoTen();
                    loadPageHoiNghiDaDangKy(1);
                }
                else
                {
                    viewModel.sapXepHoiNghiGiamDanTheoThoiGian();
                    loadPageHoiNghiDaDangKy(1);
                }
            }
        }

        private void tenHoiNghiTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string name = tenHoiNghiTextBox.Text.Trim();
            string place = diadiemTextBox.Text.Trim();
            DateTime date = ngayBatDauDatePicker.SelectedDate.GetValueOrDefault();
            DateTime defaultDate = new DateTime();

            if (name != "" || place != "" || date != defaultDate)
            {
                viewModel.ListHoiNghi = HoiNghiDAO.GetResultSearchHoiNghiDaDangKy(name, place, date);
                viewModel.PagingInfo = new PagingInfoOfHNDDK(4, viewModel.ListHoiNghi.Count);
                loadPageHoiNghiDaDangKy(1);
            }
            else
            {
                viewModel.ListHoiNghi = HoiNghiDAO.GetAllHoiNghiDaDangKy();
                viewModel.PagingInfo = new PagingInfoOfHNDDK(4, viewModel.ListHoiNghi.Count);
                loadPageHoiNghiDaDangKy(1);
            }
        }

        private void diadiemTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string name = tenHoiNghiTextBox.Text.Trim();
            string place = diadiemTextBox.Text.Trim();
            DateTime date = ngayBatDauDatePicker.SelectedDate.GetValueOrDefault();
            DateTime defaultDate = new DateTime();

            if (name != "" || place != "" || date != defaultDate)
            {
                viewModel.ListHoiNghi = HoiNghiDAO.GetResultSearchHoiNghiDaDangKy(name, place, date);
                viewModel.PagingInfo = new PagingInfoOfHNDDK(4, viewModel.ListHoiNghi.Count);
                loadPageHoiNghiDaDangKy(1);
            }
            else
            {
                viewModel.ListHoiNghi = HoiNghiDAO.GetAllHoiNghiDaDangKy();
                viewModel.PagingInfo = new PagingInfoOfHNDDK(4, viewModel.ListHoiNghi.Count);
                loadPageHoiNghiDaDangKy(1);
            }
        }

        private void ngayBatDauDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            string name = tenHoiNghiTextBox.Text.Trim();
            string place = diadiemTextBox.Text.Trim();
            DateTime date = ngayBatDauDatePicker.SelectedDate.GetValueOrDefault();
            DateTime defaultDate = new DateTime();

            if (name != "" || place != "" || date != defaultDate)
            {
                viewModel.ListHoiNghi = HoiNghiDAO.GetResultSearchHoiNghiDaDangKy(name, place, date);
                viewModel.PagingInfo = new PagingInfoOfHNDDK(4, viewModel.ListHoiNghi.Count);
                loadPageHoiNghiDaDangKy(1);
            }
            else
            {
                viewModel.ListHoiNghi = HoiNghiDAO.GetAllHoiNghiDaDangKy();
                viewModel.PagingInfo = new PagingInfoOfHNDDK(4, viewModel.ListHoiNghi.Count);
                loadPageHoiNghiDaDangKy(1);
            }
        }


        private void xemThemBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = 0;
                index = lvDanhSachHoiNghi.SelectedIndex;

                HOINGHI hn = viewModel.ListHoiNghi[((viewModel.PagingInfo.CurrentPage -1) * 4) + index];
                ChiTietHoiNghiWindow CTHN = new ChiTietHoiNghiWindow(hn, null);
                CTHN.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ListViewItem_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ListViewItem item = (ListViewItem)sender;
            item.IsSelected = false;
        }

        private void ListViewItem_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ListViewItem item = (ListViewItem)sender;
            item.IsSelected = true;
        }
    }
}
