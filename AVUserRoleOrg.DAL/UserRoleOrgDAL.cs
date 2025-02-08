using AVUserRoleOrg.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AVUserRoleOrg.DAL
{
    public class UserRoleOrgDAL
    {
        private readonly string _connectionString;
        public UserRoleOrgDAL()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["UserRoleOrgDB"].ConnectionString;
        }
        public List<Organization> GetOrganizations()
        {
            List<Organization> organizations = new List<Organization>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT OrgID, OrgName, OrgType, CreatedDate FROM Organization";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    organizations.Add(new Organization
                    {
                        OrgID = Convert.ToInt32(reader["OrgID"]),
                        OrgName = reader["OrgName"].ToString(),
                        OrgType = reader["OrgType"].ToString(),
                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                    });
                }
            }
            return organizations;
        }
        public List<Roles> GetRoles()
        {
            List<Roles> roles = new List<Roles>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT RoleID, RoleName, RoleDescription, IsActive FROM Roles";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    roles.Add(new Roles
                    {
                        RoleID = Convert.ToInt32(reader["RoleID"]),
                        RoleName = reader["RoleName"].ToString(),
                        RoleDescription = reader["RoleDescription"].ToString(),
                        IsActive = Convert.ToBoolean(reader["IsActive"])
                    });
                }
            }
            return roles;
        }
        public List<Users> GetUsers()
        {
            List<Users> users = new List<Users>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT UserID, UserName, Email, UserLoginId, OrganizationID, IsActive FROM Users";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new Users
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString(),
                        Email = reader["Email"].ToString(),
                        UserLoginId = reader["UserLoginId"].ToString(),
                        OrganizationID = reader["OrganizationID"] != DBNull.Value ? Convert.ToInt32(reader["OrganizationID"]) : (int?)null,
                        IsActive = Convert.ToBoolean(reader["IsActive"])
                    });
                }
            }
            return users;
        }
        public List<UserRoles> GetUserRoles()
        {
            List<UserRoles> userRoles = new List<UserRoles>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT UserRoleID, UserID, RoleID FROM UserRoles";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    userRoles.Add(new UserRoles
                    {
                        UserRoleID = Convert.ToInt32(reader["UserRoleID"]),
                        UserID = Convert.ToInt32(reader["UserID"]),
                        RoleID = Convert.ToInt32(reader["RoleID"])
                    });
                }
            }
            return userRoles;
        }
        public int CreateUser(Users user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(@"
                INSERT INTO Users (UserName, Email, UserLoginId, OrganizationID, IsActive)
                VALUES (@UserName, @Email, @UserLoginId, @OrganizationID, @IsActive); SELECT SCOPE_IDENTITY();", connection);

                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@UserLoginId", user.UserLoginId);
                command.Parameters.AddWithValue("@OrganizationID", user.OrganizationID);
                command.Parameters.AddWithValue("@IsActive", user.IsActive);

                connection.Open();
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }
        public void UpdateUser(Users user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(@"
                UPDATE Users 
                SET UserName = @UserName,
                    Email = @Email,
                    OrganizationID = @OrganizationID,
                    UserLoginId = @UserLoginId,
                    IsActive = @IsActive
                WHERE UserID = @UserID", connection);

                command.Parameters.AddWithValue("@UserID", user.UserID);
                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@OrganizationID", user.OrganizationID);
                command.Parameters.AddWithValue("@IsActive", user.IsActive);
                command.Parameters.AddWithValue("@UserLoginId", user.UserLoginId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void UpdateUserRole(int userId, int selectedRole)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("usp_UpdateOrInsertUserRole", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userId);
                    command.Parameters.AddWithValue("@RoleID", selectedRole);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void DeleteUser(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(@"
                DELETE From Users 
                WHERE UserID = @UserID", connection);

                command.Parameters.AddWithValue("@UserID", userId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
