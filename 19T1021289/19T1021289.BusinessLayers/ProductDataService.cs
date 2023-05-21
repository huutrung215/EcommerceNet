using _19T1021289.DomainModels;
using _19T1021289.DataLayers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021289.BusinessLayers
{
    /// <summary>
    /// Các nghiệp vụ quản lý hàng hóa
    /// </summary>
    public static class ProductDataService
    {
        private static readonly IProductDAL productDB;

        /// <summary>
        /// 
        /// </summary>
        static ProductDataService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            productDB = new DataLayers.SQLServer.ProductDAL(connectionString);
        }


        /// <summary>
        /// Tìm kiếm và lấy danh sách mặt hàng (không phân trang)
        /// </summary>
        /// <param name="searchValue">Tên mặt hàng cần tìm (chuỗi rỗng nếu không tìm kiếm)</param>
        /// <param name="categoryID">Mã loại hàng cần tìm (chuỗi rỗng nếu không tìm kiếm theo loại hàng)</param>
        /// <param name="supplierID">Mã nhà cung cấp cần tìm (chuỗi rỗng nếu không tìm kiếm theo nhà cung cấp)</param>
        /// <returns></returns>
        public static List<Product> ListProducts(string searchValue = "", int categoryID = 0, int supplierID = 0)
        {
            return productDB.List(1, 0, searchValue, categoryID, supplierID).ToList();
        }

        public static List<Product> ListOfProducts()
        {
            return productDB.List().ToList();
        }

        public static Product GetProduct(int productID)
        {
            return productDB.Get(productID);
        }

        public static IEnumerable<Product> GetAllProducts()
        {
            return productDB.GetAllProduct();
        }

        /// <summary>
        /// Tìm kiếm và lấy danh sách mặt hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="categoryID"></param>
        /// <param name="supplierID"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Product> ListProducts(int page, int pageSize, string searchValue, int categoryID, int supplierID, out int rowCount)
        {
            rowCount = productDB.Count(searchValue, categoryID, supplierID);
            if (categoryID == -99) categoryID = 0;
            if (supplierID == -99) supplierID = 0;
            return productDB.List(page, pageSize, searchValue, categoryID, supplierID).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddProduct(Product data)
        {
            return productDB.Add(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateProduct(Product data)
        {
            return productDB.Update(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static bool DeleteProduct(int productID)
        {
            return productDB.Delete(productID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static bool InUsedProduct(int productID)
        {
            return productDB.InUsed(productID);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static List<ProductPhoto> ListPhotos(int productID)
        {
            return productDB.ListPhotos(productID).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public static ProductPhoto GetPhoto(long photoID)
        {
            return productDB.GetPhoto(photoID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static long AddPhoto(ProductPhoto data)
        {
            return productDB.AddPhoto(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdatePhoto(ProductPhoto data)
        {
            return productDB.UpdatePhoto(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public static bool DeletePhoto(long photoID)
        {
            return productDB.DeletePhoto(photoID);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static List<ProductAttribute> ListAttributes(int productID)
        {
            return productDB.ListAttributes(productID).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        public static ProductAttribute GetAttribute(int attributeID)
        {
            return productDB.GetAttribute(attributeID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static long AddAttribute(ProductAttribute data)
        {
            return productDB.AddAttribute(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateAttribute(ProductAttribute data)
        {
            return productDB.UpdateAttribute(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        public static bool DeleteAttribute(long attributeID)
        {
            return productDB.DeleteAttribute(attributeID);
        }
    }
}
