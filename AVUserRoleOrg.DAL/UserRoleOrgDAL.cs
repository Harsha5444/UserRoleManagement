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
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetOrganizations", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
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
                    }
                }
            }
            catch (SqlException ex)
            {
                LogExceptionToDatabase(ex.Message, ex.StackTrace);
            }
            catch (Exception ex)
            {
                LogExceptionToDatabase(ex.Message, ex.StackTrace);
            }

            return organizations;
        }
        public List<Roles> GetRoles()
        {
            List<Roles> roles = new List<Roles>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetRoles", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
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
                    }
                }
            }
            catch (SqlException ex)
            {
                LogExceptionToDatabase(ex.Message, ex.StackTrace);
            }
            catch (Exception ex)
            {
                LogExceptionToDatabase(ex.Message, ex.StackTrace);
            }
            return roles;
        }
        public List<Users> GetUsers()
        {
            List<Users> users = new List<Users>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetUsers", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
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
                    }
                }
            }
            catch (SqlException ex)
            {
                LogExceptionToDatabase(ex.Message, ex.StackTrace);
            }
            catch (Exception ex)
            {
                LogExceptionToDatabase(ex.Message, ex.StackTrace);
            }
            return users;
        }
        public List<UserRoles> GetUserRoles()
        {
            List<UserRoles> userRoles = new List<UserRoles>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetUsersRoles", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
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
                    }
                }
            }
            catch (SqlException ex)
            {
                LogExceptionToDatabase(ex.Message, ex.StackTrace);
            }
            catch (Exception ex)
            {
                LogExceptionToDatabase(ex.Message, ex.StackTrace);
            }

            return userRoles;
        }
        public int CreateUser(Users user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand("usp_CreateUser", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    command.Parameters.AddWithValue("@UserName", user.UserName);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@UserLoginId", user.UserLoginId);
                    command.Parameters.AddWithValue("@OrganizationID", user.OrganizationID);
                    command.Parameters.AddWithValue("@IsActive", user.IsActive);

                    SqlParameter outputParameter = new SqlParameter("@UserID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParameter);

                    connection.Open();
                    command.ExecuteNonQuery();
                    return Convert.ToInt32(outputParameter.Value);
                }
            }
            catch (SqlException ex)
            {
                LogExceptionToDatabase(ex.Message, ex.StackTrace);
                return -1;
            }
            catch (Exception ex)
            {
                LogExceptionToDatabase(ex.Message, ex.StackTrace);
                return -1;
            }
        }
        public void UpdateUser(Users user)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("usp_UpdateUser", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

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
            catch (SqlException ex)
            {
                LogExceptionToDatabase(ex.Message, ex.StackTrace);
            }
            catch (Exception ex)
            {
                LogExceptionToDatabase(ex.Message, ex.StackTrace);
            }
        }
        public void UpdateUserRole(int userId, int selectedRole)
        {
            try
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
            catch (SqlException ex)
            {
                LogExceptionToDatabase(ex.Message, ex.StackTrace);
            }
            catch (Exception ex)
            {
                LogExceptionToDatabase(ex.Message, ex.StackTrace);
            }
        }
        public void DeleteUser(int userId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("usp_DeleteUser", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@UserID", userId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                LogExceptionToDatabase(ex.Message, ex.StackTrace);
            }
            catch (Exception ex)
            {
                LogExceptionToDatabase(ex.Message, ex.StackTrace);
            }
        }
        private void LogExceptionToDatabase(string message, string stackTrace)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("usp_LogException", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    command.Parameters.AddWithValue("@ErrorMessage", message);
                    command.Parameters.AddWithValue("@ErrorStackTrace", stackTrace);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error logging exception: " + ex.Message);
            }
        }
    }
}

