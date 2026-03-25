namespace sport
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
            panel1 = new Panel();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnCreate = new Button();
            bttnOrders = new Button();
            lblUserName = new Label();
            bttnLogout = new Button();
            dgvProducts = new DataGridView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnDelete);
            panel1.Controls.Add(btnUpdate);
            panel1.Controls.Add(btnCreate);
            panel1.Controls.Add(bttnOrders);
            panel1.Controls.Add(lblUserName);
            panel1.Controls.Add(bttnLogout);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(10, 10);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(0, 0, 0, 10);
            panel1.Size = new Size(1095, 81);
            panel1.TabIndex = 0;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(67, 97, 238);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnDelete.Location = new Point(427, 0);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(132, 71);
            btnDelete.TabIndex = 12;
            btnDelete.Text = "Удалить товар";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.FromArgb(67, 97, 238);
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnUpdate.Location = new Point(276, 0);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(145, 71);
            btnUpdate.TabIndex = 11;
            btnUpdate.Text = "Редактировать товар";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += BtnUpdate_Click;
            // 
            // btnCreate
            // 
            btnCreate.BackColor = Color.FromArgb(67, 97, 238);
            btnCreate.FlatStyle = FlatStyle.Flat;
            btnCreate.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnCreate.Location = new Point(138, 0);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(132, 71);
            btnCreate.TabIndex = 10;
            btnCreate.Text = "Добавить товар";
            btnCreate.UseVisualStyleBackColor = false;
            btnCreate.Click += BtnCreate_Click;
            // 
            // bttnOrders
            // 
            bttnOrders.BackColor = Color.FromArgb(67, 97, 238);
            bttnOrders.Dock = DockStyle.Left;
            bttnOrders.FlatStyle = FlatStyle.Flat;
            bttnOrders.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            bttnOrders.Location = new Point(0, 0);
            bttnOrders.Name = "bttnOrders";
            bttnOrders.Size = new Size(132, 71);
            bttnOrders.TabIndex = 9;
            bttnOrders.Text = "Посмотреть заказы";
            bttnOrders.UseVisualStyleBackColor = false;
            bttnOrders.Click += BttnOrders_Click;
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Dock = DockStyle.Right;
            lblUserName.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblUserName.Location = new Point(882, 0);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(60, 22);
            lblUserName.TabIndex = 8;
            lblUserName.Text = "label1";
            lblUserName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // bttnLogout
            // 
            bttnLogout.BackColor = Color.FromArgb(233, 245, 255);
            bttnLogout.Dock = DockStyle.Right;
            bttnLogout.FlatStyle = FlatStyle.Flat;
            bttnLogout.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            bttnLogout.Location = new Point(942, 0);
            bttnLogout.Name = "bttnLogout";
            bttnLogout.Size = new Size(153, 71);
            bttnLogout.TabIndex = 5;
            bttnLogout.Text = "Выход";
            bttnLogout.UseVisualStyleBackColor = false;
            bttnLogout.Click += BttnLogout_Click;
            // 
            // dgvProducts
            // 
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvProducts.BackgroundColor = Color.White;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.Location = new Point(10, 91);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.RowHeadersWidth = 51;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(1095, 552);
            dgvProducts.TabIndex = 1;
            dgvProducts.CellMouseClick += dgvProducts_CellMouseClick;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(1115, 653);
            Controls.Add(dgvProducts);
            Controls.Add(panel1);
            Name = "Form2";
            Padding = new Padding(10);
            Text = "Список товаров";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button bttnLogout;
        private Label lblUserName;
        private Button bttnOrders;
        private DataGridView dgvProducts;
        private Button btnCreate;
        private Button btnDelete;
        private Button btnUpdate;
    }
}