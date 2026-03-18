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
            panel1.Controls.Add(bttnOrders);
            panel1.Controls.Add(lblUserName);
            panel1.Controls.Add(bttnLogout);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(10, 10);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(0, 0, 0, 10);
            panel1.Size = new Size(962, 50);
            panel1.TabIndex = 0;
            // 
            // bttnOrders
            // 
            bttnOrders.BackColor = Color.FromArgb(67, 97, 238);
            bttnOrders.Dock = DockStyle.Left;
            bttnOrders.FlatStyle = FlatStyle.Flat;
            bttnOrders.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            bttnOrders.Location = new Point(0, 0);
            bttnOrders.Name = "bttnOrders";
            bttnOrders.Size = new Size(194, 40);
            bttnOrders.TabIndex = 9;
            bttnOrders.Text = "Посмотреть товары";
            bttnOrders.UseVisualStyleBackColor = false;
            bttnOrders.Click += BttnOrders_Click;
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Dock = DockStyle.Right;
            lblUserName.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblUserName.Location = new Point(749, 0);
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
            bttnLogout.Location = new Point(809, 0);
            bttnLogout.Name = "bttnLogout";
            bttnLogout.Size = new Size(153, 40);
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
            dgvProducts.Location = new Point(10, 60);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.RowHeadersWidth = 51;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(962, 583);
            dgvProducts.TabIndex = 1;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(982, 653);
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
    }
}