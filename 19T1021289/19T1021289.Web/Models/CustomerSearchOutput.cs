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
    public class CustomerSearchOutput : PaginationSearchOuput
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Customer> Data { get; set; }
    }
}