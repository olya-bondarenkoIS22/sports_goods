using Microsoft.EntityFrameworkCore;
using sport.Models;

namespace sport
{
    public partial class Form1 : Form
    {
        public User CurrentUser { get; private set; }
        public bool IsGuest { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void BttnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLogin.Text) ||
                string.IsNullOrEmpty(textBoxPassword.Text))
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var db = new SportingGoodsStoreContext())
            {
                var user = db.Users
                    .Where(w => w.Login == textBoxLogin.Text && w.Password == textBoxPassword.Text)
                    .Include(u => u.IdRoleNavigation)
                    .FirstOrDefault();
                if (user != null)
                {
                    CurrentUser = user;
                    IsGuest = false;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
        }
        private void BttnGuest_Click(object sender, EventArgs e)
        {
            CurrentUser = null;
            IsGuest = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
