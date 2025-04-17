using LibraryMgmt.DataAccess;
using LibraryMgmt.Models;

namespace LibraryMgmt
{
    public partial class Form1 : Form
    {
        private BookRepository _bookRepository;
        private Form2 _userMgmtForm;
        private Form3 _transactionForm;
        public event EventHandler? _OnRefreshTransaction;

        public Form1()
        {
            InitializeComponent();
            _bookRepository = new BookRepository();
            _userMgmtForm = new Form2();
            _transactionForm = new Form3();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AcceptButton = addButton;
            LoadBooks();
            CustomizeGridView(booksGridView);
            CustomizeGridColumns(booksGridView);
            OpenUserMgmtForm();
            OpenTransactionForm();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            ClearAllTextBoxes();
            booksGridView.ClearSelection();
            booksGridView.CurrentCell = null;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var book = new Book
            {
                Title = titleTextBox.Text,
                Author = authorTextBox.Text,
                Year = Convert.ToInt32(yearTextBox.Text),
                Genre = genreTextBox.Text
            };

            if (_bookRepository.BookExist(book))
            {
                MessageBox.Show("Book already exists.");
                ClearAllTextBoxes();
                return;
            }

            _bookRepository.AddBook(book);

            ClearAllTextBoxes();
            LoadBooks();

            _OnRefreshTransaction?.Invoke(this, EventArgs.Empty);
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {

            if (booksGridView.SelectedRows.Count > 0)
            {
                int bookId = Convert.ToInt32(booksGridView.SelectedRows[0].Cells[0].Value);

                _bookRepository.DeleteBook(bookId);

                LoadBooks();
                ClearAllTextBoxes();
                booksGridView.ClearSelection();
                booksGridView.CurrentCell = null;

                _OnRefreshTransaction?.Invoke(this, EventArgs.Empty);
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            var book = new Book
            {
                BookId = Convert.ToInt32(booksGridView.Rows[0].Cells[0].Value),
                Title = titleTextBox.Text,
                Author = authorTextBox.Text,
                Year = Convert.ToInt32(yearTextBox.Text),
                Genre = genreTextBox.Text
            };

            _bookRepository.UpdateBook(book);

            LoadBooks();
            ClearAllTextBoxes();
            booksGridView.ClearSelection();
            booksGridView.CurrentCell = null;

            _OnRefreshTransaction?.Invoke(this, EventArgs.Empty);
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

        private void booksGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            booksGridView.ClearSelection();
            booksGridView.CurrentCell = null;
        }

        private void LoadBooks()
        {
            booksGridView.DataSource = _bookRepository.GetAllBooks();
        }

        private void OpenUserMgmtForm()
        {
            _userMgmtForm.Show();
            _userMgmtForm.Activate();
        }

        private void OpenTransactionForm()
        {
            _transactionForm.SubscribeToFormEvents(this, _userMgmtForm);
            _transactionForm.Show();
            _transactionForm.Activate();
        }

        public void BringToFront(Form activeForm)
        {
            this.Activate();

            if (_userMgmtForm != null && _userMgmtForm != activeForm)
            {
                _userMgmtForm.Activate();
            }

            if (_transactionForm != null && _transactionForm != activeForm)
            {
                _transactionForm.Activate();
            }
        }

        public void CustomizeGridView(DataGridView grid)
        {
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Roboto", 9, FontStyle.Bold);
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.RowHeadersVisible = false;
            grid.Columns[0].Width = 50;
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

        public void CustomizeGridColumns(DataGridView grid)
        {
            foreach (DataGridViewColumn col in grid.Columns)
            {
                if (col.Index == 0)
                {
                    col.Visible = false;
                }
                else
                {
                    col.HeaderText = col.HeaderText.ToUpper();
                }
            }
        }
    }
}
