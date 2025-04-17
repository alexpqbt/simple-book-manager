namespace LibraryMgmt
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            label1 = new Label();
            idTextBox = new NumericTextBox();
            label2 = new Label();
            label3 = new Label();
            fnameTextBox = new TextBox();
            lnameTextBox = new TextBox();
            updateBtn = new Button();
            removeBtn = new Button();
            addButton = new Button();
            usersGridView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)usersGridView).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Roboto", 10F);
            label1.Location = new Point(25, 28);
            label1.Name = "label1";
            label1.Size = new Size(21, 17);
            label1.TabIndex = 0;
            label1.Text = "ID";
            // 
            // idTextBox
            // 
            idTextBox.Location = new Point(105, 25);
            idTextBox.MaxLength = 8;
            idTextBox.Name = "idTextBox";
            idTextBox.Size = new Size(102, 22);
            idTextBox.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Roboto", 10F);
            label2.Location = new Point(25, 53);
            label2.Name = "label2";
            label2.Size = new Size(74, 17);
            label2.TabIndex = 2;
            label2.Text = "First name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Roboto", 10F);
            label3.Location = new Point(25, 78);
            label3.Name = "label3";
            label3.Size = new Size(74, 17);
            label3.TabIndex = 3;
            label3.Text = "Last name";
            // 
            // fnameTextBox
            // 
            fnameTextBox.Location = new Point(105, 50);
            fnameTextBox.Name = "fnameTextBox";
            fnameTextBox.Size = new Size(267, 22);
            fnameTextBox.TabIndex = 2;
            // 
            // lnameTextBox
            // 
            lnameTextBox.Location = new Point(105, 76);
            lnameTextBox.Name = "lnameTextBox";
            lnameTextBox.Size = new Size(267, 22);
            lnameTextBox.TabIndex = 3;
            // 
            // updateBtn
            // 
            updateBtn.Font = new Font("Roboto", 10F);
            updateBtn.Location = new Point(197, 113);
            updateBtn.Name = "updateBtn";
            updateBtn.Size = new Size(80, 25);
            updateBtn.TabIndex = 6;
            updateBtn.Text = "Update";
            updateBtn.UseVisualStyleBackColor = true;
            updateBtn.Click += updateBtn_Click;
            // 
            // removeBtn
            // 
            removeBtn.Font = new Font("Roboto", 10F);
            removeBtn.Location = new Point(111, 113);
            removeBtn.Name = "removeBtn";
            removeBtn.Size = new Size(80, 25);
            removeBtn.TabIndex = 5;
            removeBtn.Text = "Remove";
            removeBtn.UseVisualStyleBackColor = true;
            removeBtn.Click += removeBtn_Click;
            // 
            // addButton
            // 
            addButton.Font = new Font("Roboto", 10F);
            addButton.Location = new Point(25, 113);
            addButton.Name = "addButton";
            addButton.Size = new Size(80, 25);
            addButton.TabIndex = 4;
            addButton.Text = "Add";
            addButton.UseVisualStyleBackColor = true;
            addButton.Click += addButton_Click;
            // 
            // usersGridView
            // 
            usersGridView.AllowUserToAddRows = false;
            usersGridView.AllowUserToDeleteRows = false;
            usersGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            usersGridView.Location = new Point(25, 165);
            usersGridView.Name = "usersGridView";
            usersGridView.ReadOnly = true;
            usersGridView.Size = new Size(513, 245);
            usersGridView.TabIndex = 7;
            usersGridView.CellClick += usersGridView_CellClick;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(564, 435);
            ControlBox = false;
            Controls.Add(usersGridView);
            Controls.Add(updateBtn);
            Controls.Add(removeBtn);
            Controls.Add(addButton);
            Controls.Add(lnameTextBox);
            Controls.Add(fnameTextBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(idTextBox);
            Controls.Add(label1);
            Font = new Font("Roboto", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "User Management";
            Load += Form2_Load;
            Click += Form2_Click;
            ((System.ComponentModel.ISupportInitialize)usersGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private NumericTextBox idTextBox;
        private Label label2;
        private Label label3;
        private TextBox fnameTextBox;
        private TextBox lnameTextBox;
        private Button updateBtn;
        private Button removeBtn;
        private Button addButton;
        private DataGridView usersGridView;
    }
}