using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021289.DomainModels;

namespace _19T1021289.DataLayers
{
    /// <summary>
    /// Dinh nghia cac phep xu ly du lieu tren nha cung cap
    /// su dung cach nay dan den viett lap di lap lai cac kieu code giong nhau
    /// cho cac doi tuong du lieu tuong tu nhu customer, employee,...
    /// => dung cach viet Generic
    /// </summary>
    public interface ISupplierDAL
    {
        /// <summary>
        /// tìm kiếm và lấy danh sách các nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <param name="page">trang cần hiển thị</param>
        /// <param name="pageSize">số dòng hiển thị trên mỗi trang (0 tức là ko yêu cầu phải phân trang)</param>
        /// <param name="searchValue">Tên cần tìm kiếm (chuỗi rỗng nếu ko tìm kiếm theo tên)</param>
        /// <returns></returns>
        IList<Supplier> List(int page = 1, int pageSize = 0, string searchValue = "");

        /// <summary>
        /// đếm số nhà cung cấp tìm được
        /// </summary>
        /// <param name="searchValue">Tên cần tìm kiếm (chuỗi rỗng nếu ko tìm kiếm theo tên)</param>
        /// <returns></returns>
        int Count(String searchValue = "");

        /// <summary>
        /// Bổ sung thêm 1 nhà cung cấp vào cơ sở dữ liệu
        /// </summary>
        /// <param name="data">Đối tượng nhà cung cấp</param>
        /// <returns>ID của nhà cung cấp đc tạo mới</returns>
       

        /// <summary>
        /// lấy thong tin của 1 ncc
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        Supplier Get(int supplierID);
        int Add(Supplier data);

        /// <summary>
        /// Cập nhật thông tin của ncc
        /// </summary>
        /// <param name="data">Đối tượng nhà cung cấp</param>
        /// <returns></returns>
        bool Update(Supplier data);

        /// <summary>
        /// xóa ncc dựa vào mã ncc
        /// </summary>
        /// <param name="supplierID">Mã ncc cần xóa</param>
        /// <returns></returns>
        bool Delete(int supplierID);


        /// <summary>
        /// kiểm tra xem ncc hiện có dữ liệu liên quan hay ko?
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        bool InUsed(int supplierID);
    }
}
