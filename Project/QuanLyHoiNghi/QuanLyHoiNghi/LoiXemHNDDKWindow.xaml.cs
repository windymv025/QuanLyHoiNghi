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
    /// Interaction logic for LoiXemHNDDKWindow.xaml
    /// </summary>
    public partial class LoiXemHNDDKWindow : Window
    {
        public LoiXemHNDDKWindow()
        {
            InitializeComponent();
        }

        private void trangChuBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Main = new MainWindow();
            Main.Show();
            this.Close();
        }

        private void quanLyHoiNghiBtn_Click(object sender, RoutedEventArgs e)
        { 
            LoiXemHNQLWindow LoiHNQL = new LoiXemHNQLWindow();
            LoiHNQL.Show();
            this.Close();
        }
    }
}
