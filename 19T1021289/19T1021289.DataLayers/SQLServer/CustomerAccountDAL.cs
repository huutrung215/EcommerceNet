using _19T1021289.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021289.DataLayers.SQLServer
{
    public class CustomerAccountDAL : _BaseDAL, IUserAccountDAL
    {
        public CustomerAccountDAL(string connectionString) : base(connectionString)
        {
        }

        public UserAccount Authorize(string userName, string password)
        {
            UserAccount data = null;
            using (SqlConnection connection = OpenConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"SELECT CustomerID, CustomerName, ContactName, Email
                                    FROM Customers
                                    WHERE ContactName = @ContactName AND Password = @Password";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@ContactName", userName);
                cmd.Parameters.AddWithValue("@Password", password);

                using (var dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new UserAccount()
                        {
                            UserId = Convert.ToString(dbReader["CustomerID"]),
                            UserName = Convert.ToString(dbReader["ContactName"]),
                            FullName = Convert.ToString(dbReader["CustomerName"]),
                            Email = Convert.ToString(dbReader["Email"]),
                            Password = "",
                            RoleNames = ""
                        };
                    }
                    dbReader.Read();
                }
                connection.Close();
            }
            return data;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            bool result = false;
            if (newPassword != confirmPassword)
            {
                return result;
            }

            using (var connection = OpenConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE Customers
                                SET Password=@NewPassword
                                WHERE ContactName=@ContactName AND Password=@OldPassword";
                cmd.Parameters.AddWithValue("@ContactName", userName);
                cmd.Parameters.AddWithValue("@NewPassword", newPassword);
                cmd.Parameters.AddWithValue("@ConfirmPassword", confirmPassword);
                cmd.Parameters.AddWithValue("@OldPassword", oldPassword);

                //Execute kieu j?
                result = cmd.ExecuteNonQuery() > 0;
                connection.Close();
            }

            return result;
        }
    }
}
