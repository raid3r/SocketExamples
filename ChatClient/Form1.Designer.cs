using ChatCoreLibrary.Models;

namespace ChatClient
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
            components = new System.ComponentModel.Container();
            button1 = new Button();
            textBox1 = new TextBox();
            dataGridView1 = new DataGridView();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            FromLogin = new DataGridViewTextBoxColumn();
            ToLogin = new DataGridViewTextBoxColumn();
            dateTimeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            textDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            fileDataDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            chatMessageBindingSource = new BindingSource(components);
            button2 = new Button();
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            timer1 = new System.Windows.Forms.Timer(components);
            usersListBox = new ListBox();
            meLoginLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chatMessageBindingSource).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(391, 415);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Send";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 416);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(280, 23);
            textBox1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { idDataGridViewTextBoxColumn, FromLogin, ToLogin, dateTimeDataGridViewTextBoxColumn, textDataGridViewTextBoxColumn, fileDataDataGridViewTextBoxColumn });
            dataGridView1.DataSource = chatMessageBindingSource;
            dataGridView1.Location = new Point(193, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(718, 397);
            dataGridView1.TabIndex = 2;
            dataGridView1.CellContentClick += downloadButtonClick;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            idDataGridViewTextBoxColumn.HeaderText = "Id";
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // FromLogin
            // 
            FromLogin.DataPropertyName = "FromLogin";
            FromLogin.HeaderText = "From";
            FromLogin.Name = "FromLogin";
            FromLogin.ReadOnly = true;
            // 
            // ToLogin
            // 
            ToLogin.DataPropertyName = "ToLogin";
            ToLogin.HeaderText = "To";
            ToLogin.Name = "ToLogin";
            ToLogin.ReadOnly = true;
            // 
            // dateTimeDataGridViewTextBoxColumn
            // 
            dateTimeDataGridViewTextBoxColumn.DataPropertyName = "DateTime";
            dateTimeDataGridViewTextBoxColumn.HeaderText = "DateTime";
            dateTimeDataGridViewTextBoxColumn.Name = "dateTimeDataGridViewTextBoxColumn";
            // 
            // textDataGridViewTextBoxColumn
            // 
            textDataGridViewTextBoxColumn.DataPropertyName = "Text";
            textDataGridViewTextBoxColumn.HeaderText = "Text";
            textDataGridViewTextBoxColumn.Name = "textDataGridViewTextBoxColumn";
            // 
            // fileDataDataGridViewTextBoxColumn
            // 
            fileDataDataGridViewTextBoxColumn.DataPropertyName = "FileData";
            fileDataDataGridViewTextBoxColumn.HeaderText = "FileData";
            fileDataDataGridViewTextBoxColumn.Name = "fileDataDataGridViewTextBoxColumn";
            // 
            // chatMessageBindingSource
            // 
            chatMessageBindingSource.DataSource = typeof(ChatMessage);
            // 
            // button2
            // 
            button2.Location = new Point(310, 416);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 3;
            button2.Text = "Send File";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // timer1
            // 
            timer1.Interval = 3000;
            timer1.Tick += timer1_Tick;
            // 
            // usersListBox
            // 
            usersListBox.FormattingEnabled = true;
            usersListBox.ItemHeight = 15;
            usersListBox.Location = new Point(7, 57);
            usersListBox.Name = "usersListBox";
            usersListBox.Size = new Size(180, 349);
            usersListBox.TabIndex = 4;
            usersListBox.SelectedIndexChanged += usersListBox_SelectedIndexChanged;
            // 
            // meLoginLabel
            // 
            meLoginLabel.AutoSize = true;
            meLoginLabel.Location = new Point(12, 12);
            meLoginLabel.Name = "meLoginLabel";
            meLoginLabel.Size = new Size(38, 15);
            meLoginLabel.TabIndex = 5;
            meLoginLabel.Text = "label1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(923, 450);
            Controls.Add(meLoginLabel);
            Controls.Add(usersListBox);
            Controls.Add(button2);
            Controls.Add(dataGridView1);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)chatMessageBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private DataGridView dataGridView1;
        private Button button2;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Timer timer1;
        private ListBox usersListBox;
        private BindingSource chatMessageBindingSource;
        private DataGridViewTextBoxColumn fromDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn toDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn FromLogin;
        private DataGridViewTextBoxColumn ToLogin;
        private DataGridViewTextBoxColumn dateTimeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn textDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn fileDataDataGridViewTextBoxColumn;
        private Label meLoginLabel;
    }
}
