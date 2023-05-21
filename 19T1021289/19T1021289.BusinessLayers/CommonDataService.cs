using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021289.DataLayers;
using _19T1021289.DomainModels;
using System.Configuration;
using _19T1021289.DataLayers.SQLServer;

namespace _19T1021289.BusinessLayers
{
    /// <summary>
    /// các chức năng nghiệp vụ liên quan đến nhà cc, khách hàng,..
    /// </summary>
    public static class CommonDataService
    {
        private static ICountryDAL countryDB;
        public static ICommonDAL<Supplier> supplierDB;
        public static ICommonDAL<Shipper> shipperDB;
        public static ICommonDAL<Customer> customerDB;
        public static ICommonDAL<Employee> employeeDB;
        public static ICommonDAL<Category> categoryDB;
        public static IProductDAL productDB;

        /// <summary>
        /// Ctor
        /// </summary>
        static CommonDataService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            countryDB = new DataLayers.SQLServer.CountryDAL(connectionString);
            supplierDB = new DataLayers.SQLServer.SupplierDAL(connectionString);
            shipperDB = new DataLayers.SQLServer.ShipperDAL(connectionString);
            customerDB = new DataLayers.SQLServer.CustomerDAL(connectionString);
            employeeDB = new DataLayers.SQLServer.EmployeeDAL(connectionString);
            categoryDB = new DataLayers.SQLServer.CategoryDAL(connectionString);
            productDB = new DataLayers.SQLServer.ProductDAL(connectionString);
        }

        /// <summary>
        /// Lay danh sach quoc gia
        /// </summary>
        /// <returns></returns>
        public static List<Country> ListOfCountries()
        {
            return countryDB.List().ToList();
        }

