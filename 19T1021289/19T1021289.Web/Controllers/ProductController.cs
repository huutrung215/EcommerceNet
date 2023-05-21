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
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("product")]
    public class ProductController : Controller
    {
        private const int PAGE_SIZE = 10;
        private const string PRODUCT_SEARCH = "SearchProductCondition";

        /// <summary>
        /// Tìm kiếm, hiển thị mặt hàng dưới dạng phân trang
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(ProductSearchInput input)
        {
            

            ProductSearchInput condition = Session[PRODUCT_SEARCH] as ProductSearchInput;
            if (condition == null)
            {
                condition = new ProductSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = "",
                    SelectedSupplierId = input.SelectedSupplierId,
                    SelectedCategoryId = input.SelectedCategoryId
                };
            }

            return View(condition);
        }


        public ActionResult Search(ProductSearchInput condition)
        {
            int rowCount = 0;
            var data = ProductDataService.ListProducts(condition.Page,
                                                        condition.PageSize,
                                                        condition.SearchValue,
                                                        condition.SelectedCategoryId.CategoryID,
                                                        condition.SelectedSupplierId.SupplierID,
                                                        out rowCount);
            var result = new ProductSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                SelectedCategoryId = condition.SelectedCategoryId.CategoryID,
                SelectedSupplierId = condition.SelectedSupplierId.SupplierID,
                RowCount = rowCount,
                Data = data
            };
            Session[PRODUCT_SEARCH] = condition;
            return View(result);
        }

        /// <summary>
        /// Tạo mặt hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new ProductViewModel()
            {
                Product = null
            };
            ViewBag.Title = "Bổ sung mặt hàng";
            return View(data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken] // dam bao tranh tan cong ben ngoai
        public ActionResult SaveProduct(ProductViewModel data, HttpPostedFileBase uploadPhoto)
        {

            // kiểm soát đầu vào
            if (string.IsNullOrWhiteSpace(data.Product.ProductName))
                ModelState.AddModelError("ProductName", "Tên không được để trống");
            if (string.IsNullOrEmpty(data.Product.CategoryID.ToString()))
                ModelState.AddModelError("CategoryID", "Loại hàng không được để trống");
            if (string.IsNullOrWhiteSpace(data.Product.SupplierID.ToString()))
                ModelState.AddModelError("SupplierID", "Nhà cung cấp không được để trống");
            if (string.IsNullOrWhiteSpace(data.Product.Unit))
                ModelState.AddModelError("Unit", "Đơn vị tính không được để trống");
            if (string.IsNullOrWhiteSpace(data.Product.Price.ToString()))
                ModelState.AddModelError("Price", "Gía không được để trống");
            if (string.IsNullOrWhiteSpace(data.Product.Photo))
                data.Product.Photo = "";

            if (!ModelState.IsValid)
            {
                if (data.Product.ProductID == 0)
                {
                    ViewBag.Title = "Bổ sung mặt hàng";
                    return View("Create", data);
                }    
                else
                {
                    ViewBag.Title = "Cập nhật mặt hàng";
                    return View("Edit", data);
                }             
            }
            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Photo");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);
                data.Product.Photo = fileName;
            }
            if (data.Product.ProductID == 0)
            {
                ProductDataService.AddProduct(data.Product);
            }
            else
            {
                ProductDataService.UpdateProduct(data.Product);
            }
            return RedirectToAction($"Edit/{data.Product.ProductID}");
        }

    
        /// <summary>
        /// Cập nhật thông tin mặt hàng, 
        /// Hiển thị danh sách ảnh và thuộc tính của mặt hàng, điều hướng đến các chức năng
        /// quản lý ảnh và thuộc tính của mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        public ActionResult Edit(int id = 0)
        {
            try
            {
                if (id == 0)
                    return RedirectToAction("Index");

                int productID = Convert.ToInt32(id);

                var product = ProductDataService.GetProduct(productID);
                var photos = ProductDataService.ListPhotos(productID);
                var attributes = ProductDataService.ListAttributes(productID);

                var data = new ProductViewModel
                {
                    Product = product,
                    Photos = photos,
                    Attributes = attributes
                };

                // nếu như id=100 thì data sẽ =null
                if (product == null)
                    return RedirectToAction("Index");

                ViewBag.Title = "Cập nhật mặt hàng";
                return View(data);
            }
            catch (Exception ex)
            {
                //ghi lại log lỗi
                return Content("Có lỗi xảy ra, Vui lòng thử lại!");
            }
        }

        /// <summary>
        /// Xóa mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        public ActionResult Delete(int id = 0)
        {
            if (id <= 0)
                return RedirectToAction("Index");

            int productID = Convert.ToInt32(id);
            if (Request.HttpMethod == "GET")
            {
                var data = ProductDataService.GetProduct(productID);
                return View(data);
            }
            else
            {
                ProductDataService.DeleteProduct(productID);
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Các chức năng quản lý ảnh của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="photoID"></param>
        /// <returns></returns>
        [Route("photo/{method?}/{productID?}/{photoID?}")]
        public ActionResult Photo(string method = "add", int productID = 0, long photoID = 0)
        {
            switch (method)
            {
                case "add":
                    var pho = ProductDataService.GetProduct(productID);
                    var data = new ProductPhoto()
                    {
                        PhotoID = 0,
                        ProductID = pho.ProductID,
                    };
                    ViewBag.Title = "Bổ sung ảnh";
                    return View("EditPhoto", data);
                case "edit":
                    var product = ProductDataService.GetProduct(productID);
                    var photo = ProductDataService.GetPhoto(photoID);
                    if (product == null || photo == null)
                    {
                        return RedirectToAction("Index");
                    }

                    var data2 = new ProductPhoto
                    {
                        PhotoID = photo.PhotoID,
                        ProductID = photo.ProductID,
                        Photo = photo.Photo,
                        Description = photo.Description,
                        DisplayOrder = photo.DisplayOrder,
                        IsHidden= photo.IsHidden
                    };
                    ViewBag.Title = "Thay đổi ảnh";
                    return View("EditPhoto",data2);
                case "delete":
                    var product1 = ProductDataService.GetProduct(productID);
                    var photo1 = ProductDataService.GetPhoto(photoID);
                    if (product1 == null || photo1 == null)
                    {
                        return RedirectToAction("Index");
                    }
                    long photoid = Convert.ToInt32(photo1.PhotoID);
                    // Delete the photo
                    ProductDataService.DeletePhoto(photoid);

                    // 
                    return RedirectToAction($"Edit/{photo1.ProductID}");
                default:
                    return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult SavePhoto(ProductPhoto data, HttpPostedFileBase uploadPhoto)
        {
            if (string.IsNullOrWhiteSpace(data.Photo))
                data.Photo = "";
            if (string.IsNullOrWhiteSpace(data.Description))
                ModelState.AddModelError("Description", "Mô tả không được để trống");
            if (string.IsNullOrEmpty(data.DisplayOrder.ToString()))
                ModelState.AddModelError("CategoryID", "Loại hàng không được để trống");
            if (string.IsNullOrWhiteSpace(data.IsHidden.ToString()))
                ModelState.AddModelError("SupplierID", "Nhà cung cấp không được để trống");

            if (!ModelState.IsValid)
            {
                ViewBag.Title = data.PhotoID == 0 ? "Bổ sung hình ảnh" : "Cập nhật hình ảnh";
                return View("EditPhoto",data);
            }
            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Photo");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);
                data.Photo = fileName;
            }
            if (data.PhotoID == 0)
            {
                ProductDataService.AddPhoto(data);
            }
            else
            {
                ProductDataService.UpdatePhoto(data);
            }
            return RedirectToAction($"Edit/{data.ProductID}");
        }

        /// <summary>
        /// Các chức năng quản lý thuộc tính của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        [Route("attribute/{method?}/{productID}/{attributeID?}")]
        public ActionResult Attribute(string method = "add", int productID = 0, int attributeID = 0)
        {
            switch (method)
            {
                case "add":
                    var attr = ProductDataService.GetProduct(productID);
                    var data = new ProductAttribute()
                    {
                       AttributeID  = 0,
                        ProductID = attr.ProductID,
                    };
                    ViewBag.Title = "Bổ sung thuộc tính";
                    return View("EditAttribute", data);
                case "edit":
                    var product = ProductDataService.GetProduct(productID);
                    var attribute = ProductDataService.GetAttribute(attributeID);
                    if (product == null || attribute == null)
                    {
                        return RedirectToAction("Index");
                    }

                    var data2 = new ProductAttribute
                    {
                        AttributeID = attribute.AttributeID,
                        ProductID = attribute.ProductID,
                        AttributeName = attribute.AttributeName,
                        AttributeValue= attribute.AttributeValue,
                        DisplayOrder = attribute.DisplayOrder,
                    };
                    ViewBag.Title = "Thay đổi thuộc tính";
                    return View("EditAttribute", data2);
                case "delete":
                    var product1 = ProductDataService.GetProduct(productID);
                    var attribute1 = ProductDataService.GetAttribute(attributeID);
                    if (product1 == null || attribute1 == null)
                    {
                        return RedirectToAction("Index");
                    }
                    long attrid = Convert.ToInt32(attribute1.AttributeID);
                    // Delete the photo
                    ProductDataService.DeleteAttribute(attrid);

                    // 
                    return RedirectToAction("Edit", new { productID = productID });
                    //ProductDataService.DeleteAttribute(attributeID);
                   /* return RedirectToAction($"Edit/{productID}"); *///return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult SaveAttribute(ProductAttribute data)
        {
            if (string.IsNullOrWhiteSpace(data.AttributeName))
                ModelState.AddModelError("AttributeName", "Tên thuộc tính không được để trống");
            if (string.IsNullOrEmpty(data.AttributeValue))
                ModelState.AddModelError("AttributeValue", "Gía trị thuộc tính không được để trống");
            if (string.IsNullOrEmpty(data.DisplayOrder.ToString()))
                ModelState.AddModelError("DisplayOrder", "Thứ tự hiển thị không được để trống");

            if (!ModelState.IsValid)
            {
                ViewBag.Title = data.AttributeID == 0 ? "Bổ sung thuộc tính" : "Cập nhật thuộc tính";
                return View("EditAttribute",data);
            }
            
            if (data.AttributeID == 0)
            {
                ProductDataService.AddAttribute(data);
            }
            else
            {
                ProductDataService.UpdateAttribute(data);
            }
            /*return RedirectToAction("Edit", "Product", new { id = data.ProductID });*/
            return RedirectToAction($"Edit/{data.ProductID}");
        }
    }
}