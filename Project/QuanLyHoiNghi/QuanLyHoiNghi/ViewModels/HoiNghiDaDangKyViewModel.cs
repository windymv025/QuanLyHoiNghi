using QuanLyHoiNghi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyHoiNghi.ViewModels
{
    public class HoiNghiDaDangKyDAO
    {
        public static List<HOINGHI> GetAllHoiNghiDaDangKy()
        {
            List<HOINGHI> list = new List<HOINGHI>();
            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {

                var listHoiNghi = from hoiNghi in db.HOINGHIs
                                  join dangkithamgia in db.DANGKITHAMGIAs
                                  on hoiNghi.IDHN equals dangkithamgia.IDHN
                                  where dangkithamgia.IDUSER == 1
                                  select hoiNghi;
                list = listHoiNghi.ToList();
            }
            return list;
        }

        public static List<HOINGHI> GetResultSearchHoiNghiDaDangKy(string name, string place, DateTime date)
        {
            List<HOINGHI> list = new List<HOINGHI>();
            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {
                string sqlHoiNghi = $"select * from HOINGHI where (TENHN LIKE N'%{name}%')";
                List<HOINGHI> resultHoiNghi = db.HOINGHIs.SqlQuery(sqlHoiNghi).ToList();
                DateTime defaultDate = new DateTime();

                if (date != defaultDate)
                {
                    if (resultHoiNghi.Count <= 0)
                    {
                        resultHoiNghi = db.HOINGHIs.ToList();
                    }
                    resultHoiNghi = (from item in resultHoiNghi
                                     where item.THOIGIANBATDAU.Date == date.Date
                                     select item).ToList();
                }

                string sqlDiaDiem = $"select* from DIADIEMTOCHUC where (TENDD LIKE N'%{place}%')";
                var resultDiaDiem = db.DIADIEMTOCHUCs.SqlQuery(sqlDiaDiem);

                if (resultDiaDiem.Count() > 0 && resultHoiNghi.Count > 0)
                {
                    var result = from hn in resultHoiNghi
                                 join dd in resultDiaDiem
                                 on hn.IDDD equals dd.IDDD
                                 select hn;

                    list = result.ToList();
                }
                else if (resultHoiNghi.Count > 0)
                {
                    list = resultHoiNghi;
                }
                else if (resultDiaDiem.Count() > 0)
                {
                    string sql = $"select hn.IDHN, hn.IDDD, TENHN,hn.MOTANGANHN, hn.MOTACHITIETHN, hn.HINHANH, hn.THOIGIANBATDAU, hn.THOIGIANKETTHUC, hn.SOLUONG from HOINGHI as hn join DIADIEMTOCHUC as dd on hn.IDDD = dd.IDDD where (dd.TENDD LIKE N'%{place}%')";
                    var result = db.HOINGHIs.SqlQuery(sql);
                    list = result.ToList();
                }

                var listHN = from hn in list
                       join dk in db.DANGKITHAMGIAs
                       on hn.IDHN equals dk.IDHN
                       where dk.IDUSER == 1
                       select hn;

                list = listHN.ToList();
            }
            return list;
        }
    }
    public class PagingInfoOfHNDDK
    {
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int ItemInPerPage { get; set; }

        public PagingInfoOfHNDDK(int numberOfItemInPerPage, int total)
        {
            if (total > 0)
            {
                CurrentPage = 1;
                ItemInPerPage = numberOfItemInPerPage;
                TotalPage = total / numberOfItemInPerPage +
                    ((total % numberOfItemInPerPage) == 0 ? 0 : 1);
            }
            else
            {
                CurrentPage = 0;
                TotalPage = 0;
            }

        }
    }
    public class HoiNghiDaDangKyViewModel
    {
        public PagingInfoOfHNDDK PagingInfo { get; set; }
        public List<HOINGHI> ListHoiNghi { get; set; }

        public List<HOINGHI> loadPage(int pageNumber, int numItemInPage)
        {
            List<HOINGHI> result = new List<HOINGHI>();
            PagingInfo.ItemInPerPage = numItemInPage;

            if (PagingInfo.TotalPage > 0)
            {
                if (pageNumber > PagingInfo.TotalPage)
                    pageNumber = 1;
                if (pageNumber < 1)
                    pageNumber = PagingInfo.TotalPage;

                PagingInfo.CurrentPage = pageNumber;

                result = ListHoiNghi.Skip((pageNumber - 1) * PagingInfo.ItemInPerPage).Take(PagingInfo.ItemInPerPage).ToList();
            }
            else
            {
                PagingInfo.CurrentPage = 0;
            }
            return result;
        }

        public void sapXepHoiNghiTangDanTheoTen()
        {
            var list = from hoiNghi in this.ListHoiNghi
                       orderby hoiNghi.TENHN
                       select hoiNghi;

            this.ListHoiNghi = list.ToList();
        }
        public void sapXepHoiNghiTangDanTheoThoiGian()
        {
            var list = from hoiNghi in this.ListHoiNghi
                       orderby hoiNghi.THOIGIANBATDAU
                       select hoiNghi;

            this.ListHoiNghi = list.ToList();
        }
        public void sapXepHoiNghiGiamDanTheoTen()
        {
            var list = from hoiNghi in this.ListHoiNghi
                       orderby hoiNghi.TENHN descending
                       select hoiNghi;

            this.ListHoiNghi = list.ToList();
        }
        public void sapXepHoiNghiGiamDanTheoThoiGian()
        {
            var list = from hoiNghi in this.ListHoiNghi
                       orderby hoiNghi.THOIGIANBATDAU descending
                       select hoiNghi;

            this.ListHoiNghi = list.ToList();
        }
    }

}
