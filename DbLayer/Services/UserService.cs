using DbLayer.Common;
using DbLayer.Models;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace DbLayer.Services
{
    public class UserService
    {
        public IList<Users> GetAllUsers()
        {
            string query = "SELECT * FROM Users";
            var dataTable = DBOps.GetDataTable(query);
            List<Users> users = new List<Users>();
            users = DBOps.ConvertDataTableToList<Users>(dataTable);
            return users;
        }

        public Users GetUserById(int id)
        {
            string query = $"SELECT * FROM Users WHERE UserId = {id}";
            var reader = DBOps.ExecuteReader(query);
            Users user = new Users();
            if (reader != null && reader.Read())
                user = DBOps.ConvertReaderToObject<Users>(reader);
            return user;
        }

        public Users GetUserByEmail(string email)
        {
            string query = $"SELECT * FROM Users WHERE Email = '{email}'";
            var reader = DBOps.ExecuteReader(query);
            Users user = new Users();
            if (reader != null && reader.Read())
                user = DBOps.ConvertReaderToObject<Users>(reader);
            return user;
        }

        public int AddUser(Users user)
        {
            string query = $"INSERT INTO Users (Email, Password, Role) VALUES ('{user.Email}', '{user.Password}', '{user.Role}')";
            int result = DBOps.ExecuteNonQuery(query);
            return result;
        }

        public int UpdateUser(Users user)
        {
            string query = $"UPDATE Users SET Email = '{user.Email}', Password = '{user.Password}', Role = '{user.Role}' WHERE UserId = {user.UserId}";
            int result = DBOps.ExecuteNonQuery(query);
            return result;
        }

        public int DeleteUser(int id)
        {
            string query = $"DELETE FROM Users WHERE UserId = {id}";
            int result = DBOps.ExecuteNonQuery(query);
            return result;
        }

        public bool ValidateUserLogin(string email, string password)
        {
            string query = $"SELECT * FROM Users WHERE Email = '{email}' AND Password = '{password}'";
            var reader = DBOps.ExecuteReader(query);
            return reader != null && reader.Read();
        }

        public string GetUserRole(string email)
        {
            string query = $"SELECT Role FROM Users WHERE Email = '{email}'";
            var reader = DBOps.ExecuteReader(query);
            if (reader != null && reader.Read())
                return reader["Role"].ToString();
            return null;
        }

        public int ResetPassword(string email, string newPassword)
        {
            string query = $"UPDATE Users SET Password = '{newPassword}' WHERE Email = '{email}'";
            return DBOps.ExecuteNonQuery(query);
        }

        public bool IsEmailExists(string email)
        {
            string query = $"SELECT COUNT(*) FROM Users WHERE Email = '{email}'";
            var scalar = DBOps.ExecuteScalar(query);
            if (scalar != null)
                return Convert.ToInt32(scalar) > 0;
            return false;
        }

    }
}
