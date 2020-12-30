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
            //lvDanhSachHoiNghi.ItemsSource = viewModel.ListHoiNghi;
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
            }
        }

        private void tenHoiNghiTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tenHoiNghiTextBox.Text.Trim() != "")
            {
                //MessageBox.Show("Đang tìm");
            }
            else
            {
                //MessageBox.Show("Hết tìm");
            }    
        }

        private void hoiNghiDangKyBtn_Click(object sender, RoutedEventArgs e)
        {
            LoiXemHNDDKWindow LoiHNDDangKy = new LoiXemHNDDKWindow();
            LoiHNDDangKy.Show();
            this.Close();
        }

        private void quanLyHoiNghiBtn_Click(object sender, RoutedEventArgs e)
        {
            LoiXemHNQLWindow LoiHNQL = new LoiXemHNQLWindow();
            LoiHNQL.Show();
            this.Close();
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
    }
}
