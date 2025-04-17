using System.Data;
using LibraryMgmt.DataAccess;
using Transaction = LibraryMgmt.Models.Transaction;

namespace LibraryMgmt
{
    public partial class Form3 : Form
    {
        private TransactionRepository _transactionRepository;

        public Form3()
        {
            InitializeComponent();
            _transactionRepository = new TransactionRepository();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.AcceptButton = borrowButton;
            LoadTransactions();
            refreshData();
            ClearAllComboBoxes();

            customizeGridView(transactionsGridView);
            transactionsGridView.Columns["TransactionId"].Visible = false;
            transactionsGridView.Columns["BookId"].Visible = false;
            transactionsGridView.Columns["UserId"].Visible = false;
            transactionsGridView.Columns["SchoolId"].HeaderText = "School ID".ToUpper();
            transactionsGridView.Columns["BorrowerName"].HeaderText = "Name".ToUpper();
            transactionsGridView.Columns["BookTitle"].HeaderText = "Book".ToUpper();
            transactionsGridView.Columns["BorrowDate"].HeaderText = "Borrow Date".ToUpper();
            transactionsGridView.Columns["DueDate"].HeaderText = "Due Date".ToUpper();
            transactionsGridView.Columns["Status"].HeaderText = "Status".ToUpper();

            setDueDate(7);
        }

        private void LoadTransactions()
        {
            _transactionRepository.CheckOverdue();
            transactionsGridView.DataSource = _transactionRepository.GetAllTransactions();
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

        private void borrowButton_Click(object sender, EventArgs e)
        {
            var transaction = new Transaction
            {
                BookId = Convert.ToInt32(bookComboBox.SelectedValue),
                UserId = Convert.ToInt32(userComboBox.SelectedValue),
                BorrowDate = Convert.ToDateTime(borrowedDatePicker.Value),
                DueDate = Convert.ToDateTime(dueDatePicker.Value),
            };

            _transactionRepository.AddTransaction(transaction);

            refreshData();
            ClearAllComboBoxes();
            LoadTransactions();
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            if (transactionsGridView.SelectedRows.Count > 0)
            {
                int transactionId = Convert.ToInt32(transactionsGridView.SelectedRows[0].Cells[0].Value);

                _transactionRepository.DeleteTransaction(transactionId);

                refreshData();
                ClearAllComboBoxes();
                LoadTransactions();
            }
        }

        private void transactionsGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (transactionsGridView.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                e.Value = e.Value.ToString()?.ToUpper();
                e.FormattingApplied = true;
            }

            if (transactionsGridView.Columns[e.ColumnIndex].Name == "BorrowDate" ||
                transactionsGridView.Columns[e.ColumnIndex].Name == "DueDate")
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
            if (row.Cells["Status"].Value != null)
            {
                string? status = row.Cells["Status"].Value.ToString();
                if (status == "overdue")
                {
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
                }
            }
        }

        private void LoadUsersToComboBox()
        {
            UserRepository userRepository = new UserRepository();
            var users = userRepository.GetAllUsers();

            var userDisplayList = users.Select(user => new
            {
                DisplayName = $"{user.LastName}, {user.FirstName} ({user.SchoolId})",
                user.UserId
            }).ToList();

            userComboBox.DataSource = userDisplayList;
            userComboBox.DisplayMember = "DisplayName";
            userComboBox.ValueMember = "UserId";
        }

        private void LoadBooksToComboBox()
        {
            BookRepository bookRepository = new BookRepository();
            var books = bookRepository.GetAvailableBooks();

            var bookDisplayList = books.Select(book => new
            {
                DisplayName = $"{book.Title} by {book.Author} ({book.Year})",
                book.BookId
            }).ToList();

            bookComboBox.DataSource = bookDisplayList;
            bookComboBox.DisplayMember = "DisplayName";
            bookComboBox.ValueMember = "BookId";
        }

        public void refreshData()
        {
            LoadBooksToComboBox();
            LoadUsersToComboBox();
            LoadTransactions();
        }

        public void SubscribeToFormEvents(Form1 form1, Form2 form2)
        {
            form1._OnRefreshTransaction += (sender, e) => refreshData();
            form2._OnRefreshTransaction += (sender, e) => refreshData();
        }

        private void ClearAllComboBoxes()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is ComboBox)
                {
                    ((ComboBox)ctrl).Text = "";
                }
            }
        }

        private void Form3_Click(object sender, EventArgs e)
        {
            ClearAllComboBoxes();
            transactionsGridView.ClearSelection();
            transactionsGridView.CurrentCell = null;
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
    }
}