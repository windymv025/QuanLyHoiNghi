using Microsoft.Win32;
using QuanLyHoiNghi.Command;
using QuanLyHoiNghi.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyHoiNghi.ViewModels
{
    public class ChinhSuaHoiNghiViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public HOINGHI HoiNghi { get; set; }

        public String TenHoiNghi { get; set; }

        public DateTime NgayBatDau { get; set; }

        public DateTime NgayKetThuc { get; set; }

        public String NoiDungHoiNghi { get; set; }

        public String ImagePathHoiNghi { get; set; }

        public List<DIADIEMTOCHUC> ListDiaDiem { get; set; }

        public List<String> ListDiaDiemString { get; set; }

        public int IndexDiaDiem { get; set; }

        public String SoLuong { get; set; }

        public String MoTa { get; set; }

        public ICommand ChooseImageCommand { get; set; }

        public ICommand SaveHoiNghiCommand { get; set; }

        public ICommand CapQuyenCommand { get; set; }

        public ChinhSuaHoiNghiViewModel(HOINGHI hoiNghi)
        {
            this.HoiNghi = hoiNghi;
            LoadData();

            this.ChooseImageCommand = new RelayCommand(ChooseImage);
            this.SaveHoiNghiCommand = new RelayCommand(SaveHoiNghi);
            this.CapQuyenCommand = new RelayCommand(CapQuyen);
        }

        private void LoadData()
        {
            this.TenHoiNghi = this.HoiNghi.TENHN;
            this.NoiDungHoiNghi = this.HoiNghi.MOTACHITIETHN;
            this.NgayBatDau = this.HoiNghi.THOIGIANBATDAU;
            this.NgayKetThuc = this.HoiNghi.THOIGIANKETTHUC;
            this.ImagePathHoiNghi = this.HoiNghi.HINHANH;
            this.SoLuong = this.HoiNghi.SOLUONG.ToString();
            this.MoTa = this.HoiNghi.MOTANGANHN;
            this.ListDiaDiemString = new List<string>();

            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {
                this.ListDiaDiem = (from dd in db.DIADIEMTOCHUCs select dd).ToList();
                for (int i = 0; i < this.ListDiaDiem.Count; i++)
                {
                    this.ListDiaDiemString.Add(this.ListDiaDiem[i].TENDD + ", " + this.ListDiaDiem[i].DIACHI);
                    if (this.ListDiaDiem[i].IDDD == this.HoiNghi.IDDD)
                    {
                        this.IndexDiaDiem = i;
                    }
                }
            }
        }

        private bool ValidateInput()
        {
            if (String.IsNullOrEmpty(TenHoiNghi.Trim()))
            {
                MessageBox.Show("Mời nhập lại tên hội nghị.");
                return false;
            }

            int num;
            if (String.IsNullOrEmpty(SoLuong.Trim()) || !int.TryParse(SoLuong, out num))
            {
                MessageBox.Show("Mời nhập lại số lượng.");
                return false;
            }

            if (String.IsNullOrEmpty(MoTa.Trim()))
            {
                MessageBox.Show("Mời nhập lại mô tả.");
                return false;
            }


            if (String.IsNullOrEmpty(NoiDungHoiNghi.Trim()))
            {
                MessageBox.Show("Mời nhập lại nội dung hội nghị.");
                return false;
            }

            if (NgayBatDau >= NgayKetThuc)
            {
                MessageBox.Show("Mời chọn lại thời gian.");
                return false;
            }

            if (IndexDiaDiem < 0)
            {
                MessageBox.Show("Mời chọn lại địa điểm.");
                return false;
            }

            return true;
        }

        private void SaveHoiNghi()
        {
            if (!ValidateInput())
            {
                LoadData();
                return;
            }

            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {
                HOINGHI hoiNghi = (from hn in db.HOINGHIs where hn.IDHN == this.HoiNghi.IDHN select hn).ToList().FirstOrDefault();

                hoiNghi.TENHN = TenHoiNghi;
                hoiNghi.MOTACHITIETHN = NoiDungHoiNghi;
                hoiNghi.IDDD = this.ListDiaDiem[IndexDiaDiem].IDDD;
                hoiNghi.THOIGIANBATDAU = NgayBatDau;
                hoiNghi.THOIGIANKETTHUC = NgayKetThuc;
                hoiNghi.HINHANH = ImagePathHoiNghi;
                hoiNghi.MOTANGANHN = MoTa;
                hoiNghi.SOLUONG = int.Parse(SoLuong);

                db.SaveChanges();
            }

            MessageBox.Show("Đã cập nhật thông tin hội nghị.");
        }

        private void CapQuyen()
        {

        }

        private void ChooseImage()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Chọn hình ảnh";
            dialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";

            if (dialog.ShowDialog() == true)
            {
                Console.WriteLine(dialog.FileName);
                ImagePathHoiNghi = dialog.FileName;
            }
        }
    }
}
