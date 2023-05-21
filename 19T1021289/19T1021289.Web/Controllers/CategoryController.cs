using _19T1021289.BusinessLayers;
using _19T1021289.DomainModels;
using _19T1021289.Web.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace _19T1021289.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string CATEGORY_SEARCH = "SearchCategoryCondition";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            PaginationSearchInput condition = Session[CATEGORY_SEARCH] as PaginationSearchInput;
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
            var data = CommonDataService.ListOfCategories(condition.Page,
                                                        condition.PageSize,
                                                        condition.SearchValue,
                                                        out rowCount);
            var result = new CategorySearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session[CATEGORY_SEARCH] = condition;
            return View(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Category()
            {
                CategoryID = 0
            };
            ViewBag.Title = "Bổ sung loại hàng";
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
                if (id == 0)
                    return RedirectToAction("Index");

                int categoryID = Convert.ToInt32(id);
                var data = CommonDataService.GetCategory(categoryID);

                if (data == null)
                    return RedirectToAction("Index");

                ViewBag.Title = "Sửa loại hàng";
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
        public ActionResult Save(Category data)
        {
            try
            {
                // kiểm soát đầu vào
                if (string.IsNullOrWhiteSpace(data.CategoryName))
                    ModelState.AddModelError("CategoryName", "Tên không được để trống");
                if (string.IsNullOrEmpty(data.Description))
                    ModelState.AddModelError("Description", "Thông tin không được để trống");

                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.CategoryID == 0 ? "Bổ sung nhà cung cấp" : "Cập nhật nhà cung cấp";
                    return View("Edit", data);
                }
                if (data.CategoryID == 0)
                {
                    CommonDataService.AddCategory(data);
                }
                else
                {
                    CommonDataService.UpdateCategory(data);
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

            int categoryID = Convert.ToInt32(id);
            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetCategory(categoryID);
                return View(data);
            }
            else
            {
                CommonDataService.DeleteCategory(categoryID);
                return RedirectToAction("Index");
            }
        }

    }
}