        /// <summary>
        /// tìm kiếm, lấy danh sách các nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">số dòng trên mỗi trang(=0 nếu ko tìm đc)</param>
        /// <param name="searchValue">Gía trị tìm kiếm</param>
        /// <param name="rowCount">Output tổng số dòng tìm đc</param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = supplierDB.List().Count();
            return supplierDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(string searchValue)
        {
            return supplierDB.List(1,0,searchValue).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers()
        {
            return supplierDB.List().ToList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddSupplier(Supplier data)
        {
            return supplierDB.Add(data);
        }

        public static bool UpdateSupplier(Supplier data)
        {
            return supplierDB.Update(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool DeleteSupplier(int  supplierID)
        {
            return supplierDB.Delete(supplierID);
        }

        /// <summary>
        /// lay thong tin cua 1 nha cung cap
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static Supplier GetSupplier(int supplierID)
        {
            return supplierDB.Get(supplierID);
        }

        /// <summary>
        /// kiem tra xem 1 nha cung cap hien co du lieu lien quan hay ko?
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool InUsedSupplier (int supplierID)
        {
            return supplierDB.InUsed(supplierID);
        }

        //====================================================
        // CUSTOMER



        /// <summary>
        /// tìm kiếm, lấy danh sách các nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">số dòng trên mỗi trang(=0 nếu ko tìm đc)</param>
        /// <param name="searchValue">Gía trị tìm kiếm</param>
        /// <param name="rowCount">Output tổng số dòng tìm đc</param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = customerDB.List().Count();
            return customerDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(string searchValue ="")    
        {
            return customerDB.List(1, 0, searchValue).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCustomer(Customer data)
        {
            return customerDB.Add(data);
        }

        public static bool UpdateCustomer(Customer data)
        {
            return customerDB.Update(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool DeleteCustomer(int customerID)
        {
            return customerDB.Delete(customerID);
        }

        /// <summary>
        /// lay thong tin cua 1 nha cung cap
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static Customer GetCustomer(int customerID)
        {
            return customerDB.Get(customerID);
        }

        public static IEnumerable<Customer> ListOfCustomer()
        {
            return customerDB.Get();
        }

        /// <summary>
        /// kiem tra xem 1 nha cung cap hien co du lieu lien quan hay ko?
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool InUsedCustomer(int customerID)
        {
            return customerDB.InUsed(customerID);
        }


        //====================================================
        // EMPLOYEE

        /// <summary>
        /// tìm kiếm, lấy danh sách các nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">số dòng trên mỗi trang(=0 nếu ko tìm đc)</param>
        /// <param name="searchValue">Gía trị tìm kiếm</param>
        /// <param name="rowCount">Output tổng số dòng tìm đc</param>
        /// <returns></returns>
        public static List<Employee> ListOfEmployees(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = employeeDB.List().Count();
            return employeeDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Employee> ListOfEmployees(string searchValue = "")
        {
            return employeeDB.List(1, 0, searchValue).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddEmployee(Employee data)
        {
            return employeeDB.Add(data);
        }

        public static bool UpdateEmployee(Employee data)
        {
            return employeeDB.Update(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool DeleteEmployee(int employeeID)
        {
            return employeeDB.Delete(employeeID);
        }

        /// <summary>
        /// lay thong tin cua 1 nha cung cap
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static Employee GetEmployee(int employeeID)
        {
            return employeeDB.Get(employeeID);
        }

        public static IEnumerable<Employee> ListOfEmployee()
        {
            return employeeDB.Get();
        }

        /// <summary>
        /// kiem tra xem 1 nha cung cap hien co du lieu lien quan hay ko?
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool InUsedEmployee(int employeeID)
        {
            return employeeDB.InUsed(employeeID);
        }



        //====================================================
        // SHIPPER

        /// <summary>
        /// tìm kiếm, lấy danh sách các nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">số dòng trên mỗi trang(=0 nếu ko tìm đc)</param>
        /// <param name="searchValue">Gía trị tìm kiếm</param>
        /// <param name="rowCount">Output tổng số dòng tìm đc</param>
        /// <returns></returns>
        public static List<Shipper> ListOfShippers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = shipperDB.List().Count();
            return shipperDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Shipper> ListOfShippers(string searchValue)
        {
            return shipperDB.List(1, 0, searchValue).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddShipper(Shipper data)
        {
            return shipperDB.Add(data);
        }

        public static bool UpdateShipper(Shipper data)
        {
            return shipperDB.Update(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool DeleteShipper(int shipperID)
        {
            return shipperDB.Delete(shipperID);
        }

        /// <summary>
        /// lay thong tin cua 1 nha cung cap
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static Shipper GetShipper(int shipperID)
        {
            return shipperDB.Get(shipperID);
        }

        public static IEnumerable<Shipper> ListOfShippers()
        {
            return shipperDB.Get();
        }

        /// <summary>
        /// kiem tra xem 1 nha cung cap hien co du lieu lien quan hay ko?
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool InUsedShipper(int shipperID)
        {
            return shipperDB.InUsed(shipperID);
        }



        //====================================================
        // CATEGORY

        /// <summary>
        /// tìm kiếm, lấy danh sách các nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">số dòng trên mỗi trang(=0 nếu ko tìm đc)</param>
        /// <param name="searchValue">Gía trị tìm kiếm</param>
        /// <param name="rowCount">Output tổng số dòng tìm đc</param>
        /// <returns></returns>
        public static List<Category> ListOfCategories(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = categoryDB.List().Count();
            return categoryDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Category> ListOfCategories(string searchValue)
        {
            return categoryDB.List(1, 0, searchValue).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Category> ListOfCategories()
        {
            return categoryDB.List().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCategory(Category data)
        {
            return categoryDB.Add(data);
        }

        public static bool UpdateCategory(Category data)
        {
            return categoryDB.Update(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static bool DeleteCategory(int categoryID)
        {
            return categoryDB.Delete(categoryID);
        }

        /// <summary>
        /// lay thong tin cua 1 nha cung cap
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static Category GetCategory(int categoryID)
        {
            return categoryDB.Get(categoryID);
        }

        /// <summary>
        /// kiem tra xem 1 nha cung cap hien co du lieu lien quan hay ko?
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static bool InUsedCategory(int categoryID)
        {
            return categoryDB.InUsed(categoryID);
        }


        //=======================================
        // PRODUCT

        /// <summary>
        /// tìm kiếm, lấy danh sách các nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">số dòng trên mỗi trang(=0 nếu ko tìm đc)</param>
        /// <param name="searchValue">Gía trị tìm kiếm</param>
        /// <param name="rowCount">Output tổng số dòng tìm đc</param>
        /// <returns></returns>
        public static List<Product> ListOfProducts(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = productDB.List().Count();
            return productDB.List(page, pageSize, searchValue).ToList();
        }

        public static List<Product> ListOfProducts()
        {
            return productDB.List().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Product> ListOfProducts(string searchValue)
        {
            return productDB.List(1, 0, searchValue).ToList();
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
        /// lay thong tin cua 1 nha cung cap
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static Product GetProduct(int productID)
        {
            return productDB.Get(productID);
        }

        /// <summary>
        /// kiem tra xem 1 nha cung cap hien co du lieu lien quan hay ko?
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static bool InUsedProduct(int productID)
        {
            return productDB.InUsed(productID);
        }
    }
}
