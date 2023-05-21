using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021289.Web.Controllers
{
    /*[RoutePrefix("thu-nghiem")]*/
    public class TestController : Controller
    {
        /* // GET: Test
         [Route("xin-chao/{name}/{age}")]
         public string SayHello(string name, int age)
         {
             return $"Hello {name}, {age} years old";//trong chuoi chen buoi thuc dau ngoac nhon
         }*/

        [HttpGet]
        public ActionResult Input()
        {
            Person p = new Person()
            {

            };
            return View(p);

        }

        [HttpPost]
        public ActionResult Input(Person p)
        {
            var data = new
            {
                Name = p.Name,
                BirthDate = string.Format("{0:dd/MM/yyyy}", p.BirthDate),
                Salary = p.Salary,
            };
            return Json(p, JsonRequestBehavior.AllowGet);
        }

        public string TestDate(DateTime value)
        {
            DateTime d = value;
            return string.Format("{0:dd/MM/yyyy}", d);
        }
    }


    public class Person
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; } = new DateTime(1000, 1, 1);

        public float Salary { get; set; }

    }
}