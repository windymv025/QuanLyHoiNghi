using QuanLyHoiNghi.Command;
using QuanLyHoiNghi.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public bool IsSignedUp { get; set; }

        public ICommand SignUpCommand { get; set; }

        public ChiTietHoiNghiViewModel(HOINGHI hoiNghi, USER user)
        {
            this.User = user;

            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {
                var HoiNghi = (from hn in db.HOINGHIs
                               join dd in db.DIADIEMTOCHUCs on hn.IDDD equals dd.IDDD
                               where hn.IDHN == 1
                               select new { HoiNghi = hn, DiaDiem = dd }).ToList().FirstOrDefault();

                //var DiaDiemHN = (from dd in db.DIADIEMTOCHUCs
                //                where dd.IDDD == hoiNghi.IDDD
                //                select dd).ToList().FirstOrDefault();

                this.HoiNghi = HoiNghi.HoiNghi;
                this.DiaDiem = HoiNghi.DiaDiem.TENDD + ", " + HoiNghi.DiaDiem.DIACHI;
            }

            this.NgayBatDau = this.HoiNghi.THOIGIANBATDAU.ToString("dd/MM/yy hh:mm");
            this.NgayKetThuc = this.HoiNghi.THOIGIANKETTHUC.ToString("dd/MM/yy hh:mm");
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

        }

    }
}
