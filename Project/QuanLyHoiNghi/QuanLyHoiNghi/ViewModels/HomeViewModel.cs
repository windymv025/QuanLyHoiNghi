using QuanLyHoiNghi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyHoiNghi.ViewModels
{
    public class HoiNghiDAO
    {
        public static List<HOINGHI> GetAllHoiNghi()
        {
            List<HOINGHI> list = new List<HOINGHI>();
            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {
                list = db.HOINGHIs.ToList();
            }
            return list;
        }
        public static List<HOINGHI> GetResultSearchHoiNghi(string name, string place, DateTime date)
        {
            List<HOINGHI> list = new List<HOINGHI>();
            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {
                string sqlHoiNghi = $"select* from HOINGHI where (TENHN LIKE N'%{name}%')";
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
            }
            return list;
        }
    }
    public class PagingInfo
    {
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int ItemInPerPage { get; set; }

        public PagingInfo(int numberOfItemInPerPage, int total)
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
    public class HomeViewModel
    {
        public PagingInfo PagingInfo { get; set; }
        public List<HOINGHI> ListHoiNghi { get; set; }

        public List<HOINGHI> loadPage(int pageNumber, int numItemInPage)
        {
            List<HOINGHI> resulf = new List<HOINGHI>();
            PagingInfo.ItemInPerPage = numItemInPage;

            if (PagingInfo.TotalPage > 0)
            {
                if (pageNumber > PagingInfo.TotalPage)
                    pageNumber = 1;
                if (pageNumber < 1)
                    pageNumber = PagingInfo.TotalPage;

                PagingInfo.CurrentPage = pageNumber;

                resulf = ListHoiNghi.Skip((pageNumber - 1) * PagingInfo.ItemInPerPage).Take(PagingInfo.ItemInPerPage).ToList();
            }
            else
            {
                PagingInfo.CurrentPage = 0;
            }
            return resulf;
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
