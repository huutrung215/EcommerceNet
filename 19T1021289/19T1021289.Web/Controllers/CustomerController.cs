using _19T1021289.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using _19T1021289.DomainModels;
using _19T1021289.DataLayers;
using _19T1021289.BusinessLayers;
using _19T1021289.Web.Models;

namespace _19T1021289.Web.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string CUSTOMER_SEARCH = "SearchCustomerCondition";
        // GET: Supplier
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public ActionResult Index()
        {
            PaginationSearchInput condition = Session[CUSTOMER_SEARCH] as PaginationSearchInput;
            if (condition == null)
            {
                condition = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = ""
                };
            }

            return View(condition);
        }

        public ActionResult Search(PaginationSearchInput condition)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfCustomers(condition.Page,
                                                        condition.PageSize,
                                                        condition.SearchValue,
                                                        out rowCount);
            var result = new CustomerSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session[CUSTOMER_SEARCH] = condition;
            return View(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Customer()
            {
                CustomerID = 0
            };
            ViewBag.Title = "Thêm khách hàng";
            return View("Edit", data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            try
            {
                ViewBag.Title = "Sửa thông tin khách hàng";

                if (id == 0)
                    return RedirectToAction("Index");

                int customerID = Convert.ToInt32(id);
                var data = CommonDataService.GetCustomer(customerID);

                if (data == null)
                    return RedirectToAction("Index");

                return View(data);
            }
            catch (Exception ex)
            {
                //ghi lại log lỗi
                return Content("Có lỗi xảy ra, Vui lòng thử lại!");
            }      
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // dam bao tranh tan cong ben ngoai
        public ActionResult Save(Customer data)
        {
            try
            {
                // kiểm soát đầu vào
                if (string.IsNullOrWhiteSpace(data.CustomerName))
                    ModelState.AddModelError("CustomerName", "Tên không được để trống");
                if (string.IsNullOrEmpty(data.ContactName))
                    ModelState.AddModelError("ContactName", "Tên liên hệ không được để trống");
                if (string.IsNullOrWhiteSpace(data.Country))
                    ModelState.AddModelError("Country", "Tên quốc gia không được để trống");
                if (string.IsNullOrWhiteSpace(data.City))
                    ModelState.AddModelError("City", "Tên thành phố không được để trống");
                if (string.IsNullOrWhiteSpace(data.Address))
                    ModelState.AddModelError("Address", "Địa chỉ không được để trống");
                if (string.IsNullOrWhiteSpace(data.Email))
                    ModelState.AddModelError("Email", "Email không được để trống");
                if (string.IsNullOrWhiteSpace(data.PostalCode))
                    ModelState.AddModelError("PostalCode", "Mã bưu chính không được để trống");

                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.CustomerID == 0 ? "Bổ sung khách hàng" : "Cập nhật khách hàng";
                    return View("Edit", data);
                }
                if (data.CustomerID == 0)
                {
                    CommonDataService.AddCustomer(data);
                }
                else
                {
                    CommonDataService.UpdateCustomer(data);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //ghi lại log lỗi
                return Content("Có lỗi xảy ra, Vui lòng thử lại!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
        {
            if (id <= 0)
                return RedirectToAction("Index");

            int customerID = Convert.ToInt32(id);
            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetCustomer(customerID);
                return View(data);
            }
            else
            {
                CommonDataService.DeleteCustomer(customerID);
                return RedirectToAction("Index");
            }
        }
    }
}