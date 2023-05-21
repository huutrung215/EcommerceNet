using _19T1021289.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021289.DataLayers
{
    /// <summary>
    /// dinh nghia cacs phep xu ly tai khoan nguoi dung
    /// </summary>
    public interface IUserAccountDAL
    {
        /// <summary>
        /// kiem tra ten dn va password co hop le hay ko
        /// neu hop le thi tra ve thong tin tk, ko hop le thi tra ve gia tri null
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserAccount Authorize (string userName, string password);

        /// <summary>
        /// doi mat khau
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool ChangePassword (string userName, string oldPassword, string newPassword, string comfirmPassword);
    }
}
