using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021289.DomainModels
{
    /// <summary>
    /// thông tin khách hàng
    /// </summary>
    public class Customer
    {
        public int CustomerID { get; set; }
        public String CustomerName { get; set; }
        public String ContactName { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String Country { get; set; }
        public String Email { get; set; }
        public String PostalCode { get; set; }

        public String Password { get; set; }


    }
}
