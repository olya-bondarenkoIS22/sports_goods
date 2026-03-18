namespace sport
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
            panel1 = new Panel();
            bttnGuest = new Button();
            bttnLogin = new Button();
            textBoxPassword = new TextBox();
            label2 = new Label();
            label1 = new Label();
            textBoxLogin = new TextBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(bttnGuest);
            panel1.Controls.Add(bttnLogin);
            panel1.Controls.Add(textBoxPassword);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(textBoxLogin);
            panel1.Location = new Point(51, 26);
            panel1.Name = "panel1";
            panel1.Size = new Size(337, 214);
            panel1.TabIndex = 0;
            // 
            // bttnGuest
            // 
            bttnGuest.BackColor = Color.FromArgb(67, 97, 238);
            bttnGuest.FlatStyle = FlatStyle.Flat;
            bttnGuest.Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            bttnGuest.Location = new Point(92, 174);
            bttnGuest.Name = "bttnGuest";
            bttnGuest.Size = new Size(153, 29);
            bttnGuest.TabIndex = 5;
            bttnGuest.Text = "Войти как гость";
            bttnGuest.UseVisualStyleBackColor = false;
            bttnGuest.Click += BttnGuest_Click;
            // 
            // bttnLogin
            // 
            bttnLogin.BackColor = Color.FromArgb(233, 245, 255);
            bttnLogin.FlatStyle = FlatStyle.Flat;
            bttnLogin.Location = new Point(92, 139);
            bttnLogin.Name = "bttnLogin";
            bttnLogin.Size = new Size(153, 29);
            bttnLogin.TabIndex = 4;
            bttnLogin.Text = "Войти";
            bttnLogin.UseVisualStyleBackColor = false;
            bttnLogin.Click += BttnLogin_Click;
            // 
            // textBoxPassword
            // 
            textBoxPassword.Location = new Point(50, 103);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.Size = new Size(236, 30);
            textBoxPassword.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(129, 75);
            label2.Name = "label2";
            label2.Size = new Size(78, 22);
            label2.TabIndex = 2;
            label2.Text = "Пароль:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(133, 11);
            label1.Name = "label1";
            label1.Size = new Size(70, 22);
            label1.TabIndex = 1;
            label1.Text = "Логин:";
            // 
            // textBoxLogin
            // 
            textBoxLogin.Location = new Point(50, 39);
            textBoxLogin.Name = "textBoxLogin";
            textBoxLogin.Size = new Size(236, 30);
            textBoxLogin.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 22F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(438, 267);
            Controls.Add(panel1);
            Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Вход в систему";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button bttnLogin;
        private TextBox textBoxPassword;
        private Label label2;
        private Label label1;
        private TextBox textBoxLogin;
        private Button bttnGuest;
    }
}
