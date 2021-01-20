using Microsoft.Win32;
using QuanLyHoiNghi.Command;
using QuanLyHoiNghi.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

        public Window Window { get; set; }

        public HOINGHI HoiNghi { get; set; }

        public String TenHoiNghi { get; set; }

        public DateTime NgayBatDau { get; set; }

        public DateTime NgayKetThuc { get; set; }

        public String NoiDungHoiNghi { get; set; }

        public String ImagePathHoiNghi { get; set; }

        public List<DIADIEMTOCHUC> ListDiaDiem { get; set; }

        public List<String> ListDiaDiemString { get; set; }

        public int IndexDiaDiem { get; set; }

        public string SoLuong { get; set; }

        public String MoTa { get; set; }

        public ICommand ChooseImageCommand { get; set; }

        public ICommand SaveHoiNghiCommand { get; set; }

        public ICommand CapQuyenCommand { get; set; }

        public bool IsChooseImage { get; set; }

        public ChinhSuaHoiNghiViewModel(HOINGHI hoiNghi, Window window)
        {
            this.Window = window;
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
            this.ImagePathHoiNghi = Path.Combine(Environment.CurrentDirectory, this.HoiNghi.HINHANH);
            IsChooseImage = !String.IsNullOrEmpty(this.ImagePathHoiNghi);
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

            if (NgayBatDau >= NgayKetThuc)
            {
                MessageBox.Show("Mời chọn lại thời gian.");
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

            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {
                int iddd = this.ListDiaDiem[IndexDiaDiem].IDDD;
                var listHN = db.HOINGHIs.Where(o =>
                    (o.IDDD == iddd && o.IDHN != this.HoiNghi.IDHN && 
                        (o.THOIGIANBATDAU >= this.NgayBatDau && o.THOIGIANBATDAU <= this.NgayKetThuc 
                            || o.THOIGIANKETTHUC >= this.NgayBatDau && o.THOIGIANKETTHUC <= this.NgayKetThuc)
                        )).ToList();
               
                if (listHN.Count() > 0)
                {
                    MessageBox.Show("Đã có hội nghị diễn tra trong khung giờ này.");
                    return false;
                }
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

            try
            {
                using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
                {
                    HOINGHI hoiNghi = (from hn in db.HOINGHIs where hn.IDHN == this.HoiNghi.IDHN select hn).ToList().FirstOrDefault();

                    hoiNghi.TENHN = TenHoiNghi;
                    hoiNghi.MOTACHITIETHN = NoiDungHoiNghi;
                    hoiNghi.IDDD = this.ListDiaDiem[IndexDiaDiem].IDDD;
                    hoiNghi.THOIGIANBATDAU = NgayBatDau;
                    hoiNghi.THOIGIANKETTHUC = NgayKetThuc;

                    if (!ImagePathHoiNghi.Contains(hoiNghi.HINHANH))
                    {
                        String path = SaveImage(ImagePathHoiNghi);
                        String oldPath = hoiNghi.HINHANH;
                        hoiNghi.HINHANH = path;
                    }
                    

                    hoiNghi.MOTANGANHN = MoTa;
                    hoiNghi.SOLUONG = int.Parse(SoLuong);

                    db.SaveChanges();

                    MessageBox.Show("Đã cập nhật thông tin hội nghị.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra.");
            }

        }

        
        private void CapQuyen()
        {
            CapQuyenHoiNghiWindow window = new CapQuyenHoiNghiWindow(this.HoiNghi, "1");
            window.Show();
            this.Window.Close();
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

        private string SaveImage(string fileName)
        {
            string imageFolderPath = Path.Combine(Environment.CurrentDirectory, "Images");
            if (!Directory.Exists(imageFolderPath))
                Directory.CreateDirectory(imageFolderPath);

            var info = new FileInfo(fileName);
            var newName = $"{Guid.NewGuid()}{info.Extension}";

            //string newFileName = DateTime.Now.ToString("dd-MM-yy-HH-mm-ss") + Path.GetExtension(fileName);
            string imagePath = Path.Combine(imageFolderPath, newName);
            File.Copy(fileName, imagePath);

            return "Images\\" + newName;

        }

        private void DeleteImage(String fileName)
        {
            string path = Path.Combine(Environment.CurrentDirectory, fileName);
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}
