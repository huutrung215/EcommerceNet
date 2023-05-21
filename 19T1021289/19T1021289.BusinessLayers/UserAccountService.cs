using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021289.DataLayers;
using _19T1021289.DomainModels;

namespace _19T1021289.BusinessLayers
{
    /// <summary>
    /// cac chuc nang lien quan den tai khoan
    /// </summary>
    public class UserAccountService
    {
        private static IUserAccountDAL employeeAccountDB;
        private static IUserAccountDAL customerAccountDB;
        /// <summary>
        /// contructor
        /// </summary>
        static UserAccountService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            employeeAccountDB = new DataLayers.SQLServer.EmployeeAccountDAL(connectionString);
            customerAccountDB = new DataLayers.SQLServer.CustomerAccountDAL(connectionString);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountType"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static UserAccount Authorize(AccountTypes accountType, string userName, string password)
        {
            if (accountType == AccountTypes.Employee)
            {
                return employeeAccountDB.Authorize(userName, password);
            }
            else
            {
                return customerAccountDB.Authorize(userName, password);
            }
        }
        public static bool ChangePassword(AccountTypes accountType, string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            if (accountType == AccountTypes.Employee)
            {
                return employeeAccountDB.ChangePassword(userName, oldPassword, newPassword, confirmPassword);
            }
            else
            {
                return customerAccountDB.ChangePassword(userName, oldPassword, newPassword, confirmPassword);
            }
        }
    }
}
