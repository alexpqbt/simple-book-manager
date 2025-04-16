using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Windows.Forms;

namespace LibraryMgmt
{
    public partial class Form1 : Form
    {
        private SQLiteConnection conn;
        private Form2 userMgmtForm;
        private Form3 transactionForm;
        public event EventHandler? OnRefreshTransaction;
        public Form1()
        {
            string dbFolderPath = Path.Combine(Application.StartupPath, @"..\..\..\Database", "test2.db");
            string connStr = $"Data Source={Path.GetFullPath(dbFolderPath)};Version=3;";
            conn = new SQLiteConnection(connStr);
            conn.Open();

            InitializeComponent();

            userMgmtForm = new Form2(conn);
            transactionForm = new Form3(conn);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadBooks();
            customizeGridView(booksGridView);
            foreach (DataGridViewColumn col in booksGridView.Columns)
            {
                if (col.Index == 0)
                {
                    //col.HeaderText = "ID";
                    col.Visible = false;
                }
                else
                {
                    col.HeaderText = col.HeaderText.ToUpper();
                }
            }

            OpenUserMgmtForm();
            OpenTransactionForm();
        }

        private void booksGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = booksGridView.Rows[e.RowIndex];
                titleTextBox.Text = selectedRow.Cells["title"].Value?.ToString() ?? "";
                authorTextBox.Text = selectedRow.Cells["author"].Value?.ToString() ?? "";
                yearTextBox.Text = selectedRow.Cells["year"].Value?.ToString() ?? "";
                genreTextBox.Text = selectedRow.Cells["genre"].Value?.ToString() ?? "";
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {

            string title = titleTextBox.Text;
            string author = authorTextBox.Text;
            int year = Int32.Parse(yearTextBox.Text);
            string genre = genreTextBox.Text;

            string checkQuery = "SELECT COUNT(*) FROM Books WHERE title = @title AND author = @author AND year = @year";
            using (SQLiteCommand checkCmd = new SQLiteCommand(checkQuery, conn))
            {
                checkCmd.Parameters.AddWithValue("@title", title);
                checkCmd.Parameters.AddWithValue("@author", author);
                checkCmd.Parameters.AddWithValue("@year", year);

                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Book already exist");
                    ClearAllTextBoxes();
                    return;
                }

            }

            string query = "INSERT INTO Books (title, author, year, genre) VALUES (@title, @author, @year, @genre)";
            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@author", author);
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Parameters.AddWithValue("@genre", genre);

                cmd.ExecuteNonQuery();
            }

            ClearAllTextBoxes();
            LoadBooks();

            OnRefreshTransaction?.Invoke(this, EventArgs.Empty);
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {

            if (booksGridView.SelectedRows.Count > 0)
            {
                int bookId = Convert.ToInt32(booksGridView.SelectedRows[0].Cells["book_id"].Value);

                string query = "DELETE FROM Books WHERE book_id = @bookId";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@bookId", bookId);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Book deleted");
                    }
                    else
                    {
                        MessageBox.Show("Book not available");
                    }
                }
            }

            ClearAllTextBoxes();
            LoadBooks();
            booksGridView.ClearSelection();
            booksGridView.CurrentCell = null;

            OnRefreshTransaction?.Invoke(this, EventArgs.Empty);
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {

            int bookId = Convert.ToInt32(booksGridView.SelectedRows[0].Cells["book_id"].Value);
            string title = titleTextBox.Text;
            string author = authorTextBox.Text;
            int year = Int32.Parse(yearTextBox.Text);
            string genre = genreTextBox.Text;

            string query = "UPDATE Books SET title = @title, author = @author, year = @year, genre = @genre WHERE book_id = @bookId";
            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@bookId", bookId);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@author", author);
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Parameters.AddWithValue("@genre", genre);
                cmd.ExecuteNonQuery();
            }

            ClearAllTextBoxes();
            LoadBooks();
            booksGridView.ClearSelection();
            booksGridView.CurrentCell = null;

            OnRefreshTransaction?.Invoke(this, EventArgs.Empty);
        }

        private void LoadBooks()
        {
            string query = "SELECT * FROM Books ORDER BY book_id DESC";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
            DataTable table = new DataTable();

            adapter.Fill(table);

            booksGridView.DataSource = table;
        }

        private void ClearAllTextBoxes()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).Clear();
                }
            }
        }

        private void OpenUserMgmtForm()
        {
            userMgmtForm.Show();
            userMgmtForm.Activate();
        }

        private void OpenTransactionForm()
        {
            transactionForm.SubscribeToFormEvents(this, userMgmtForm);
            transactionForm.Show();
            transactionForm.Activate();
        }

        public void BringToFront(Form activeForm)
        {
            this.Activate();

            if (userMgmtForm != null && userMgmtForm != activeForm)
            {
                userMgmtForm.Activate();
            }

            if (transactionForm != null && transactionForm != activeForm)
            {
                transactionForm.Activate();
            }
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

        private void booksGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            booksGridView.ClearSelection();
            booksGridView.CurrentCell = null;
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            ClearAllTextBoxes();
            booksGridView.ClearSelection();
            booksGridView.CurrentCell = null;
        }
    }
}
