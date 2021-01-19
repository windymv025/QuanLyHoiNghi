using QuanLyHoiNghi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyHoiNghi.ViewModels
{
    public class PagingInfoOfHNQL
    {
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int ItemInPerPage { get; set; }

        public PagingInfoOfHNQL(int numberOfItemInPerPage, int total)
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
    public class QuanLyHoiNghiViewModel
    {
        public PagingInfoOfHNQL PagingInfo { get; set; }
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
