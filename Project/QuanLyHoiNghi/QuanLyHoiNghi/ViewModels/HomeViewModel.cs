using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyHoiNghi.ViewModels
{
    public class HoiNghiDAO
    {
        public static List<object> GetAllHoiNghi()
        {
            return new List<object>();
        }
    }
    public class PagingInfo
    {
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int NumberOfTripInPerPage { get; set; }

        public PagingInfo(int numberOfItemInPerPage, int total)
        {
            if (total > 0)
            {
                CurrentPage = 1;
                NumberOfTripInPerPage = numberOfItemInPerPage;
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
        public List<object> HoiNghis { get; set; }

        public List<object> loadPage(int pageNumber, int numItemInPage)
        {
            List<object> resulf = new List<object>();
            PagingInfo.NumberOfTripInPerPage = numItemInPage;

            if (PagingInfo.TotalPage > 0)
            {
                if (pageNumber > PagingInfo.TotalPage)
                    pageNumber = 1;
                if (pageNumber < 1)
                    pageNumber = PagingInfo.TotalPage;

                PagingInfo.CurrentPage = pageNumber;

                //resulf = ChuyenDis.Skip((pageNumber - 1) * PagingInfo.NumberOfTripInPerPage).Take(PagingInfo.NumberOfTripInPerPage).ToList();
            }
            else
            {
                PagingInfo.CurrentPage = 0;
            }
            return resulf;
        }

    }
}
