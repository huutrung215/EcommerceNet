using _19T1021289.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021289.Web.Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<ProductPhoto> Photos { get; set; }
        public IEnumerable<ProductAttribute> Attributes { get; set; }
    }

}
