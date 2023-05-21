using _19T1021289.BusinessLayers;
using _19T1021289.DomainModels;
using _19T1021289.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021289.Web.Controllers
{
    [Authorize]
    public class ShipperController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string SHIPPER_SEARCH = "SearchShipperCondition";

        ///
        public ActionResult Index()
        {
            PaginationSearchInput condition = Session[SHIPPER_SEARCH] as PaginationSearchInput;
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
            var data = CommonDataService.ListOfShippers(condition.Page,
                                                        condition.PageSize,
                                                        condition.SearchValue,
                                                        out rowCount);
            var result = new ShipperSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session[SHIPPER_SEARCH] = condition;
            return View(result);
        }

        public ActionResult Create()
        {
            var data = new Shipper()
            {
                ShipperID = 0
            };
            ViewBag.Title = "Bổ sung người giao hàng";
            return View("Edit",data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public ActionResult Edit(int id = 0)
        {
            try
            {
                if (id == 0)
                    return RedirectToAction("Index");

                int shipperID = Convert.ToInt32(id);
                var data = CommonDataService.GetShipper(shipperID);
                // nếu như id=100 thì data sẽ =null
                if (data == null)
                    return RedirectToAction("Index");

                ViewBag.Title = "Cập nhật người giao hàng";

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
        public ActionResult Save(Shipper data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.ShipperName))
                    ModelState.AddModelError("ShipperName", "Tên không được để trống");
                if (string.IsNullOrWhiteSpace(data.Phone))
                    ModelState.AddModelError("Phone", "Số điện thoại không được để trống");


                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.ShipperID == 0 ? "Bổ sung người giao hàng" : "Cập nhật người giao hàng";
                    return View("Edit", data);
                }
                if (data.ShipperID == 0)
                {
                    CommonDataService.AddShipper(data);
                }
                else
                {
                    CommonDataService.UpdateShipper(data);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                //ghi lại log lỗi
                return Content("Có lỗi xảy ra, Vui lòng thử lại!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id=0)
        {
            if (id <= 0)
                return RedirectToAction("Index");

            int shipperID = Convert.ToInt32(id);
            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetShipper(shipperID);
                return View(data);
            }
            else
            {
                CommonDataService.DeleteShipper(shipperID);
                return RedirectToAction("Index");
            }
        }
    }
}