﻿using Microsoft.Win32;
using QuanLyHoiNghi.Model;
using QuanLyHoiNghi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ChinhSuaHoiNghiWindow.xaml
    /// </summary>
    public partial class ChinhSuaHoiNghiWindow : Window
    {
        private ChinhSuaHoiNghiViewModel viewModel;

        public ChinhSuaHoiNghiWindow(HOINGHI HoiNghi)
        {
            InitializeComponent();
            ChinhSuaHoiNghiViewModel viewModel = new ChinhSuaHoiNghiViewModel(HoiNghi, this);
            this.viewModel = viewModel;
            this.DataContext = this.viewModel;
        }

        private void btnTrangChu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Main = new MainWindow();
            Main.Show();
            this.Close();
        }

        private void btnHoiNghiDangKy_Click(object sender, RoutedEventArgs e)
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

        private void btnQuanLyHoiNghi_Click(object sender, RoutedEventArgs e)
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

        private void btnTaiKhoan_Click(object sender, RoutedEventArgs e)
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


        private void btnDanhSachDangKy_Click(object sender, RoutedEventArgs e)
        {
            DanhSachDangKyHoiNghi dangKyHoiNghi = new DanhSachDangKyHoiNghi(viewModel.HoiNghi);
            dangKyHoiNghi.Show();
            this.Close();
        }

        private void soLuongTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            soLuongTxt.Text = Regex.Replace(soLuongTxt.Text, "[^0-9]+", String.Empty);
        }
    }
}
