using DbLayer.Common;
using DbLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DbLayer.Services
{
    public class UserDetailService
    {
        public UserDetail GetUserDetailByUserId(int userId)
        {
            string query = $"SELECT * FROM UserDetail WHERE UserId = {userId}";
            var reader = DBOps.ExecuteReader(query);
            UserDetail userDetail = new UserDetail();
            if (reader != null && reader.Read())
                userDetail = DBOps.ConvertReaderToObject<UserDetail>(reader);
            return userDetail;
        }

        public UserDetail GetUserDetailById(int id)
        {
            string query = $"SELECT * FROM UserDetail WHERE UDId = {id}";
            var reader = DBOps.ExecuteReader(query);
            UserDetail userDetail = new UserDetail();
            if (reader != null && reader.Read())
                userDetail = DBOps.ConvertReaderToObject<UserDetail>(reader);
            return userDetail;
        }

        public int AddUserDetail(UserDetail userDetail)
        {
            string query = $"INSERT INTO UserDetail (UserId, Address, City, PostCode, Phone, Photo) " +
                $"VALUES ({userDetail.UserId}, '{userDetail.Address}', '{userDetail.City}', '{userDetail.PostCode}', '{userDetail.Phone}', @Photo)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Photo", SqlDbType.VarBinary) { Value = userDetail.Photo ?? (object)DBNull.Value }
            };
            return DBOps.ExecuteNonQuery(query, parameters);
        }

        public int UpdateUserDetail(UserDetail userDetail)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Photo", SqlDbType.VarBinary) { Value = userDetail.Photo ?? (object)DBNull.Value }
            };
            string query = $"UPDATE UserDetail SET " +
                $"Address = '{userDetail.Address}'" +
                $", City = '{userDetail.City}'" +
                $", PostCode = '{userDetail.PostCode}'" +
                $", Phone = '{userDetail.Phone}'" +
                $", Photo = @Photo" +
                $" WHERE UserId = {userDetail.UserId}";

            return DBOps.ExecuteNonQuery(query, parameters);
        }

        public int DeleteUserDetail(int id)
        {
            string query = $"DELETE FROM UserDetail WHERE UDId = {id}";
            return DBOps.ExecuteNonQuery(query);
        }

    }
}
