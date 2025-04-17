using LibraryMgmt.DataAccess;
using LibraryMgmt.Models;

namespace LibraryMgmt
{
    public partial class Form2 : Form
    {
        private UserRepository _userRepository;
        public event EventHandler? _OnRefreshTransaction;

        public Form2()
        {
            InitializeComponent();
            _userRepository = new UserRepository();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.AcceptButton = addButton;
            LoadUsers();
            customizeGridView(usersGridView);
            usersGridView.Columns["UserId"].Visible = false;
            usersGridView.Columns["SchoolId"].HeaderText = "School ID".ToUpper();
            usersGridView.Columns["FirstName"].HeaderText = "Firstname".ToUpper();
            usersGridView.Columns["LastName"].HeaderText = "Lastname".ToUpper();
        }

        private void LoadUsers()
        {
            usersGridView.DataSource = _userRepository.GetAllUsers();
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
                idTextBox.Text = selectedRow.Cells["SchoolId"].Value?.ToString() ?? "";
                fnameTextBox.Text = selectedRow.Cells["FirstName"].Value?.ToString() ?? "";
                lnameTextBox.Text = selectedRow.Cells["LastName"].Value?.ToString() ?? "";
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var user = new User
            {
                SchoolId = Convert.ToInt32(idTextBox.Text),
                FirstName = fnameTextBox.Text,
                LastName = lnameTextBox.Text
            };

            if (_userRepository.UserExist(user))
            {
                MessageBox.Show("User already exist.");
                ClearAllTextBoxes();
                return;
            }

            _userRepository.AddUser(user);

            ClearAllTextBoxes();
            LoadUsers();

            _OnRefreshTransaction?.Invoke(this, EventArgs.Empty);
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            if (usersGridView.SelectedRows.Count > 0)
            {
                int userId = Convert.ToInt32(usersGridView.SelectedRows[0].Cells[0].Value);

                _userRepository.DeleteUser(userId);

                ClearAllTextBoxes();
                LoadUsers();
                usersGridView.ClearSelection();
                usersGridView.CurrentCell = null;

                _OnRefreshTransaction?.Invoke(this, EventArgs.Empty);
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            var user = new User
            {
                UserId = Convert.ToInt32(usersGridView.SelectedRows[0].Cells[0].Value),
                SchoolId = Convert.ToInt32(idTextBox.Text),
                FirstName = fnameTextBox.Text,
                LastName = lnameTextBox.Text
            };

            _userRepository.UpdateUser(user);

            ClearAllTextBoxes();
            LoadUsers();
            usersGridView.ClearSelection();
            usersGridView.CurrentCell = null;

            _OnRefreshTransaction?.Invoke(this, EventArgs.Empty);

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
