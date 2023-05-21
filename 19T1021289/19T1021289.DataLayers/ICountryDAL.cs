using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021289.DomainModels;

namespace _19T1021289.DataLayers
{
    /// <summary>
    /// dinh nghia cac chuc nang xu ly du lieu cho quoc gia
    /// </summary>
    public interface ICountryDAL
    {
        /// <summary>
        /// Lay danh sach cac quoc gia
        /// </summary>
        /// <returns></returns>
        IList<Country> List();
    }
}
