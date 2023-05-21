using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021289.DomainModels;
using _19T1021289.DataLayers;
using _19T1021289.BusinessLayers;
using _19T1021289.Web.Models;
namespace _19T1021289.Web.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string SUPPLIER_SEARCH = "SearchSupplierCondition";

        ///
        public ActionResult Index()
        {
            PaginationSearchInput condition = Session[SUPPLIER_SEARCH] as PaginationSearchInput;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Search(PaginationSearchInput condition)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfSuppliers(condition.Page, 
                                                        condition.PageSize, 
                                                        condition.SearchValue, 
                                                        out rowCount);
            var result = new SupplierSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session[SUPPLIER_SEARCH] = condition;
            return View(result);
        }

        /// <summary>
        /// Bổ sung nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Supplier()
            {
                SupplierID = 0
            };
            ViewBag.Title = "Bổ sung nhà cung cấp";
            return View("Edit", data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        // nếu nhập bậy (edit/12dss) mà class edit(string id) thì sẽ lỗi ngay lời gọi hàm
        public ActionResult Edit(int id=0)
        {
            try
            {
                if (id == 0)
                    return RedirectToAction("Index");

                int supplierID = Convert.ToInt32(id);
                var data = CommonDataService.GetSupplier(supplierID);
                // nếu như id=100 thì data sẽ =null
                if (data == null)
                    return RedirectToAction("Index");

                ViewBag.Title = "Cập nhật nhà cung cấp";
                return View(data);
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
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken] // dam bao tranh tan cong ben ngoai
        public ActionResult Save(Supplier data) 
        {
            try
            {
                // kiểm soát đầu vào
                if(string.IsNullOrWhiteSpace(data.SupplierName))
                    ModelState.AddModelError("SupplierName", "Tên không được để trống");
                if (string.IsNullOrEmpty(data.ContactName))
                    ModelState.AddModelError("ContactName", "Tên liên hệ không được để trống");
                if (string.IsNullOrWhiteSpace(data.Country))
                    ModelState.AddModelError("Country", "Tên quốc gia không được để trống");
                if (string.IsNullOrWhiteSpace(data.City))
                    ModelState.AddModelError("City", "Tên thành phố không được để trống");
                if (string.IsNullOrWhiteSpace(data.Address))
                    ModelState.AddModelError("Address", "Địa chỉ không được để trống");
                if (string.IsNullOrWhiteSpace(data.Phone))
                    ModelState.AddModelError("Phone", "Số điện thoại không được để trống");
                if (string.IsNullOrWhiteSpace(data.PostalCode))
                    ModelState.AddModelError("PostalCode", "Mã bưu chính không được để trống");

                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.SupplierID == 0 ? "Bổ sung nhà cung cấp" : "Cập nhật nhà cung cấp";
                    return View("Edit", data);
                }
                if (data.SupplierID == 0)
                {
                    CommonDataService.AddSupplier(data);
                }
                else
                {
                    CommonDataService.UpdateSupplier(data);
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

            int supplierID = Convert.ToInt32(id);
            if (Request.HttpMethod== "GET")
            {
                var data = CommonDataService.GetSupplier(supplierID);
                return View(data);
            } else
            {
                CommonDataService.DeleteSupplier(supplierID);
                return RedirectToAction("Index");
            }
        }
    }
}