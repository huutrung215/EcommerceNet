using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using _19T1021289.BusinessLayers;
using _19T1021289.DomainModels;
using Antlr.Runtime.Misc;
using Newtonsoft.Json.Linq;

namespace _19T1021289.Web
{
    /// <summary>
    /// cung cấp hàm tiện ích liên quan đến SelectList
    /// </summary>
    public static class SelectListHelper
    {
        public static List<SelectListItem> Countries()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value= "",
                Text = "-- Chọn quốc gia --"
            });
            foreach (var item in CommonDataService.ListOfCountries())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CountryName,
                    Text = item.CountryName
                });
            }

            return list;
        }

        public static List<SelectListItem> Categories()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "0",
                Text = "-- Chọn loại hàng --"
            });
            foreach (var i in CommonDataService.ListOfCategories())
            {
                list.Add(new SelectListItem()
                {
                    Value = i.CategoryID.ToString(),
                    Text = i.CategoryName
                });
            }
            return list;
        }

        public static List<SelectListItem> Suppliers()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "0",
                Text = "-- Chọn nhà cung cấp --"
            });
            foreach (var i in CommonDataService.ListOfSuppliers())
            {
                list.Add(new SelectListItem()
                {
                    Value = i.SupplierID.ToString(),
                    Text = i.SupplierName
                });
            }
            return list;
        }

        public static List<SelectListItem> Shippers()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "0",
                Text = "-- Chọn người giao hàng  --"
            });
            foreach(var i in CommonDataService.ListOfShippers())
            {
                list.Add(new SelectListItem()
                {
                    Value = i.ShipperID.ToString(),
                    Text = i.ShipperName
                });
            }
            return list;
        }


        public static List<SelectListItem> Statuses()
        {
            var statusDict = new Dictionary<int, string>()
            {
                { OrderStatus.INIT, "Đơn hàng mới (chờ duyệt)" },
                { OrderStatus.ACCEPTED, "Đơn hàng đã duyệt (chờ chuyển hàng)" },
                { OrderStatus.SHIPPING, "Đơn hàng đang được giao" },
                { OrderStatus.FINISHED, "Đơn hàng đã hoàn tất thành công" },
                { OrderStatus.CANCEL, "Đơn hàng bị hủy" },
                { OrderStatus.REJECTED, "Đơn hàng bị từ chối" }
            };

            var statusList = new List<SelectListItem>();
            statusList.Add(new SelectListItem()
            {
                Value = "0",
                Text = "-- Trạng thái --"
            });

            foreach (var status in statusDict)
            {
                var item = new SelectListItem()
                {
                    Value = status.Key.ToString(),
                    Text = status.Value
                };

                statusList.Add(item);
            }

            return statusList;
        }

       

        public static DateTime? DMYStringToDateTime(string s, string format = "d/M/yyyy")
        {
            try
            {
                return DateTime.ParseExact(s, format, CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }
    }
}
