using _19T1021289.BusinessLayers;
using _19T1021289.DomainModels;
using _19T1021289.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using _19T1021289.Web;


namespace _19T1021289.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private const int PAGE_SIZE = 6;
        private const string EMPLOYEE_SEARCH = "SearchEmployeeCondition";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            PaginationSearchInput condition = Session[EMPLOYEE_SEARCH] as PaginationSearchInput;
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
            var data = CommonDataService.ListOfEmployees(condition.Page,
                                                        condition.PageSize,
                                                        condition.SearchValue,
                                                        out rowCount);
            var result = new EmployeeSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session[EMPLOYEE_SEARCH] = condition;
            return View(result);
        }

        public ActionResult Create()
        {
            var data = new Employee()
            {
                EmployeeID = 0
            };
            ViewBag.Title = "Bổ sung nhân viên";
            return View("Edit", data);
        }


        public ActionResult Edit(int id = 0)
        {
            try
            {
                if (id == 0)
                    return RedirectToAction("Index");

                int employeeID = Convert.ToInt32(id);
                var data = CommonDataService.GetEmployee(employeeID);
                // nếu như id=100 thì data sẽ =null
                if (data == null)
                    return RedirectToAction("Index");

                ViewBag.Title = "Sửa thông tin nhân viên";
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
        public ActionResult Save(Employee data, string birthday, HttpPostedFileBase uploadPhoto)
        {
           
            DateTime? d = SelectListHelper.DMYStringToDateTime(birthday);
            if (d == null)
                ModelState.AddModelError("BirthDate", "Ngày sinh không được để trống");
            else
                data.BirthDate = d.Value;

            // kiểm soát đầu vào
            if (string.IsNullOrWhiteSpace(data.LastName))
                ModelState.AddModelError("LastName", "Họ được để trống");
            if (string.IsNullOrEmpty(data.FirstName))
                ModelState.AddModelError("FirstName", "Tên không được để trống");
            if (string.IsNullOrWhiteSpace(data.BirthDate.ToLongDateString()))
                ModelState.AddModelError("BirthDate", "Ngày sinh không được để trống");
            if (string.IsNullOrWhiteSpace(data.Email))
                ModelState.AddModelError("Email", "Email không được để trống");
            if (string.IsNullOrWhiteSpace(data.Notes))
                ModelState.AddModelError("Notes", "Ghi chú không được để trống");
            if (string.IsNullOrWhiteSpace(data.Photo))
                data.Photo = "";
                /*ModelState.AddModelError("Photo", "Ảnh không được để trống");*/

            if (!ModelState.IsValid)
            {
                ViewBag.Title = data.EmployeeID == 0 ? "Bổ sung nhân viên" : "Cập nhật nhân viên";
                return View("Edit", data);
            }

            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Photo");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);
                data.Photo = fileName;
            }

            if (data.EmployeeID == 0)
            {
                CommonDataService.AddEmployee(data);
            }
            else
            {
                CommonDataService.UpdateEmployee(data);
            }
            return RedirectToAction("Index");
        }
            

        public ActionResult Delete(int id = 0)
        {
            if (id <= 0)
                return RedirectToAction("Index");

            int employeeID = Convert.ToInt32(id);
            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetEmployee(employeeID);
                return View(data);
            }
            else
            {
                CommonDataService.DeleteEmployee(employeeID);
                return RedirectToAction("Index");
            }
        }
    }
}