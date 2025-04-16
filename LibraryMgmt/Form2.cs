using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryMgmt
{
    public partial class Form2 : Form
    {
        private SQLiteConnection conn;
        public event EventHandler? OnRefreshTransaction;

        public Form2(SQLiteConnection connection)
        {
            InitializeComponent();
            conn = connection;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadUsers();
            customizeGridView(usersGridView);
            usersGridView.Columns["user_id"].Visible = false;
            usersGridView.Columns["school_id"].HeaderText = "School ID".ToUpper();
            usersGridView.Columns["fname"].HeaderText = "Firstname".ToUpper();
            usersGridView.Columns["lname"].HeaderText = "Lastname".ToUpper();
        }

        private void LoadUsers()
        {
            string query = "SELECT * FROM Users ORDER BY user_id DESC";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
            DataTable table = new DataTable();

            adapter.Fill(table);

            usersGridView.DataSource = table;
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

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (this.Owner != null)
            {
                ((Form1)this.Owner).BringToFront();
            }
        }

        private void usersGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = usersGridView.Rows[e.RowIndex];
                idTextBox.Text = selectedRow.Cells["school_id"].Value?.ToString() ?? "";
                fnameTextBox.Text = selectedRow.Cells["fname"].Value?.ToString() ?? "";
                lnameTextBox.Text = selectedRow.Cells["lname"].Value?.ToString() ?? "";
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(idTextBox.Text);
            string fname = fnameTextBox.Text;
            string lname = lnameTextBox.Text;

            string checkQuery = "SELECT COUNT(*) FROM Users WHERE school_id = @schoolId";
            using (SQLiteCommand checkCmd = new SQLiteCommand(checkQuery, conn))
            {
                checkCmd.Parameters.AddWithValue("@schoolId", id);

                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("User already exist");
                    ClearAllTextBoxes();
                    return;
                }

            }

            string query = "INSERT INTO Users (school_id, fname, lname) VALUES (@schoolId, @fname, @lname)";
            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@schoolId", id);
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@lname", lname);

                cmd.ExecuteNonQuery();
            }

            ClearAllTextBoxes();
            LoadUsers();

            OnRefreshTransaction?.Invoke(this, EventArgs.Empty);
        }
        private void removeBtn_Click(object sender, EventArgs e)
        {
            if (usersGridView.SelectedRows.Count > 0)
            {
                int userId = Convert.ToInt32(usersGridView.SelectedRows[0].Cells["user_id"].Value);

                string query = "DELETE FROM Users WHERE user_id = @userId";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User deleted");
                    }
                    else
                    {
                        MessageBox.Show("User not available");
                    }
                }
            }

            ClearAllTextBoxes();
            LoadUsers();
            usersGridView.ClearSelection();
            usersGridView.CurrentCell = null;

            OnRefreshTransaction?.Invoke(this, EventArgs.Empty);

        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(usersGridView.SelectedRows[0].Cells["user_id"].Value);
            int id = Int32.Parse(idTextBox.Text);
            string fname = fnameTextBox.Text;
            string lname = lnameTextBox.Text;

            string query = "UPDATE Users SET school_id = @id, fname = @fname, lname = @lname WHERE user_id = @userId";
            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@lname", lname);

                cmd.ExecuteNonQuery();
            }

            ClearAllTextBoxes();
            LoadUsers();
            usersGridView.ClearSelection();
            usersGridView.CurrentCell = null;

            OnRefreshTransaction?.Invoke(this, EventArgs.Empty);

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

        private void Form2_Click(object sender, EventArgs e)
        {
            ClearAllTextBoxes();
            usersGridView.ClearSelection();
            usersGridView.CurrentCell = null;
        }

        
    }
}
