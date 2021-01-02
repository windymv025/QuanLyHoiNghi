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
    /// Interaction logic for CapQuyenHoiNghiWindow.xaml
    /// </summary>
    public partial class CapQuyenHoiNghiWindow : Window
    {
        private CapQuyenHoiNghiViewModel viewModel;

        public CapQuyenHoiNghiWindow(HOINGHI HoiNghi, String type)
        {
            InitializeComponent();
            CapQuyenHoiNghiViewModel viewModel = new CapQuyenHoiNghiViewModel(HoiNghi, type);
            this.viewModel = viewModel;
            this.DataContext = this.viewModel;
        }
    }
}
