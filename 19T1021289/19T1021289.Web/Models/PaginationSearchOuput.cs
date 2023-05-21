using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021289.Web.Models
{
    /// <summary>
    /// lop co so dung de luu tru  ket qua tim kiem duoi dang phan trang, lop abstract ko dung new, ke thua abstract ko dc dung
    /// action nhan dau vao, view dau ra
    /// </summary>
    public abstract class PaginationSearchOuput
    {
        /// <summary>
        /// trang dc hien thi
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// so dong tren moi trang
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// gia tri dang dc tim kiem
        /// </summary>
        public string SearchValue { get; set; }
        /// <summary>
        /// so dong tim dc
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// tong so trang
        /// </summary>
        public int PageCount 
        { get
            {
                if (PageSize == 0) 
                    return 1;
                int p = RowCount / PageSize;
                if (RowCount % PageSize > 0) 
                    p += 1;
                return p;
            }
        }

    }
}