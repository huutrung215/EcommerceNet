using _19T1021289.DomainModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021289.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép dữ liệu chung cho các dữ liệu
    /// đơn giản trên bảng
    /// </summary>
    public interface ICommonDAL<T> where T : class
    {
        /// <summary>
        /// tìm kiếm và lấy danh sách các dữ liệu dạng phân trang
        /// </summary>
        /// <param name="page">trang cần hiển thị</param>
        /// <param name="pageSize">số dòng hiển thị trên mỗi trang (0 tức là ko yêu cầu phải phân trang)</param>
        /// <param name="searchValue">Tên cần tìm kiếm (chuỗi rỗng nếu ko tìm kiếm theo tên)</param>
        /// <returns></returns>
        IList<T> List(int page = 1, int pageSize = 0, string searchValue = "");

        /// <summary>
        /// đếm số  dữ liệu tìm được
        /// </summary>
        /// <param name="searchValue">Tên cần tìm kiếm (chuỗi rỗng nếu ko tìm kiếm theo tên)</param>
        /// <returns></returns>
        int Count(String searchValue = "");

        /// <summary>
        /// Bổ sung thêm 1  dữ liệu vào cơ sở dữ liệu
        /// </summary>
        /// <param name="data">Đối tượng nhà cung cấp</param>
        /// <returns>ID của nhà cung cấp đc tạo mới</returns>


        /// <summary>
        /// lấy thong tin của 1  dữ liệu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);

        IEnumerable<T> Get();
        int Add(T data);

        /// <summary>
        /// Cập nhật thông tin của  dữ liệu
        /// </summary>
        /// <param name="data">Đối tượng của dữ liệu</param>
        /// <returns></returns>
        bool Update(T data);

        /// <summary>
        /// xóa ncc dựa vào mã  dữ liệu
        /// </summary>
        /// <param name="id">ID của dữ liệu cần xóa</param>
        /// <returns></returns>
        bool Delete(int id);


        /// <summary>
        /// kiểm tra xem dữ liệu hiện có dữ liệu liên quan hay ko?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool InUsed(int id);
    }
}

