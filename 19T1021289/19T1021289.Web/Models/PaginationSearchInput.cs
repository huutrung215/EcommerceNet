using _19T1021289.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021289.Web.Models
{
    /// <summary>
    /// bieu dien du lieu dau vao de tim kiem, phan trang chung
    /// </summary>
    public class PaginationSearchInput
    {
        /// <summary>
        /// Trang can hien thi
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// so dong tren trang
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gia tri can tim
        /// </summary>
        public string SearchValue { get; set; }
    }
}