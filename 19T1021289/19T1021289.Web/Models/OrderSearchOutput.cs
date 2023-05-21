using _19T1021289.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021289.Web.Models
{
    public class OrderSearchOutput : PaginationSearchOuput
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Order> Data { get; set; }

        public int OrderStatus { get; set; }
    }
}
