using System.Data.SQLite;
using Transaction = LibraryMgmt.Models.Transaction;

namespace LibraryMgmt.DataAccess
{
    internal class TransactionRepository
    {
        private readonly SQLiteConnection _connection;

        public TransactionRepository()
        {
            _connection = DatabaseSingleton.Instance.Connection;
        }

        public List<Transaction> GetAllTransactions()
        {
            var transactions = new List<Transaction>();

            string query = @"
                SELECT
                    t.transaction_id,
                    t.book_id,
                    t.user_id,
                    u.school_id,
                    u.fname || ' ' || u.lname AS borrower_name,
                    b.title AS book_title,
                    t.borrow_date,
                    t.due_date,
                    t.status
                FROM
                    Transactions t
                JOIN
                    Users u ON t.user_id = u.user_id
                JOIN
                    Books b ON t.book_id = b.book_id
                ORDER BY
                    transaction_id DESC;";
            using (SQLiteCommand cmd = new SQLiteCommand(query, _connection))
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    transactions.Add(new Transaction
                    {
                        TransactionId = reader["transaction_id"] != DBNull.Value ? Convert.ToInt32(reader["transaction_id"]) : -1,
                        BookId = reader["book_id"] != DBNull.Value ? Convert.ToInt32(reader["book_id"]) : -1,
                        UserId = reader["user_id"] != DBNull.Value ? Convert.ToInt32(reader["user_id"]) : -1,
                        SchoolId = reader["school_id"] != DBNull.Value ? Convert.ToInt32(reader["school_id"]) : -1,
                        BorrowerName = reader["borrower_name"] as string ?? string.Empty,
                        BookTitle = reader["book_title"] as string ?? string.Empty,
                        BorrowDate = reader["borrow_date"] != DBNull.Value ? Convert.ToDateTime(reader["borrow_date"]) : DateTime.MinValue,
                        DueDate = reader["due_date"] != DBNull.Value ? Convert.ToDateTime(reader["due_date"]) : DateTime.MinValue,
                        Status = reader["status"] as string ?? string.Empty
                    });
                }
            }

            return transactions;
        }

        public void CheckOverdue()
        {
            string checkOverdue = "UPDATE Transactions SET status = 'overdue' WHERE status = 'borrowed' AND due_date < DATETIME('now')";
            using (SQLiteCommand cmd = new SQLiteCommand(checkOverdue, _connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void AddTransaction(Transaction transaction)
        {
            string query = @"
                INSERT INTO Transactions (book_id, user_id, borrow_date, due_date, status) 
                VALUES (@book_id, @user_id, @borrow_date, @due_date, 'borrowed')";

            using (SQLiteCommand cmd = new SQLiteCommand(query, _connection))
            {
                {
                    cmd.Parameters.AddWithValue("@book_id", transaction.BookId);
                    cmd.Parameters.AddWithValue("@user_id", transaction.UserId);
                    cmd.Parameters.AddWithValue("@borrow_date", transaction.BorrowDate);
                    cmd.Parameters.AddWithValue("@due_date", transaction.DueDate);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTransaction(int transactionId)
        {
            string query = "DELETE FROM Transactions WHERE transaction_id = @transactionId";
            using (SQLiteCommand cmd = new SQLiteCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@transactionId", transactionId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
