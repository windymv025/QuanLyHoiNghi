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
    /// Interaction logic for ThemHoiNghiWindow.xaml
    /// </summary>
    public partial class ThemHoiNghiWindow : Window
    {
        private ThemHoiNghiViewModel viewModel;

        public ThemHoiNghiWindow()
        {
            InitializeComponent();
            ThemHoiNghiViewModel viewModel = new ThemHoiNghiViewModel();
            this.viewModel = viewModel;
            this.DataContext = this.viewModel;
        }

        private void btnTrangChu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnHoiNghiDangKy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnQuanLyHoiNghi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTaiKhoan_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
