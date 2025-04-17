using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LibraryMgmt.Models;

namespace LibraryMgmt.DataAccess
{
    internal class UserRepository
    {
        private readonly SQLiteConnection _connection;

        public UserRepository()
        {
            _connection = DatabaseSingleton.Instance.Connection;
        }

        public List<User> GetAllUsers()
        {
            var users = new List<User>();

            string query = "SELECT * FROM Users ORDER BY user_id DESC";
            using (SQLiteCommand cmd = new SQLiteCommand(query, _connection))
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        UserId = reader["user_id"] != DBNull.Value ? Convert.ToInt32(reader["user_id"]) : -1,
                        SchoolId = reader["school_id"] != DBNull.Value ? Convert.ToInt32(reader["school_id"]) : -1,
                        FirstName = reader["fname"] as string ?? string.Empty,
                        LastName = reader["lname"] as string ?? string.Empty
                    });
                }
            }

            return users;
        }

        public bool UserExist(User user)
        {
            string checkQuery = "SELECT COUNT(*) FROM Users WHERE school_id = @schoolId";
            using (SQLiteCommand checkCmd = new SQLiteCommand(checkQuery, _connection))
            {
                checkCmd.Parameters.AddWithValue("@schoolId", user.SchoolId);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                return count > 0;
            }
        }

        public void AddUser(User user)
        {
            string query = "INSERT INTO Users (school_id, fname, lname) VALUES (@schoolId, @fname, @lname)";
            using (SQLiteCommand cmd = new SQLiteCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@schoolId", user.SchoolId);
                cmd.Parameters.AddWithValue("@fname", user.FirstName);
                cmd.Parameters.AddWithValue("@lname", user.LastName);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteUser(int userId)
        {
            string query = "DELETE FROM Users WHERE user_id = @userId";
            using (SQLiteCommand cmd = new SQLiteCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateUser(User user)
        {
            string query = "UPDATE Users SET school_id = @id, fname = @fname, lname = @lname WHERE user_id = @userId";
            using (SQLiteCommand cmd = new SQLiteCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@userId", user.UserId);
                cmd.Parameters.AddWithValue("@id", user.SchoolId);
                cmd.Parameters.AddWithValue("@fname", user.FirstName);
                cmd.Parameters.AddWithValue("@lname", user.LastName);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
