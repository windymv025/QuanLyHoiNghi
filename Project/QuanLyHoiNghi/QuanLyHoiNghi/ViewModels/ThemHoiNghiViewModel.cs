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
    public class ThemHoiNghiViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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

        public ICommand AddHoiNghiCommand { get; set; }

        public bool IsChooseImage { get; set; }

        public ThemHoiNghiViewModel()
        {
            ImagePathHoiNghi = NoiDungHoiNghi = TenHoiNghi = MoTa = "";
            SoLuong = "0";
            NgayBatDau = NgayKetThuc = DateTime.Now;
            IsChooseImage = false;
            IndexDiaDiem = -1;

            ListDiaDiemString = new List<string>();
            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {
                this.ListDiaDiem = (from dd in db.DIADIEMTOCHUCs select dd).ToList();

                foreach (var diaDiem in ListDiaDiem)
                {
                    ListDiaDiemString.Add(diaDiem.TENDD + ", " + diaDiem.DIACHI);
                }
            }

            this.ChooseImageCommand = new RelayCommand(ChooseImage);
            this.AddHoiNghiCommand = new RelayCommand(AddHoiNghi);
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
        private void AddHoiNghi()
        {
            if (!ValidateInput())
                return;

            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {
                int id = db.HOINGHIs.Max(hn => hn.IDHN) + 1;

                HOINGHI HoiNghi = new HOINGHI();
                HoiNghi.IDHN = id;
                HoiNghi.IDDD = ListDiaDiem[IndexDiaDiem].IDDD;
                HoiNghi.TENHN = TenHoiNghi;
                HoiNghi.MOTACHITIETHN = NoiDungHoiNghi;
                HoiNghi.THOIGIANBATDAU = NgayBatDau;
                HoiNghi.THOIGIANKETTHUC = NgayKetThuc;
                HoiNghi.HINHANH = ImagePathHoiNghi;
                HoiNghi.MOTANGANHN = MoTa;
                HoiNghi.SOLUONG = int.Parse(SoLuong);

                db.HOINGHIs.Add(HoiNghi);
                db.SaveChanges();
            }

            MessageBox.Show("Đã thêm hội nghị.");
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
                IsChooseImage = true;
            }
        }
    }
}
