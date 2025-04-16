using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LibraryMgmt
{
    public partial class Form3 : Form
    {
        private SQLiteConnection conn;
        public Form3(SQLiteConnection connection)
        {
            InitializeComponent();
            conn = connection;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            LoadTransactions();
            refreshData();
            ClearAllComboBoxes();
            customizeGridView(transactionsGridView);
            transactionsGridView.Columns["transaction_id"].Visible = false;
            transactionsGridView.Columns["school_id"].HeaderText = "School ID".ToUpper();
            transactionsGridView.Columns["borrower_name"].HeaderText = "Name".ToUpper();
            transactionsGridView.Columns["book_title"].HeaderText = "Book".ToUpper();
            transactionsGridView.Columns["borrow_date"].HeaderText = "Borrow Date".ToUpper();
            transactionsGridView.Columns["due_date"].HeaderText = "Due Date".ToUpper();
            transactionsGridView.Columns["status"].HeaderText = "Status".ToUpper();

            setDueDate(7);
        }

        private void setDueDate(int weekdaysToAdd)
        {
            DateTime startDate = dueDatePicker.Value;
            DateTime resultDate = startDate;

            while (weekdaysToAdd > 0)
            {
                resultDate = resultDate.AddDays(1);
                if (resultDate.DayOfWeek != DayOfWeek.Saturday && resultDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    weekdaysToAdd--;
                }
            }

            dueDatePicker.Value = resultDate;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (this.Owner != null)
            {
                ((Form1)this.Owner).BringToFront();
            }
        }

        private void LoadTransactions()
        {
            string query = @"
                SELECT
                    t.transaction_id,
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
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
            DataTable table = new DataTable();

            string checkOverdue = "UPDATE Transactions SET status = 'overdue' WHERE status = 'borrowed' AND due_date < DATETIME('now')";
            using (SQLiteCommand cmd = new SQLiteCommand(checkOverdue, conn))
            {
                cmd.ExecuteNonQuery();
            }

            adapter.Fill(table);

            transactionsGridView.DataSource = table;

        }
        public void customizeGridView(DataGridView grid)
        {
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Roboto", 9, FontStyle.Bold);
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.RowHeadersVisible = false;
            grid.Columns[0].Width = 50;
        }

        private void transactionsGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (transactionsGridView.Columns[e.ColumnIndex].Name == "status" && e.Value != null)
            {
                e.Value = e.Value.ToString()?.ToUpper();
                e.FormattingApplied = true;
            }

            if (transactionsGridView.Columns[e.ColumnIndex].Name == "borrow_date" ||
                transactionsGridView.Columns[e.ColumnIndex].Name == "due_date")
            {
                if (e.Value != null)
                {
                    e.Value = ((DateTime)e.Value).ToString("MMM dd, yyyy"); // or any format you like
                    e.FormattingApplied = true;
                }
            }
        }

        private void transactionsGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var row = transactionsGridView.Rows[e.RowIndex];
            if (row.Cells["status"].Value != null)
            {
                string? status = row.Cells["status"].Value.ToString();
                if (status == "overdue")
                {
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
                }
            }
        }

        private void LoadUsersToComboBox()
        {
            string query = "SELECT user_id, lname || ', ' || fname || ' (' || school_id || ')' AS full_user FROM Users";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            userComboBox.DataSource = dt;
            userComboBox.DisplayMember = "full_user";  // Display book titles
            userComboBox.ValueMember = "user_id"; // Store book ids as the value
        }

        private void LoadBooksToComboBox()
        {
            string query = @"
                SELECT book_id, title || ' by ' || author AS full_book 
                FROM Books 
                WHERE book_id 
                NOT IN (
                    SELECT book_id FROM Transactions WHERE status = 'borrowed' OR status = 'overdue'
                )";

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            bookComboBox.DataSource = dt;
            bookComboBox.DisplayMember = "full_book";  // Display book titles
            bookComboBox.ValueMember = "book_id"; // Store book ids as the value
        }

        public void refreshData()
        {
            LoadBooksToComboBox();
            LoadUsersToComboBox();
        }

        public void SubscribeToFormEvents(Form1 form1, Form2 form2)
        {
            form1.OnRefreshTransaction += (sender, e) => refreshData();
            form2.OnRefreshTransaction += (sender, e) => refreshData();
        }

        private void ClearAllComboBoxes()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is System.Windows.Forms.ComboBox)
                {
                    ((System.Windows.Forms.ComboBox)ctrl).Text = "";
                }
            }
        }

        private void Form3_Click(object sender, EventArgs e)
        {
            ClearAllComboBoxes();
            transactionsGridView.ClearSelection();
            transactionsGridView.CurrentCell = null;
        }

        private void borrowButton_Click(object sender, EventArgs e)
        {
            int book_id = Convert.ToInt32(bookComboBox.SelectedValue);
            int user_id = Convert.ToInt32(userComboBox.SelectedValue);
            DateTime borrow_date = borrowedDatePicker.Value;
            DateTime due_date = dueDatePicker.Value;

            string query = @"
                INSERT INTO Transactions (book_id, user_id, borrow_date, due_date, status) 
                VALUES (@book_id, @user_id, @borrow_date, @due_date, 'borrowed')";

            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            {
                {
                    cmd.Parameters.AddWithValue("@book_id", book_id);
                    cmd.Parameters.AddWithValue("@user_id", user_id);
                    cmd.Parameters.AddWithValue("@borrow_date", borrow_date);
                    cmd.Parameters.AddWithValue("@due_date", due_date);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Transaction recorded successfully.");
            refreshData();
            ClearAllComboBoxes();
            LoadTransactions();
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            if (transactionsGridView.SelectedRows.Count > 0)
            {
                int transactionId = Convert.ToInt32(transactionsGridView.SelectedRows[0].Cells["transaction_id"].Value);

                string query = "DELETE FROM Transactions WHERE transaction_id = @transactionId";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@transactionId", transactionId);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Book returned.");

                    }
                }
            }

            refreshData();
            ClearAllComboBoxes();
            LoadTransactions();
        }


    }
}