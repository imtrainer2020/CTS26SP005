using DbLayer.Common;
using DbLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbLayer.Services
{
    public class UserRoleService
    {
        public IList<UserRole> GetAllUserRoles()
        {
            string query = "SELECT RoleName FROM UserRole";
            var dataTable = DBOps.GetDataTable(query);
            List<UserRole> userRoles = new List<UserRole>();
            userRoles = DBOps.ConvertDataTableToList<UserRole>(dataTable);
            return userRoles;
        }

        public int AddUserRole(UserRole userRole)
        {
            string query = $"INSERT INTO UserRole (RoleName) VALUES ('{userRole.RoleName}')";
            int result = DBOps.ExecuteNonQuery(query);
            return result;
        }

        public int UpdateUserRole(string oldRoleName, string newRoleName)
        {
            string query = $"UPDATE UserRole SET RoleName = '{newRoleName}' WHERE LOWER(RoleName) = '{oldRoleName.ToLower()}'";
            int result = DBOps.ExecuteNonQuery(query);
            return result;
        }

        public int DeleteUserRole(string oldRoleName)
        {
            string query = $"DELETE FROM UserRole WHERE LOWER(RoleName) = '{oldRoleName.ToLower()}'";
            int result = DBOps.ExecuteNonQuery(query);
            return result;
        }

    }
}
