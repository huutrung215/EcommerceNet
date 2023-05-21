using _19T1021289.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021289.Web.Models
{
    public class OrderViewModel
    {
        public Order Orders { get; set; }
        public IEnumerable<OrderDetail> OrderDetail { get; set; }
        public OrderDetail Detail { get; set; }
    }
}
