using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021289.DomainModels
{
    /// <summary>
    /// thông tin nhân viên
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// 
        /// </summary>
        public int EmployeeID { get; set; }

        public String LastName { get; set; }

        public String FirstName { get; set; }

        public DateTime BirthDate { get; set; }

        public String Photo { get; set; }
        public String Notes { get; set; }

        public String Email { get; set; }

        public String Password { get; set; }
    }
}
