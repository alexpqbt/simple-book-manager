namespace LibraryMgmt
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            userComboBox = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            borrowedDatePicker = new DateTimePicker();
            bookComboBox = new ComboBox();
            dueDatePicker = new DateTimePicker();
            transactionsGridView = new DataGridView();
            borrowButton = new Button();
            returnButton = new Button();
            ((System.ComponentModel.ISupportInitialize)transactionsGridView).BeginInit();
            SuspendLayout();
            // 
            // userComboBox
            // 
            userComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            userComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            userComboBox.FormattingEnabled = true;
            userComboBox.Location = new Point(119, 25);
            userComboBox.Name = "userComboBox";
            userComboBox.Size = new Size(267, 22);
            userComboBox.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Roboto", 10F);
            label1.Location = new Point(25, 27);
            label1.Name = "label1";
            label1.Size = new Size(36, 17);
            label1.TabIndex = 1;
            label1.Text = "User";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Roboto", 10F);
            label2.Location = new Point(25, 56);
            label2.Name = "label2";
            label2.Size = new Size(40, 17);
            label2.TabIndex = 2;
            label2.Text = "Book";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Roboto", 10F);
            label3.Location = new Point(25, 85);
            label3.Name = "label3";
            label3.Size = new Size(88, 17);
            label3.TabIndex = 3;
            label3.Text = "Borrowed on";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Roboto", 10F);
            label4.Location = new Point(25, 114);
            label4.Name = "label4";
            label4.Size = new Size(63, 17);
            label4.TabIndex = 4;
            label4.Text = "Due date";
            // 
            // borrowedDatePicker
            // 
            borrowedDatePicker.Format = DateTimePickerFormat.Short;
            borrowedDatePicker.Location = new Point(119, 81);
            borrowedDatePicker.Name = "borrowedDatePicker";
            borrowedDatePicker.Size = new Size(128, 22);
            borrowedDatePicker.TabIndex = 5;
            // 
            // bookComboBox
            // 
            bookComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            bookComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            bookComboBox.FormattingEnabled = true;
            bookComboBox.Location = new Point(119, 53);
            bookComboBox.Name = "bookComboBox";
            bookComboBox.Size = new Size(267, 22);
            bookComboBox.TabIndex = 6;
            // 
            // dueDatePicker
            // 
            dueDatePicker.Format = DateTimePickerFormat.Short;
            dueDatePicker.Location = new Point(119, 109);
            dueDatePicker.Name = "dueDatePicker";
            dueDatePicker.Size = new Size(128, 22);
            dueDatePicker.TabIndex = 7;
            // 
            // transactionsGridView
            // 
            transactionsGridView.AllowUserToAddRows = false;
            transactionsGridView.AllowUserToDeleteRows = false;
            transactionsGridView.AllowUserToOrderColumns = true;
            transactionsGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            transactionsGridView.Location = new Point(25, 189);
            transactionsGridView.Name = "transactionsGridView";
            transactionsGridView.ReadOnly = true;
            transactionsGridView.Size = new Size(608, 252);
            transactionsGridView.TabIndex = 8;
            transactionsGridView.CellFormatting += transactionsGridView_CellFormatting;
            transactionsGridView.RowPrePaint += transactionsGridView_RowPrePaint;
            // 
            // borrowButton
            // 
            borrowButton.Font = new Font("Roboto", 10F);
            borrowButton.Location = new Point(25, 151);
            borrowButton.Name = "borrowButton";
            borrowButton.Size = new Size(75, 23);
            borrowButton.TabIndex = 9;
            borrowButton.Text = "Borrow";
            borrowButton.UseVisualStyleBackColor = true;
            borrowButton.Click += borrowButton_Click;
            // 
            // returnButton
            // 
            returnButton.Font = new Font("Roboto", 10F);
            returnButton.Location = new Point(106, 151);
            returnButton.Name = "returnButton";
            returnButton.Size = new Size(75, 23);
            returnButton.TabIndex = 10;
            returnButton.Text = "Return";
            returnButton.UseVisualStyleBackColor = true;
            returnButton.Click += returnButton_Click;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(664, 468);
            ControlBox = false;
            Controls.Add(returnButton);
            Controls.Add(borrowButton);
            Controls.Add(transactionsGridView);
            Controls.Add(dueDatePicker);
            Controls.Add(bookComboBox);
            Controls.Add(borrowedDatePicker);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(userComboBox);
            Font = new Font("Roboto", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "Form3";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Transaction";
            Load += Form3_Load;
            Click += Form3_Click;
            ((System.ComponentModel.ISupportInitialize)transactionsGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox userComboBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private DateTimePicker borrowedDatePicker;
        private ComboBox bookComboBox;
        private DateTimePicker dueDatePicker;
        private DataGridView transactionsGridView;
        private Button borrowButton;
        private Button returnButton;
    }
}