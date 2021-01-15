using Microsoft.Win32;
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
    public class CapQuyenHoiNghiViewModel : INotifyPropertyChanged
    {
        const int USER_PER_PAGE = 4;

        public event PropertyChangedEventHandler PropertyChanged;

        public HOINGHI HoiNghi { get; set; }

        public ObservableCollection<CapQuyenUser> ListCapQuyenUser { get; set; }

        public int PageIndex { get; set; }

        public int LastPage { get; set; }

        public String Pagination { get; set; }

        public String LoaiAdmin { get; set; }

        public ICommand PreviousPageCommand { get; set; }

        public ICommand NextPageCommand { get; set; }

        public CapQuyenHoiNghiViewModel(HOINGHI HoiNghi, String type)
        {
            this.HoiNghi = HoiNghi;
            this.LoaiAdmin = type;
            this.PageIndex = 1;

            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {
                float count = db.USERs.Count();
                LastPage = (int)Math.Ceiling(count / USER_PER_PAGE);
                Pagination = PageIndex.ToString() + "/" + LastPage.ToString();
            }

            this.ListCapQuyenUser = new ObservableCollection<CapQuyenUser>();
            LoadData();

            PreviousPageCommand = new RelayCommand(GetPreviousPage);
            NextPageCommand = new RelayCommand(GetNextPage);
        }

        private void LoadData()
        {
            ListCapQuyenUser.Clear();
            int index = PageIndex - 1;

            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {
                var ListUser = db.USERs.OrderBy(u => u.IDUSER).Skip(index * USER_PER_PAGE).Take(USER_PER_PAGE).ToList();
                var ListChiTietAdmin = db.CHITIETADMINs.Where(ct => ct.IDHN == this.HoiNghi.IDHN).ToList();

                foreach (var user in ListUser)
                {
                    var chiTiet = ListChiTietAdmin.Find(ct => ct.IDUSER == user.IDUSER);

                    CapQuyenUser capQuyenUser = new CapQuyenUser(user, this.HoiNghi.IDHN, chiTiet != null, LoaiAdmin);
                    ListCapQuyenUser.Add(capQuyenUser);
                }
            }
        }

        private void GetPreviousPage()
        {
            if (PageIndex == 1)
                PageIndex = LastPage;
            else PageIndex--;

            LoadData();
            Pagination = PageIndex.ToString() + "/" + LastPage.ToString();
        }

        private void GetNextPage()
        {
            if (PageIndex == LastPage)
                PageIndex = 1;
            else PageIndex++;

            LoadData();
            Pagination = PageIndex.ToString() + "/" + LastPage.ToString();
        }

        public class CapQuyenUser : INotifyPropertyChanged
        {

            public event PropertyChangedEventHandler PropertyChanged;

            public USER User { get; set; }

            public int IdHN { get; set; }

            public bool IsCapQuyen { get; set; }

            public String ImagePath { get; set; }

            public String LoaiAdmin { get; set; }

            public ICommand CapQuyenCommand { get; set; }

            public CapQuyenUser(USER user, int IdHN, bool isCapQuyen, String LoaiAdmin)
            {
                this.User = user;
                this.IdHN = IdHN;
                this.IsCapQuyen = isCapQuyen;
                this.LoaiAdmin = LoaiAdmin;
                this.ImagePath = Path.Combine(Environment.CurrentDirectory, this.User.HINHANH ?? "Images\\user_example.png");
                this.CapQuyenCommand = new RelayCommand(CapQuyen);
            }

            private void CapQuyen()
            {
                this.IsCapQuyen = !this.IsCapQuyen;

                try
                {
                    if (this.IsCapQuyen)
                    {
                        using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
                        {
                            CHITIETADMIN chiTiet = new CHITIETADMIN();
                            chiTiet.IDHN = IdHN;
                            chiTiet.IDUSER = User.IDUSER;
                            chiTiet.LOAIADMIN = LoaiAdmin;
                            db.CHITIETADMINs.Add(chiTiet);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
                        {
                            CHITIETADMIN chiTiet = (from ct in db.CHITIETADMINs
                                                    where ct.IDHN == IdHN && ct.IDUSER == User.IDUSER
                                                    select ct).ToList().FirstOrDefault();

                            db.CHITIETADMINs.Remove(chiTiet);
                            db.SaveChanges();
                        }
                    }
                }
                catch
                {
                    this.IsCapQuyen = !this.IsCapQuyen;
                    MessageBox.Show("Đã có lỗi xảy ra.");
                }

            }
        }
    }
}
