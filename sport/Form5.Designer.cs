namespace sport
{
    partial class Form5
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
            monthCalendar = new MonthCalendar();
            label1 = new Label();
            label2 = new Label();
            cbAddess = new ComboBox();
            label3 = new Label();
            cbUser = new ComboBox();
            label4 = new Label();
            tbCode = new TextBox();
            label5 = new Label();
            cbStatus = new ComboBox();
            btnCreate = new Button();
            SuspendLayout();
            // 
            // monthCalendar
            // 
            monthCalendar.Location = new Point(29, 86);
            monthCalendar.Margin = new Padding(12, 10, 12, 10);
            monthCalendar.Name = "monthCalendar";
            monthCalendar.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 61);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(138, 22);
            label1.TabIndex = 1;
            label1.Text = "Дата доставки:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(301, 24);
            label2.Name = "label2";
            label2.Size = new Size(214, 22);
            label2.TabIndex = 2;
            label2.Text = "Адрес пункта доставки:";
            // 
            // cbAddess
            // 
            cbAddess.FormattingEnabled = true;
            cbAddess.Location = new Point(301, 58);
            cbAddess.Name = "cbAddess";
            cbAddess.Size = new Size(267, 30);
            cbAddess.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(301, 100);
            label3.Name = "label3";
            label3.Size = new Size(131, 22);
            label3.TabIndex = 4;
            label3.Text = "Пользователь:";
            // 
            // cbUser
            // 
            cbUser.FormattingEnabled = true;
            cbUser.Location = new Point(301, 134);
            cbUser.Name = "cbUser";
            cbUser.Size = new Size(267, 30);
            cbUser.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(301, 176);
            label4.Name = "label4";
            label4.Size = new Size(47, 22);
            label4.TabIndex = 6;
            label4.Text = "Код:";
            // 
            // tbCode
            // 
            tbCode.Location = new Point(301, 210);
            tbCode.Name = "tbCode";
            tbCode.Size = new Size(267, 30);
            tbCode.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(301, 252);
            label5.Name = "label5";
            label5.Size = new Size(73, 22);
            label5.TabIndex = 8;
            label5.Text = "Статус:";
            // 
            // cbStatus
            // 
            cbStatus.FormattingEnabled = true;
            cbStatus.Location = new Point(301, 286);
            cbStatus.Name = "cbStatus";
            cbStatus.Size = new Size(267, 30);
            cbStatus.TabIndex = 9;
            // 
            // btnCreate
            // 
            btnCreate.BackColor = Color.FromArgb(67, 97, 238);
            btnCreate.FlatStyle = FlatStyle.Flat;
            btnCreate.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnCreate.Location = new Point(29, 347);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(539, 46);
            btnCreate.TabIndex = 13;
            btnCreate.Text = "Добавить";
            btnCreate.UseVisualStyleBackColor = false;
            btnCreate.Click += btnCreate_Click;
            // 
            // Form5
            // 
            AutoScaleDimensions = new SizeF(11F, 22F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(596, 405);
            Controls.Add(btnCreate);
            Controls.Add(cbStatus);
            Controls.Add(label5);
            Controls.Add(tbCode);
            Controls.Add(label4);
            Controls.Add(cbUser);
            Controls.Add(label3);
            Controls.Add(cbAddess);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(monthCalendar);
            Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form5";
            Text = "Добавление заказа";
            Load += Form5_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MonthCalendar monthCalendar;
        private Label label1;
        private Label label2;
        private ComboBox cbAddess;
        private Label label3;
        private ComboBox cbUser;
        private Label label4;
        private TextBox tbCode;
        private Label label5;
        private ComboBox cbStatus;
        private Button btnCreate;
    }
}