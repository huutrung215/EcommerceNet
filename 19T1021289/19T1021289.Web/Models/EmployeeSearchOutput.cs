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
    public class EmployeeSearchOutput : PaginationSearchOuput
    {
        /// <summary>
        /// danh sach nha cung cap
        /// </summary>
        public List<Employee> Data { get; set; }
    }
}