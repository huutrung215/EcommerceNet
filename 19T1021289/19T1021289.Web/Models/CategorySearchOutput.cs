using _19T1021289.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021289.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CategorySearchOutput : PaginationSearchOuput
    {
        /// <summary>
        /// danh sach nha cung cap
        /// </summary>
        public List<Category> Data { get; set; }
    }
}