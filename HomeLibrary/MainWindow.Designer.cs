namespace HomeLibrary
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            btnUpdate = new Button();
            btnDelete = new Button();
            btnCreate = new Button();
            dataGridView1 = new DataGridView();
            panel2 = new Panel();
            btnClearInfo = new Button();
            cbGenre = new ComboBox();
            rtbDescription = new RichTextBox();
            tbName = new TextBox();
            cbAuthor = new ComboBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            panel3 = new Panel();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(84, 327);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 2;
            btnUpdate.Text = "Изменить";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(165, 327);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 1;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(3, 327);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(75, 23);
            btnCreate.TabIndex = 0;
            btnCreate.Text = "Добавить";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 12);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(443, 356);
            dataGridView1.TabIndex = 4;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(btnClearInfo);
            panel2.Controls.Add(cbGenre);
            panel2.Controls.Add(btnDelete);
            panel2.Controls.Add(btnCreate);
            panel2.Controls.Add(btnUpdate);
            panel2.Controls.Add(rtbDescription);
            panel2.Controls.Add(tbName);
            panel2.Controls.Add(cbAuthor);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(461, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(250, 356);
            panel2.TabIndex = 6;
            // 
            // btnClearInfo
            // 
            btnClearInfo.Location = new Point(165, 6);
            btnClearInfo.Name = "btnClearInfo";
            btnClearInfo.Size = new Size(75, 23);
            btnClearInfo.TabIndex = 11;
            btnClearInfo.Text = "Очистить";
            btnClearInfo.UseVisualStyleBackColor = true;
            btnClearInfo.Click += btnClearInfo_Click;
            // 
            // cbGenre
            // 
            cbGenre.FormattingEnabled = true;
            cbGenre.Location = new Point(3, 139);
            cbGenre.Name = "cbGenre";
            cbGenre.Size = new Size(237, 23);
            cbGenre.TabIndex = 10;
            cbGenre.DropDown += cbGenre_DropDown;
            // 
            // rtbDescription
            // 
            rtbDescription.Location = new Point(3, 183);
            rtbDescription.Name = "rtbDescription";
            rtbDescription.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbDescription.Size = new Size(237, 138);
            rtbDescription.TabIndex = 9;
            rtbDescription.Text = "";
            // 
            // tbName
            // 
            tbName.Location = new Point(3, 51);
            tbName.Name = "tbName";
            tbName.Size = new Size(237, 23);
            tbName.TabIndex = 8;
            // 
            // cbAuthor
            // 
            cbAuthor.FormattingEnabled = true;
            cbAuthor.Location = new Point(3, 95);
            cbAuthor.Name = "cbAuthor";
            cbAuthor.Size = new Size(237, 23);
            cbAuthor.TabIndex = 6;
            cbAuthor.DropDown += cbAuthor_DropDown;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(3, 165);
            label5.Name = "label5";
            label5.Size = new Size(65, 15);
            label5.TabIndex = 4;
            label5.Text = "Описание:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 121);
            label4.Name = "label4";
            label4.Size = new Size(41, 15);
            label4.TabIndex = 3;
            label4.Text = "Жанр:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 77);
            label3.Name = "label3";
            label3.Size = new Size(43, 15);
            label3.TabIndex = 2;
            label3.Text = "Автор:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 33);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 1;
            label2.Text = "Название:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(3, 2);
            label1.Name = "label1";
            label1.Size = new Size(139, 25);
            label1.TabIndex = 0;
            label1.Text = "Текущая книга";
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(label6);
            panel3.Location = new Point(12, 374);
            panel3.Name = "panel3";
            panel3.Size = new Size(699, 36);
            panel3.TabIndex = 7;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(3, 9);
            label6.Name = "label6";
            label6.Size = new Size(38, 15);
            label6.TabIndex = 0;
            label6.Text = "label6";
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(724, 420);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(dataGridView1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainWindow";
            Text = "Моя домашняя библиотека";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnUpdate;
        private Button btnDelete;
        private Button btnCreate;
        private DataGridView dataGridView1;
        private Panel panel2;
        private Label label2;
        private Label label1;
        private RichTextBox rtbDescription;
        private TextBox tbName;
        private ComboBox cbAuthor;
        private Label label5;
        private Label label4;
        private Label label3;
        private Panel panel3;
        private Label label6;
        private ComboBox cbGenre;
        private Button btnClearInfo;
    }
}
