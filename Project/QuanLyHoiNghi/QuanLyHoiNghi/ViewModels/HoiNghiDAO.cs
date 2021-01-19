using QuanLyHoiNghi.Model;
using System;
using System.Collections.Generic;
using System.Linq;

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
                string sqlHoiNghi = $"select* from HOINGHI where freetext(TENHN, N'%{name}%')";
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

                string sqlDiaDiem = $"select* from DIADIEMTOCHUC where freetext(TENDD, N'%{place}%')";
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
                    string sql = $"select hn.IDHN, hn.IDDD, TENHN,hn.MOTANGANHN, hn.MOTACHITIETHN, hn.HINHANH, hn.THOIGIANBATDAU, hn.THOIGIANKETTHUC, hn.SOLUONG from HOINGHI as hn join DIADIEMTOCHUC as dd on hn.IDDD = dd.IDDD where freetext(dd.TENDD, N'%{place}%')";
                    var result = db.HOINGHIs.SqlQuery(sql);
                    list = result.ToList();
                }
            }
            return list;
        }

        public static List<HOINGHI> GetAllHoiNghiDaDangKy()
        {
            List<HOINGHI> list = new List<HOINGHI>();
            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {

                var listHoiNghi = from hoiNghi in db.HOINGHIs
                                  join dangkithamgia in db.DANGKITHAMGIAs
                                  on hoiNghi.IDHN equals dangkithamgia.IDHN
                                  where dangkithamgia.IDUSER == DangNhapViewModel.User.IDUSER
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
                             where dk.IDUSER == DangNhapViewModel.User.IDUSER
                             select hn;

                list = listHN.ToList();
            }
            return list;
        }

        public static List<HOINGHI> GetAllHoiNghiQuanLy()
        {
            List<HOINGHI> list = new List<HOINGHI>();
            using (DBQuanLiHoiNghiEntities db = new DBQuanLiHoiNghiEntities())
            {

                var listHoiNghi = from hoiNghi in db.HOINGHIs
                                  join chitietadmin in db.CHITIETADMINs
                                  on hoiNghi.IDHN equals chitietadmin.IDHN
                                  where chitietadmin.IDUSER == DangNhapViewModel.User.IDUSER
                                  select hoiNghi;
                list = listHoiNghi.ToList();
            }
            return list;
        }

        public static List<HOINGHI> GetResultSearchHoiNghiQuanLy(string name, string place, DateTime date)
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
                             join chitietadmin in db.CHITIETADMINs
                             on hn.IDHN equals chitietadmin.IDHN
                             where chitietadmin.IDUSER == DangNhapViewModel.User.IDUSER
                             select hn;

                list = listHN.ToList();
            }
            return list;
        }
    }
}
