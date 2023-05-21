using _19T1021289.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace _19T1021289.DataLayers.SQLServer
{
    /// <summary>
    /// Cai dat xu ly du lieu lien quan den quoc gia
    /// </summary>
    public class CountryDAL : _BaseDAL, ICountryDAL
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="connectionString"></param>
        public CountryDAL(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IList<Country> List()
        {
            List<Country> data = new List<Country>();

            using (var connection = OpenConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT CountryName FROM Countries";
                cmd.CommandType = CommandType.Text;

                SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dbReader.Read())
                {
                    data.Add(new Country()
                    {
                        CountryName = Convert.ToString(dbReader["CountryName"])
                    });
                }

                dbReader.Close();
                connection.Close();
            }

            return data;
        }
    }
}
