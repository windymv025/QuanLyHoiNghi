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
using System.Windows.Navigation;
using System.Windows.Shapes;

using QuanLyHoiNghi.Model;

namespace QuanLyHoiNghi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HomeViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new HomeViewModel();
            viewModel.ListHoiNghi = HoiNghiDAO.GetAllHoiNghi();
            int total = viewModel.ListHoiNghi.Count;
            viewModel.PagingInfo = new PagingInfo(4, total);
            loadPageHoiNghi(1);
        }

        private void loadPageHoiNghi(int page)
        {
            var result = viewModel.loadPage(page, viewModel.PagingInfo.ItemInPerPage);
            lvDanhSachHoiNghi.ItemsSource = result;
            if (viewModel.PagingInfo.TotalPage == 0) 
            {
                viewModel.PagingInfo.CurrentPage = 0;
            }
            trangHienTaiTxt.Text = $"{viewModel.PagingInfo.CurrentPage}/{viewModel.PagingInfo.TotalPage}";
        }

        private void timKiemBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(sapXep_grid.Visibility==Visibility.Visible)
            {
                sapXep_grid.Visibility = Visibility.Collapsed;
                timKiem_grid.Visibility = Visibility.Visible;
            }
            else
            {
                sapXep_grid.Visibility = Visibility.Visible;
                timKiem_grid.Visibility = Visibility.Collapsed;

                tenHoiNghiTextBox.Text = "";
                diadiemTextBox.Text = "";
                
                viewModel.ListHoiNghi = HoiNghiDAO.GetAllHoiNghi();
                viewModel.PagingInfo = new PagingInfo(4, viewModel.ListHoiNghi.Count);
                loadPageHoiNghi(1);
            }
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

        private void nextBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            loadPageHoiNghi(viewModel.PagingInfo.CurrentPage + 1);
        }

        private void prevBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            loadPageHoiNghi(viewModel.PagingInfo.CurrentPage - 1);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(loaiSapXep.SelectedIndex == 0)
            {
                if (hinhThucSapXepCB.SelectedIndex == 0)
                {
                    viewModel.sapXepHoiNghiTangDanTheoTen();
                    loadPageHoiNghi(1);
                }
                else
                {
                    viewModel.sapXepHoiNghiTangDanTheoThoiGian();
                    loadPageHoiNghi(1);
                }
            }
            else
            {
                if (hinhThucSapXepCB.SelectedIndex == 0)
                {
                    viewModel.sapXepHoiNghiGiamDanTheoTen();
                    loadPageHoiNghi(1);
                }
                else
                {
                    viewModel.sapXepHoiNghiGiamDanTheoThoiGian();
                    loadPageHoiNghi(1);
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
                viewModel.ListHoiNghi = HoiNghiDAO.GetResultSearchHoiNghi(name, place, date);
                viewModel.PagingInfo = new PagingInfo(4, viewModel.ListHoiNghi.Count);
                loadPageHoiNghi(1);
            }
            else
            {
                viewModel.ListHoiNghi = HoiNghiDAO.GetAllHoiNghi();
                viewModel.PagingInfo = new PagingInfo(4, viewModel.ListHoiNghi.Count);
                loadPageHoiNghi(1);
            }
        }

        private void diaiemTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string name = tenHoiNghiTextBox.Text.Trim();
            string place = diadiemTextBox.Text.Trim();
            DateTime date = ngayBatDauDatePicker.SelectedDate.GetValueOrDefault();
            DateTime defaultDate = new DateTime();
            if (name != "" || place != "" || date != defaultDate)
            {
                viewModel.ListHoiNghi = HoiNghiDAO.GetResultSearchHoiNghi(name, place, date);
                viewModel.PagingInfo = new PagingInfo(4, viewModel.ListHoiNghi.Count);
                loadPageHoiNghi(1);
            }
            else
            {
                viewModel.ListHoiNghi = HoiNghiDAO.GetAllHoiNghi();
                viewModel.PagingInfo = new PagingInfo(4, viewModel.ListHoiNghi.Count);
                loadPageHoiNghi(1);
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
                viewModel.ListHoiNghi = HoiNghiDAO.GetResultSearchHoiNghi(name, place, date);
                viewModel.PagingInfo = new PagingInfo(4, viewModel.ListHoiNghi.Count);
                loadPageHoiNghi(1);
            }
            else
            {
                viewModel.ListHoiNghi = HoiNghiDAO.GetAllHoiNghi();
                viewModel.PagingInfo = new PagingInfo(4, viewModel.ListHoiNghi.Count);
                loadPageHoiNghi(1);
            }
        }

        private void ListViewItem_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ListViewItem item = (ListViewItem)sender;
            item.IsSelected = true;
        }

        private void ListViewItem_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ListViewItem item = (ListViewItem)sender;
            item.IsSelected = false;
        }

        private void xemThemBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = 0;
                index = lvDanhSachHoiNghi.SelectedIndex;

                HOINGHI hn = viewModel.ListHoiNghi[((viewModel.PagingInfo.CurrentPage - 1) * 4) + index];
                ChiTietHoiNghiWindow CTHN = new ChiTietHoiNghiWindow(hn);
                CTHN.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lvDanhSachHoiNghi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
