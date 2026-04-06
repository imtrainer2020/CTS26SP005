using DbLayer.Common;
using DbLayer.Models;
using System;
using System.Collections.Generic;
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

        public int AddUserDetail(UserDetail userDetail)
        {
            string query = $"INSERT INTO UserDetail (UserId, Address, City, PostCode, Phone, Photo) VALUES ({userDetail.UserId}, '{userDetail.Address}', '{userDetail.City}', '{userDetail.PostCode}', '{userDetail.Phone}', '{userDetail.Photo}')";
            return DBOps.ExecuteNonQuery(query);
        }

        public int UpdateUserDetail(UserDetail userDetail)
        {
            string query = $"UPDATE UserDetail SET " +
                $"Address = '{userDetail.Address}'" +
                $", City = '{userDetail.City}'" +
                $", PostCode = '{userDetail.PostCode}'" +
                $", Phone = '{userDetail.Phone}'" +
                $", Photo = '{userDetail.Photo}'" +
                $" WHERE UserId = {userDetail.UserId}";

            return DBOps.ExecuteNonQuery(query);
        }

        public int DeleteUserDetail(int userId)
        {
            string query = $"DELETE FROM UserDetail WHERE UserId = {userId}";
            return DBOps.ExecuteNonQuery(query);
        }

    }
}
