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
    /// Interaction logic for ChiTietHoiNghi.xaml
    /// </summary>
    public partial class ChiTietHoiNghiWindow : Window
    {
        private ChiTietHoiNghiViewModel viewModel;

        public ChiTietHoiNghiWindow()
        {
            InitializeComponent();
            ChiTietHoiNghiViewModel viewModel = new ChiTietHoiNghiViewModel(null, null);
            this.viewModel = viewModel;
            this.DataContext = viewModel;
        }

        private void btnTrangChu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnHoiNghiDangKY_Click(object sender, RoutedEventArgs e)
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
