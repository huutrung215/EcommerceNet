using _19T1021289.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace _19T1021289.Web.Models
{
    public class ProductSearchInput : PaginationSearchInput
    {
        public Supplier SelectedSupplierId { get; set; }
        public Category SelectedCategoryId { get; set; } 
    }
}
