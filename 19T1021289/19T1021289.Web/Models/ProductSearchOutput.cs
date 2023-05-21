using _19T1021289.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace _19T1021289.Web.Models
{
    public class ProductSearchOutput : PaginationSearchOuput
    {
        /// <summary>
        /// danh sach mat hang
        /// </summary>
        public List<Product> Data { get; set; }

        public int SelectedSupplierId { get; set; }
        public int SelectedCategoryId { get; set; }
    }
}
