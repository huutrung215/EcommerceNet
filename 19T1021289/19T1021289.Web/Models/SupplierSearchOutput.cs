using _19T1021289.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021289.Web.Models
{
    /// <summary>
    /// ket qua tim kiem nha cung cap duoi dang phan trang
    /// </summary>
    public class SupplierSearchOutput : PaginationSearchOuput
    {
        /// <summary>
        /// danh sach nha cung cap
        /// </summary>
        public List<Supplier> Data { get; set; }
    }
}