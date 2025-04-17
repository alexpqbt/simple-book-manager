namespace LibraryMgmt
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            addButton = new Button();
            removeBtn = new Button();
            updateBtn = new Button();
            titleTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            authorTextBox = new TextBox();
            genreTextBox = new TextBox();
            booksGridView = new DataGridView();
            yearTextBox = new NumericTextBox();
            ((System.ComponentModel.ISupportInitialize)booksGridView).BeginInit();
            SuspendLayout();
            // 
            // addButton
            // 
            addButton.Font = new Font("Roboto", 10F);
            addButton.Location = new Point(25, 139);
            addButton.Name = "addButton";
            addButton.Size = new Size(80, 25);
            addButton.TabIndex = 5;
            addButton.Text = "Add";
            addButton.UseVisualStyleBackColor = true;
            addButton.Click += addButton_Click;
            // 
            // removeBtn
            // 
            removeBtn.Font = new Font("Roboto", 10F);
            removeBtn.Location = new Point(111, 139);
            removeBtn.Name = "removeBtn";
            removeBtn.Size = new Size(80, 25);
            removeBtn.TabIndex = 6;
            removeBtn.Text = "Remove";
            removeBtn.UseVisualStyleBackColor = true;
            removeBtn.Click += removeBtn_Click;
            // 
            // updateBtn
            // 
            updateBtn.Font = new Font("Roboto", 10F);
            updateBtn.Location = new Point(197, 139);
            updateBtn.Name = "updateBtn";
            updateBtn.Size = new Size(80, 25);
            updateBtn.TabIndex = 7;
            updateBtn.Text = "Update";
            updateBtn.UseVisualStyleBackColor = true;
            updateBtn.Click += updateBtn_Click;
            // 
            // titleTextBox
            // 
            titleTextBox.Font = new Font("Roboto", 9F);
            titleTextBox.Location = new Point(82, 26);
            titleTextBox.Name = "titleTextBox";
            titleTextBox.Size = new Size(267, 22);
            titleTextBox.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Roboto", 10F);
            label1.Location = new Point(25, 28);
            label1.Name = "label1";
            label1.Size = new Size(34, 17);
            label1.TabIndex = 15;
            label1.Text = "Title";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Roboto", 10F);
            label2.Location = new Point(25, 53);
            label2.Name = "label2";
            label2.Size = new Size(51, 17);
            label2.TabIndex = 16;
            label2.Text = "Author";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Roboto", 10F);
            label3.Location = new Point(25, 78);
            label3.Name = "label3";
            label3.Size = new Size(36, 17);
            label3.TabIndex = 17;
            label3.Text = "Year";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Roboto", 10F);
            label4.Location = new Point(25, 104);
            label4.Name = "label4";
            label4.Size = new Size(45, 17);
            label4.TabIndex = 18;
            label4.Text = "Genre";
            // 
            // authorTextBox
            // 
            authorTextBox.Font = new Font("Roboto", 9F);
            authorTextBox.Location = new Point(82, 51);
            authorTextBox.Name = "authorTextBox";
            authorTextBox.Size = new Size(267, 22);
            authorTextBox.TabIndex = 2;
            // 
            // genreTextBox
            // 
            genreTextBox.Font = new Font("Roboto", 9F);
            genreTextBox.Location = new Point(82, 104);
            genreTextBox.Name = "genreTextBox";
            genreTextBox.Size = new Size(267, 22);
            genreTextBox.TabIndex = 4;
            // 
            // booksGridView
            // 
            booksGridView.AllowUserToAddRows = false;
            booksGridView.AllowUserToDeleteRows = false;
            booksGridView.AllowUserToOrderColumns = true;
            booksGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            booksGridView.Location = new Point(25, 189);
            booksGridView.Name = "booksGridView";
            booksGridView.ReadOnly = true;
            booksGridView.Size = new Size(569, 221);
            booksGridView.TabIndex = 8;
            booksGridView.CellClick += booksGridView_CellClick;
            booksGridView.DataBindingComplete += booksGridView_DataBindingComplete;
            // 
            // yearTextBox
            // 
            yearTextBox.Location = new Point(82, 78);
            yearTextBox.Name = "yearTextBox";
            yearTextBox.Size = new Size(102, 22);
            yearTextBox.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(625, 435);
            Controls.Add(yearTextBox);
            Controls.Add(booksGridView);
            Controls.Add(genreTextBox);
            Controls.Add(authorTextBox);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(titleTextBox);
            Controls.Add(updateBtn);
            Controls.Add(removeBtn);
            Controls.Add(addButton);
            Font = new Font("Roboto", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Book Management";
            Load += Form1_Load;
            Click += Form1_Click;
            ((System.ComponentModel.ISupportInitialize)booksGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button addButton;
        private Button removeBtn;
        private Button updateBtn;
        private TextBox titleTextBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox authorTextBox;
        private TextBox genreTextBox;
        private DataGridView booksGridView;
        private NumericTextBox yearTextBox;
    }
}
