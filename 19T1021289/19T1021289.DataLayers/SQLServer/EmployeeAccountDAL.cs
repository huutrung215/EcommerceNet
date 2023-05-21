using _19T1021289.DataLayers.SQLServer;
using _19T1021289.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021289.DataLayers.SQLServer
{
    /// <summary>
    /// xu ly du lieu lien quan den tai khoan cua nhan vien
    /// </summary>
    public class EmployeeAccountDAL : _BaseDAL, IUserAccountDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public EmployeeAccountDAL(string connectionString) : base(connectionString)
        {
        }

        public UserAccount Authorize(string userName, string password)
        {
            UserAccount data = null;
            using (SqlConnection connection = OpenConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"SELECT EmployeeID, FirstName, LastName, Email, Photo
                                    FROM Employees
                                    WHERE Email = @Email AND Password = @Password";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Email", userName);
                cmd.Parameters.AddWithValue ("@Password", password);

                using (var dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new UserAccount()
                        {
                            UserId = Convert.ToString(dbReader["EmployeeID"]),
                            UserName = Convert.ToString(dbReader["Email"]),
                            FullName = $"{dbReader["FirstName"]} {dbReader["LastName"]}",
                            Email = Convert.ToString(dbReader["Email"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
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
                cmd.CommandText = @"UPDATE Employees 
                                SET Password=@NewPassword
                                WHERE Email=@Email AND Password=@OldPassword";
                cmd.Parameters.AddWithValue("@Email", userName);
                cmd.Parameters.AddWithValue("@NewPassword", newPassword);
                cmd.Parameters.AddWithValue("@ConfirmPassword", confirmPassword);
                cmd.Parameters.AddWithValue("@OldPassword", oldPassword);

                //phương thức ExecuteNonQuery() của đối tượng SqlCommand được sử dụng để thực thi một câu lệnh truy vấn
                //(INSERT, UPDATE, DELETE) và không trả về bất kỳ giá trị nào. Kết quả trả về của phương thức này là số
                //hàng bị ảnh hưởng bởi câu lệnh truy vấn.
                result = cmd.ExecuteNonQuery() > 0;
                connection.Close();
            }

            return result;
        }
    }
}
