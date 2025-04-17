using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LibraryMgmt.DataAccess;
using LibraryMgmt.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryMgmt.DataAccess
{
    internal class BookRepository
    {
        private readonly SQLiteConnection _connection;

        public BookRepository()
        {
            _connection = DatabaseSingleton.Instance.Connection;
        }

        public List<Book> GetAllBooks()
        {
            var books = new List<Book>();

            string query = "SELECT * FROM Books ORDER BY book_id DESC";
            using (SQLiteCommand cmd = new SQLiteCommand(query, _connection))
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    books.Add(new Book
                    {
                        BookId = reader["book_id"] != DBNull.Value ? Convert.ToInt32(reader["book_id"]) : -1,
                        Title = reader["title"] as string ?? string.Empty,
                        Author = reader["author"] as string ?? string.Empty,
                        Year = reader["year"] != DBNull.Value ? Convert.ToInt32(reader["year"]) : -1,
                        Genre = reader["genre"] as string ?? string.Empty
                    });
                }
            }

            return books;
        }

        public List<Book> GetAvailableBooks()
        {
            var availBooks = new List<Book>();

            string query = @"
                SELECT * FROM Books WHERE book_id 
                NOT IN (
                    SELECT book_id FROM Transactions WHERE status = 'borrowed' OR status = 'overdue'
                )";
            using (SQLiteCommand cmd = new SQLiteCommand(query, _connection))
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    availBooks.Add(new Book
                    {
                        BookId = reader["book_id"] != DBNull.Value ? Convert.ToInt32(reader["book_id"]) : -1,
                        Title = reader["title"] as string ?? string.Empty,
                        Author = reader["author"] as string ?? string.Empty,
                        Year = reader["year"] != DBNull.Value ? Convert.ToInt32(reader["year"]) : -1,
                        Genre = reader["genre"] as string ?? string.Empty
                    });
                }
            }

            return availBooks;
        }

        public bool BookExist(Book book)
        {
            string checkQuery = "SELECT COUNT(*) FROM Books WHERE title = @title AND author = @author AND year = @year";
            using (SQLiteCommand checkCmd = new SQLiteCommand(checkQuery, _connection))
            {
                checkCmd.Parameters.AddWithValue("@title", book.Title);
                checkCmd.Parameters.AddWithValue("@author", book.Author);
                checkCmd.Parameters.AddWithValue("@year", book.Year);

                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                return count > 0;
            }
        }

        public void AddBook(Book book)
        {
            string query = "INSERT INTO Books (title, author, year, genre) VALUES (@title, @author, @year, @genre)";
            using (SQLiteCommand cmd = new SQLiteCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@title", book.Title);
                cmd.Parameters.AddWithValue("@author", book.Author);
                cmd.Parameters.AddWithValue("@year", book.Year);
                cmd.Parameters.AddWithValue("@genre", book.Genre);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteBook(int bookId)
        {
            string query = "DELETE FROM Books WHERE book_id = @bookId";
            using (SQLiteCommand cmd = new SQLiteCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@bookId", bookId);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateBook(Book book)
        {
            string query = "UPDATE Books SET title = @title, author = @author, year = @year, genre = @genre WHERE book_id = @bookId";
            using (SQLiteCommand cmd = new SQLiteCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@bookId", book.BookId);
                cmd.Parameters.AddWithValue("@title", book.Title);
                cmd.Parameters.AddWithValue("@author", book.Author);
                cmd.Parameters.AddWithValue("@year", book.Year);
                cmd.Parameters.AddWithValue("@genre", book.Genre);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
