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
    /// Interaction logic for LoiXemHNQLWindow.xaml
    /// </summary>
    public partial class LoiXemHNQLWindow : Window
    {
        public LoiXemHNQLWindow()
        {
            InitializeComponent();
        }

        private void trangChuBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Main = new MainWindow();
            Main.Show();
            this.Close();
        }

        private void hoiNghiDangKyBtn_Click(object sender, RoutedEventArgs e)
        {
            LoiXemHNDDKWindow LoiHNDDangKy = new LoiXemHNDDKWindow();
            LoiHNDDangKy.Show();
            this.Close();
        }
    }
}
