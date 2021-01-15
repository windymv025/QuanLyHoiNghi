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
    /// Interaction logic for QuanLyHoiNghiWindow.xaml
    /// </summary>
    public partial class QuanLyHoiNghiWindow : Window
    {
        QuanLyHoiNghiViewModel viewModel;
        public QuanLyHoiNghiWindow()
        {
            InitializeComponent();
            viewModel = new QuanLyHoiNghiViewModel();
            viewModel.ListHoiNghi = QuanLyHoiNghiDAO.GetAllHoiNghiQuanLy();
            int total = viewModel.ListHoiNghi.Count;
            viewModel.PagingInfo = new PagingInfoOfHNQL(4, total);

            loadPageHoiNghiQuanLy(1);
        }
        private void loadPageHoiNghiQuanLy(int page)
        {
            var result = viewModel.loadPage(page, viewModel.PagingInfo.ItemInPerPage);
            lvDanhSachHoiNghiQL.ItemsSource = result;
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

                viewModel.ListHoiNghi = QuanLyHoiNghiDAO.GetAllHoiNghiQuanLy();
                viewModel.PagingInfo = new PagingInfoOfHNQL(4, viewModel.ListHoiNghi.Count);
                loadPageHoiNghiQuanLy(1);
            }
        }
       
        private void prevBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            loadPageHoiNghiQuanLy(viewModel.PagingInfo.CurrentPage - 1);
        }

        private void nextBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            loadPageHoiNghiQuanLy(viewModel.PagingInfo.CurrentPage + 1);
        }

        private void loaiSapXepCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loaiSapXepCB.SelectedIndex == 0)
            {
                if (hinhThucSapXepCB.SelectedIndex == 0)
                {
                    viewModel.sapXepHoiNghiTangDanTheoTen();
                    loadPageHoiNghiQuanLy(1);
                }
                else
                {
                    viewModel.sapXepHoiNghiTangDanTheoThoiGian();
                    loadPageHoiNghiQuanLy(1);
                }
            }
            else
            {
                if (hinhThucSapXepCB.SelectedIndex == 0)
                {
                    viewModel.sapXepHoiNghiGiamDanTheoTen();
                    loadPageHoiNghiQuanLy(1);
                }
                else
                {
                    viewModel.sapXepHoiNghiGiamDanTheoThoiGian();
                    loadPageHoiNghiQuanLy(1);
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
                viewModel.ListHoiNghi = QuanLyHoiNghiDAO.GetResultSearchHoiNghiQuanLy(name, place, date);
                viewModel.PagingInfo = new PagingInfoOfHNQL(4, viewModel.ListHoiNghi.Count);
                loadPageHoiNghiQuanLy(1);
            }
            else
            {
                viewModel.ListHoiNghi = QuanLyHoiNghiDAO.GetAllHoiNghiQuanLy();
                viewModel.PagingInfo = new PagingInfoOfHNQL(4, viewModel.ListHoiNghi.Count);
                loadPageHoiNghiQuanLy(1);
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
                viewModel.ListHoiNghi = QuanLyHoiNghiDAO.GetResultSearchHoiNghiQuanLy(name, place, date);
                viewModel.PagingInfo = new PagingInfoOfHNQL(4, viewModel.ListHoiNghi.Count);
                loadPageHoiNghiQuanLy(1);
            }
            else
            {
                viewModel.ListHoiNghi = QuanLyHoiNghiDAO.GetAllHoiNghiQuanLy();
                viewModel.PagingInfo = new PagingInfoOfHNQL(4, viewModel.ListHoiNghi.Count);
                loadPageHoiNghiQuanLy(1);
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
                viewModel.ListHoiNghi = QuanLyHoiNghiDAO.GetResultSearchHoiNghiQuanLy(name, place, date);
                viewModel.PagingInfo = new PagingInfoOfHNQL(4, viewModel.ListHoiNghi.Count);
                loadPageHoiNghiQuanLy(1);
            }
            else
            {
                viewModel.ListHoiNghi = QuanLyHoiNghiDAO.GetAllHoiNghiQuanLy();
                viewModel.PagingInfo = new PagingInfoOfHNQL(4, viewModel.ListHoiNghi.Count);
                loadPageHoiNghiQuanLy(1);
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

        private void themBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ThemHoiNghiWindow ThemHN = new ThemHoiNghiWindow();
            ThemHN.Show();
            this.Close();
        }

        private void chinhSuaBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = 0;
                index = lvDanhSachHoiNghiQL.SelectedIndex;

                HOINGHI hn = viewModel.ListHoiNghi[((viewModel.PagingInfo.CurrentPage - 1) * 4) + index];
                ChinhSuaHoiNghiWindow CSHN = new ChinhSuaHoiNghiWindow(hn);
                CSHN.Show();
                this.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
