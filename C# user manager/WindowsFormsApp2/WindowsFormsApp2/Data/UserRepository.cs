using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using WindowsFormsApp2.Models;

namespace WindowsFormsApp2.Data
{
    /// <summary>
    /// Data access layer for User operations
    /// </summary>
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository()
        {
            _connectionString = DatabaseConfig.ConnectionString;
        }

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Gets all users from the database
        /// </summary>
        public DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(_connectionString))
                {
                    con.Open();
                    string query = "SELECT ID, NV, username, usersurname, category, mission FROM usertable ORDER BY ID";
                    using (MySqlDataAdapter da = new MySqlDataAdapter(query, con))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving users: {ex.Message}", ex);
            }
            return dt;
        }

        /// <summary>
        /// Gets a single user by ID
        /// </summary>
        public User GetUserById(int id)
        {
            User user = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(_connectionString))
                {
                    con.Open();
                    string query = "SELECT ID, NV, username, usersurname, category, mission FROM usertable WHERE ID = @ID";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = new User
                                {
                                    ID = reader.GetInt32("ID"),
                                    NV = reader.GetFloat("NV"),
                                    Username = reader.IsDBNull(reader.GetOrdinal("username")) ? "" : reader.GetString("username"),
                                    UserSurname = reader.IsDBNull(reader.GetOrdinal("usersurname")) ? "" : reader.GetString("usersurname"),
                                    Category = reader.IsDBNull(reader.GetOrdinal("category")) ? "" : reader.GetString("category"),
                                    Mission = reader.IsDBNull(reader.GetOrdinal("mission")) ? "" : reader.GetString("mission")
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving user with ID {id}: {ex.Message}", ex);
            }
            return user;
        }

        /// <summary>
        /// Adds a new user to the database
        /// </summary>
        public bool AddUser(User user)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(_connectionString))
                {
                    con.Open();
                    string query = @"INSERT INTO usertable (ID, NV, username, usersurname, category, mission) 
                                   VALUES (@ID, @NV, @Username, @UserSurname, @Category, @Mission)";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", user.ID);
                        cmd.Parameters.AddWithValue("@NV", user.NV);
                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@UserSurname", user.UserSurname);
                        cmd.Parameters.AddWithValue("@Category", user.Category);
                        cmd.Parameters.AddWithValue("@Mission", user.Mission);
                        
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding user: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Updates an existing user
        /// </summary>
        public bool UpdateUser(User user)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(_connectionString))
                {
                    con.Open();
                    string query = @"UPDATE usertable 
                                   SET NV = @NV, username = @Username, usersurname = @UserSurname, 
                                       category = @Category, mission = @Mission 
                                   WHERE ID = @ID";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", user.ID);
                        cmd.Parameters.AddWithValue("@NV", user.NV);
                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@UserSurname", user.UserSurname);
                        cmd.Parameters.AddWithValue("@Category", user.Category);
                        cmd.Parameters.AddWithValue("@Mission", user.Mission);
                        
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating user: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Deletes a user by ID
        /// </summary>
        public bool DeleteUser(int id)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(_connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM usertable WHERE ID = @ID";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting user with ID {id}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Searches users by name, surname, category, or mission
        /// </summary>
        public DataTable SearchUsers(string searchTerm)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(_connectionString))
                {
                    con.Open();
                    string query = @"SELECT ID, NV, username, usersurname, category, mission 
                                   FROM usertable 
                                   WHERE username LIKE @SearchTerm 
                                      OR usersurname LIKE @SearchTerm 
                                      OR category LIKE @SearchTerm 
                                      OR mission LIKE @SearchTerm
                                   ORDER BY ID";
                    using (MySqlDataAdapter da = new MySqlDataAdapter(query, con))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching users: {ex.Message}", ex);
            }
            return dt;
        }

        /// <summary>
        /// Checks if a user ID already exists
        /// </summary>
        public bool UserExists(int id)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(_connectionString))
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM usertable WHERE ID = @ID";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking user existence: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets statistics about users
        /// </summary>
        public Dictionary<string, object> GetStatistics()
        {
            Dictionary<string, object> stats = new Dictionary<string, object>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(_connectionString))
                {
                    con.Open();
                    
                    // Total users
                    string query = "SELECT COUNT(*) FROM usertable";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        stats["TotalUsers"] = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // Average NV
                    query = "SELECT AVG(NV) FROM usertable";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        object result = cmd.ExecuteScalar();
                        stats["AverageNV"] = result != DBNull.Value ? Convert.ToDouble(result) : 0.0;
                    }

                    // Max NV
                    query = "SELECT MAX(NV) FROM usertable";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        object result = cmd.ExecuteScalar();
                        stats["MaxNV"] = result != DBNull.Value ? Convert.ToDouble(result) : 0.0;
                    }

                    // Min NV
                    query = "SELECT MIN(NV) FROM usertable";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        object result = cmd.ExecuteScalar();
                        stats["MinNV"] = result != DBNull.Value ? Convert.ToDouble(result) : 0.0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving statistics: {ex.Message}", ex);
            }
            return stats;
        }
    }
}
