using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021289.DomainModels
{
    /// <summary>
    /// thong tin nha cung cap
    /// </summary>
    public class Supplier
    {
        /// <summary>
        /// 
        /// </summary>
        public int SupplierID { get; set; }
        
        public String SupplierName { get; set; }

        public String ContactName { get; set; }

        public String Address { get; set; }
        public String Phone { get; set; }

        public String Country { get; set; }

        public String City { get; set; }

        public String PostalCode { get; set; }
    }
}
