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
    /// Interaction logic for HoiNghiDaDangKyWindow.xaml
    /// </summary>
    public partial class HoiNghiDaDangKyWindow : Window
    {
        public HoiNghiDaDangKyWindow()
        {
            InitializeComponent();
        }

        private void trangChuBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Main = new MainWindow();
            Main.Show();
            this.Close();
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
            }
        }
    }
}
