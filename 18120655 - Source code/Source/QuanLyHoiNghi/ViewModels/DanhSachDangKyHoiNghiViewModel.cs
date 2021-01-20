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

        public int SucChua { get; set; }

        public DanhSachDangKyHoiNghiViewModel(HOINGHI HoiNghi)
        {
            this.HoiNghi = HoiNghi;
            this.ListDangKyUser = new ObservableCollection<XacNhanDangKyUser>();
            LoadData();
        }

        public void LoadDataByNameAndEmail(string name, string email)
        {
            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {
                string sql = "select* from [QuanLiHoiNghi].[dbo].[USER] ";
                string sqlName = $" freetext(TENUSER, N'%{name}%')";
                string sqlEmail = $" EMAIL like N'%{email}%'";

                if (name != "" && email != "")
                {
                    sql += "where" + sqlName + "and" + sqlEmail;
                }
                else if (name != "") 
                {
                    sql += "where" + sqlName;
                }
                else if(email!="")
                {
                    sql += "where" + sqlEmail;
                }

                var ListUser = db.USERs.SqlQuery(sql);

                this.ListDangKyUser = new ObservableCollection<XacNhanDangKyUser>();

                foreach (var user in ListUser)
                {
                    foreach (var dangKy in db.DANGKITHAMGIAs)
                    {
                        if (dangKy.IDUSER == user.IDUSER && dangKy.IDHN == HoiNghi.IDHN)
                        {
                            if (user.HINHANH != string.Empty)
                            {
                                user.HINHANH = Path.GetFullPath(user.HINHANH);
                            }
                            else
                                user.HINHANH = Path.GetFullPath("Images/user_example.png");
                            XacNhanDangKyUser capQuyenUser = new XacNhanDangKyUser(user, dangKy, HoiNghi, SucChua);
                            ListDangKyUser.Add(capQuyenUser);
                        }
                    }
                }
            }

        }

        public void LoadData()
        {
            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {
                SucChua = (from dd in db.DIADIEMTOCHUCs
                           where dd.IDDD == HoiNghi.IDDD
                           select dd).First().SUCCHUA;

                var ListUser = db.USERs;

                this.ListDangKyUser = new ObservableCollection<XacNhanDangKyUser>();


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
                            XacNhanDangKyUser capQuyenUser = new XacNhanDangKyUser(user, dangKy, HoiNghi, SucChua);
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

        public void sapXepUserTangDanTheoTen()
        {
            var list = from user in this.ListDangKyUser
                       orderby user.User.TENUSER
                       select user;
            this.ListDangKyUser = new ObservableCollection<XacNhanDangKyUser>();
            foreach (var item in list)
            {
                this.ListDangKyUser.Add(item);
            }
        }
        public void sapXepUserGiamDanTheoTen()
        {
            var list = from user in this.ListDangKyUser
                       orderby user.User.TENUSER descending
                       select user;

            this.ListDangKyUser = new ObservableCollection<XacNhanDangKyUser>();

            foreach (var item in list)
            {
                this.ListDangKyUser.Add(item);
            }
        }

        public void sapXepUserTangDanTheoEmail()
        {
            var list = from user in this.ListDangKyUser
                       orderby user.User.EMAIL
                       select user;

            this.ListDangKyUser = new ObservableCollection<XacNhanDangKyUser>();

            foreach (var item in list)
            {
                this.ListDangKyUser.Add(item);
            }
        }
        public void sapXepUserGiamDanTheoEmail()
        {
            var list = from user in this.ListDangKyUser
                       orderby user.User.EMAIL descending
                       select user;

            this.ListDangKyUser = new ObservableCollection<XacNhanDangKyUser>();

            foreach (var item in list)
            {
                this.ListDangKyUser.Add(item);
            }
        }

        public class XacNhanDangKyUser : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public USER User { get; set; }
            public DANGKITHAMGIA DangKiThamGia { get; set; }
            public HOINGHI HoiNghi { get; set; }
            public int SucChua { get; set; }

            public ICommand XacNhanDangKyCommand { get; set; }
            public ICommand ChanNguoiDungCommand { get; set; }

            public XacNhanDangKyUser(USER user, DANGKITHAMGIA dangKy, HOINGHI hoiNghi, int sucChua)
            {
                this.User = user;
                this.DangKiThamGia = dangKy;
                this.HoiNghi = hoiNghi;
                this.SucChua = sucChua;
                this.XacNhanDangKyCommand = new RelayCommand(xachNhanDangKy);
                this.ChanNguoiDungCommand = new RelayCommand(chanNguoiDung);
            }

            private void chanNguoiDung()
            {
                if (this.DangKiThamGia.TRANGTHAI == 2)  // 0: Chua xac nhan ; 1: Chan ; 2: Xac nhan
                {
                    this.DangKiThamGia.TRANGTHAI = 1;
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
                        foreach (var item in db.HOINGHIs)
                        {
                            if (item.IDHN == this.HoiNghi.IDHN)
                            {
                                item.SOLUONG--;
                                this.HoiNghi.SOLUONG--;
                                break;
                            }
                        }
                        db.SaveChanges();

                    }
                }
                else if (this.DangKiThamGia.TRANGTHAI == 0)
                {
                    this.DangKiThamGia.TRANGTHAI = 1;
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
                else
                {
                    this.DangKiThamGia.TRANGTHAI = 0;
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
            }
            private void xachNhanDangKy()
            {
                try
                {
                    if (this.DangKiThamGia.TRANGTHAI == 2)
                    {
                        this.DangKiThamGia.TRANGTHAI = 0;
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
                            foreach (var item in db.HOINGHIs)
                            {
                                if (item.IDHN == this.HoiNghi.IDHN)
                                {
                                    item.SOLUONG--;
                                    this.HoiNghi.SOLUONG--;
                                    break;
                                }
                            }
                            db.SaveChanges();
                        }
                    }
                    else if (this.DangKiThamGia.TRANGTHAI == 0)
                    {
                        if (this.HoiNghi.SOLUONG == SucChua)
                        {
                            MessageBox.Show("Số lượng người tham dự không thể vượt quá sức chứa của địa điểm.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            this.DangKiThamGia.TRANGTHAI = 2;
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
                                foreach (var item in db.HOINGHIs)
                                {
                                    if (item.IDHN == this.HoiNghi.IDHN)
                                    {
                                        item.SOLUONG++;
                                        this.HoiNghi.SOLUONG++;
                                        break;
                                    }
                                }
                                db.SaveChanges();
                            }
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
