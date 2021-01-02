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
    public class ChiTietHoiNghiViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public HOINGHI HoiNghi { get; set; }

        public USER User { get; set; }

        public String NgayBatDau { get; set; }

        public String NgayKetThuc { get; set; }

        public String DiaDiem { get; set; }

        public int SucChua { get; set; }

        public String ImagePathHoiNghi { get; set; }

        public bool IsSignedUp { get; set; }

        public ICommand SignUpCommand { get; set; }

        public ChiTietHoiNghiViewModel(HOINGHI hoiNghi)
        {
            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {
                this.HoiNghi = hoiNghi;
                DIADIEMTOCHUC diaDiem = (from dd in db.DIADIEMTOCHUCs
                                         where dd.IDDD == hoiNghi.IDDD
                                         select dd).ToList().FirstOrDefault();

                this.DiaDiem = diaDiem.TENDD + ", " + diaDiem.DIACHI;
                this.SucChua = diaDiem.SUCCHUA;
            }

            this.NgayBatDau = this.HoiNghi.THOIGIANBATDAU.ToString("dd/MM/yy hh:mm");
            this.NgayKetThuc = this.HoiNghi.THOIGIANKETTHUC.ToString("dd/MM/yy hh:mm");
            this.ImagePathHoiNghi = Path.Combine(Environment.CurrentDirectory, this.HoiNghi.HINHANH);
            this.IsSignedUp = false;
            this.SignUpCommand = new RelayCommand(SignUp);
        }

        private void SignUp()
        {
            if (User == null)
            {

            }
            else
            {
                if (this.HoiNghi.SOLUONG >= this.SucChua)
                {
                    MessageBox.Show("Số lượng tham gia hội nghị đã đạt giới hạn.");
                    return;
                }

                try
                {
                    using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
                    {
                        DANGKITHAMGIA dangky = new DANGKITHAMGIA();
                        dangky.IDHN = this.HoiNghi.IDHN;
                        dangky.IDUSER = 1;
                        dangky.TRANGTHAI = 0;
                        dangky.THOIGIANDK = DateTime.Now;

                        db.DANGKITHAMGIAs.Add(dangky);
                        db.SaveChanges();
                    }

                    IsSignedUp = !IsSignedUp;
                }
                catch
                {
                    MessageBox.Show("Đã có lỗi xảy ra.");
                }


            }

        }

    }
}
