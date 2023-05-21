using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using _19T1021289.BusinessLayers;
using _19T1021289.DomainModels;

namespace _19T1021289.Web
{
    public static class Converter
    {
        public static UserAccount CookieToUserAccount(string cookie)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<UserAccount>(cookie);
        }
    }
}
