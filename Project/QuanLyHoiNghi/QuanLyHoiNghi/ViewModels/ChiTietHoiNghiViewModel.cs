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

        public Window Window { get; set; }

        public HOINGHI HoiNghi { get; set; }

        public String NgayBatDau { get; set; }

        public String NgayKetThuc { get; set; }

        public String DiaDiem { get; set; }

        public int SucChua { get; set; }

        public String ImagePathHoiNghi { get; set; }

        public bool IsSignedUp { get; set; }

        public ICommand SignUpCommand { get; set; }

        public ChiTietHoiNghiViewModel(HOINGHI hoiNghi, Window window)
        {
            this.Window = window;
            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {
                this.HoiNghi = hoiNghi;
                DIADIEMTOCHUC diaDiem = (from dd in db.DIADIEMTOCHUCs
                                         where dd.IDDD == hoiNghi.IDDD
                                         select dd).ToList().FirstOrDefault();

                this.DiaDiem = diaDiem.TENDD + ", " + diaDiem.DIACHI;
                this.SucChua = diaDiem.SUCCHUA;

                if (DangNhapViewModel.User == null)
                {
                    this.IsSignedUp = false;
                    this.SignUpCommand = new RelayCommand(SignUp);
                }
                else
                {
                    int isSignedUpCount = (from dk in db.DANGKITHAMGIAs
                                           where dk.IDUSER == DangNhapViewModel.User.IDUSER && dk.IDHN == hoiNghi.IDHN
                                           select dk).Count();
                    if (isSignedUpCount <= 0)
                    {
                        this.IsSignedUp = false;
                        this.SignUpCommand = new RelayCommand(SignUp);
                    }
                    else
                    {
                        this.IsSignedUp = true;
                        this.SignUpCommand = new RelayCommand(UnSignUp);
                    }
                }
            }

            this.NgayBatDau = this.HoiNghi.THOIGIANBATDAU.ToString("dd/MM/yyyy hh:mm");
            this.NgayKetThuc = this.HoiNghi.THOIGIANKETTHUC.ToString("dd/MM/yyyy hh:mm");
            this.ImagePathHoiNghi = Path.Combine(Environment.CurrentDirectory, this.HoiNghi.HINHANH);
            //this.IsSignedUp = false;
            //this.SignUpCommand = new RelayCommand(SignUp);
        }

        private void SignUp()
        {
            if (DangNhapViewModel.User == null)
            {
                DangNhapWindow dangNhapWindow = new DangNhapWindow();
                dangNhapWindow.Show();
                this.Window.Close();
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
                        dangky.IDUSER = DangNhapViewModel.User.IDUSER;
                        dangky.TRANGTHAI = 0;
                        dangky.THOIGIANDK = DateTime.Now;

                        db.DANGKITHAMGIAs.Add(dangky);
                        db.SaveChanges();
                    }

                    IsSignedUp = !IsSignedUp;
                    SignUpCommand = new RelayCommand(UnSignUp);
                }
                catch
                {
                    MessageBox.Show("Đã có lỗi xảy ra.");
                }
            }
        }
        
        private void UnSignUp()
        {
            if (DangNhapViewModel.User == null)
            {
                DangNhapWindow dangNhapWindow = new DangNhapWindow();
                dangNhapWindow.Show();
                this.Window.Close();
            }
            else
            {
                try
                {
                    using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
                    {

                        var dky = from dk in db.DANGKITHAMGIAs
                                  where dk.IDHN == this.HoiNghi.IDHN && dk.IDUSER == DangNhapViewModel.User.IDUSER
                                  select dk;
                        if (dky.Count() > 0)
                        {
                            db.DANGKITHAMGIAs.Remove(dky.First());
                            db.SaveChanges();
                            IsSignedUp = !IsSignedUp;
                            SignUpCommand = new RelayCommand(SignUp);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Đã có lỗi xảy ra.");
                }
            }
        }
    }
}
