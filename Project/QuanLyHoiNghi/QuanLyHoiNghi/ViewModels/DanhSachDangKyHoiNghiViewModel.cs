using QuanLyHoiNghi.Command;
using QuanLyHoiNghi.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyHoiNghi.ViewModels
{
    public class DanhSachDangKyHoiNghiViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public HOINGHI HoiNghi { get; set; }

        public ObservableCollection<XacNhanDangKyUser> ListDangKyUser { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public DanhSachDangKyHoiNghiViewModel(HOINGHI HoiNghi)
        {
            this.HoiNghi = HoiNghi;
            this.ListDangKyUser = new ObservableCollection<XacNhanDangKyUser>();
            LoadData();
        }

        public void LoadData()
        {
            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {
                var ListUser = db.USERs;

                foreach (var user in ListUser)
                {
                    foreach(var dangKy in db.DANGKITHAMGIAs)
                    {
                        if (dangKy.IDUSER == user.IDUSER && dangKy.IDHN == HoiNghi.IDHN)
                        {
                            if (user.HINHANH != string.Empty)
                                user.HINHANH = Path.GetFullPath(user.HINHANH);
                            else
                                user.HINHANH = Path.GetFullPath("Images/user_example.png");
                            XacNhanDangKyUser capQuyenUser = new XacNhanDangKyUser(user, dangKy);
                            ListDangKyUser.Add(capQuyenUser);
                        }
                    }
                }
            }
        }

        public ObservableCollection<XacNhanDangKyUser> loadPage(int pageNumber, int numItemInPage)
        {
            var resulf = new ObservableCollection<XacNhanDangKyUser>();
            PagingInfo.ItemInPerPage = numItemInPage;

            if (PagingInfo.TotalPage > 0)
            {
                if (pageNumber > PagingInfo.TotalPage)
                    pageNumber = 1;
                if (pageNumber < 1)
                    pageNumber = PagingInfo.TotalPage;

                PagingInfo.CurrentPage = pageNumber;

                var list = ListDangKyUser.Skip((pageNumber - 1) * PagingInfo.ItemInPerPage).Take(PagingInfo.ItemInPerPage);

                foreach(var item in list)
                {
                    resulf.Add(item);
                }
            }
            else
            {
                PagingInfo.CurrentPage = 0;
            }
            return resulf;
        }

        public class XacNhanDangKyUser : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public USER User { get; set; }
            public DANGKITHAMGIA DangKiThamGia { get; set; }

            public ICommand XacNhanDangKyCommand { get; set; }
            public ICommand ChanNguoiDungCommand { get; set; }

            public XacNhanDangKyUser(USER user, DANGKITHAMGIA dangKy)
            {
                this.User = user;
                this.DangKiThamGia = dangKy;
                this.XacNhanDangKyCommand = new RelayCommand(xachNhanDangKy);
                this.ChanNguoiDungCommand = new RelayCommand(chanNguoiDung);
            }

            private void chanNguoiDung()
            {
                if (this.DangKiThamGia.TRANGTHAI != -1)
                {
                    this.DangKiThamGia.TRANGTHAI = -1;
                    
                }
                else
                {
                    this.DangKiThamGia.TRANGTHAI = 0;
                }
                using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
                {
                    foreach (var item in db.DANGKITHAMGIAs)
                    {
                        if (item.IDUSER == this.User.IDUSER && item.IDHN == this.DangKiThamGia.IDHN)
                        {
                            item.TRANGTHAI = this.DangKiThamGia.TRANGTHAI;
                            break;
                        }
                    }
                    db.SaveChanges();
                }
            }
            private void xachNhanDangKy()
            {
                try
                {
                    if (this.DangKiThamGia.TRANGTHAI == 1)
                    {
                        this.DangKiThamGia.TRANGTHAI = 0;
                    }
                    else if(this.DangKiThamGia.TRANGTHAI == 0)
                    {
                        this.DangKiThamGia.TRANGTHAI = 1;
                    }
                    using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
                    {
                        foreach (var item in db.DANGKITHAMGIAs)
                        {
                            if (item.IDUSER == this.User.IDUSER && item.IDHN == this.DangKiThamGia.IDHN)
                            {
                                item.TRANGTHAI = this.DangKiThamGia.TRANGTHAI;
                                break;
                            }
                        }
                        db.SaveChanges();
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
