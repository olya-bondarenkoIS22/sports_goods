using sport.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace sport
{
    public partial class Form5 : Form
    {
        Order editableOrder;

        public Form5()
        {
            InitializeComponent();
        }

        public Form5(Order editableOrder)
        {
            InitializeComponent();
            this.editableOrder = editableOrder;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            using (var db = new SportingGoodsStoreContext())
            {
                List<AddressesOfPickUpPoint> pointPickUp = db.AddressesOfPickUpPoints.ToList();
                List<User> users = db.Users.ToList();
                List<Status> status = db.Statuses.ToList();

                // Сначала очищаем
                cbAddess.Items.Clear();
                cbUser.Items.Clear();
                cbStatus.Items.Clear();

                // Сначала задаем DataSource
                cbAddess.DataSource = pointPickUp;
                cbUser.DataSource = users;
                cbStatus.DataSource = status;

                // Потом указываем DisplayMember и ValueMember
                cbAddess.DisplayMember = "Address"; // здесь поле модели, которое показываем
                cbAddess.ValueMember = "Id";

                cbUser.DisplayMember = "FullName"; // замените на поле, которое хотите показать
                cbUser.ValueMember = "Id";

                cbStatus.DisplayMember = "Status1"; // замените на нужное поле
                cbStatus.ValueMember = "Id";

                // Только после этого можно установить SelectedValue
                if (editableOrder != null)
                {
                    tbCode.Text = editableOrder.Code;

                    if (editableOrder.DeliveryDate.HasValue)
                        monthCalendar.SetDate(editableOrder.DeliveryDate.Value.ToDateTime(TimeOnly.MinValue));

                    cbAddess.SelectedValue = editableOrder.IdDeliveryPointAddress;
                    cbUser.SelectedValue = editableOrder.IdUser;
                    cbStatus.SelectedValue = editableOrder.IdStatus;

                    btnCreate.Text = "Сохранить";
                }
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new SportingGoodsStoreContext())
                {
                    var selectedAddress = cbAddess.SelectedItem as AddressesOfPickUpPoint;
                    var selectedUser = cbUser.SelectedItem as User;
                    var selectedStatus = cbStatus.SelectedItem as Status;

                    if (selectedAddress == null || selectedUser == null || selectedStatus == null)
                    {
                        MessageBox.Show("Заполните все поля!");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(tbCode.Text))
                    {
                        MessageBox.Show("Введите код заказа!");
                        return;
                    }

                    if (editableOrder == null)
                    {
                        
                        Order newOrder = new Order()
                        {
                            OrderDate = DateOnly.FromDateTime(DateTime.Today),
                            DeliveryDate = DateOnly.FromDateTime(monthCalendar.SelectionStart),
                            IdDeliveryPointAddress = selectedAddress.Id,
                            IdUser = selectedUser.Id,
                            IdStatus = selectedStatus.Id,
                            Code = tbCode.Text
                        };

                        db.Orders.Add(newOrder);
                    }
                    else
                    {
                        
                        var orderFromDb = db.Orders.FirstOrDefault(o => o.Id == editableOrder.Id);

                        if (orderFromDb == null)
                        {
                            MessageBox.Show("Заказ не найден!");
                            return;
                        }

                        orderFromDb.DeliveryDate = DateOnly.FromDateTime(monthCalendar.SelectionStart);
                        orderFromDb.IdDeliveryPointAddress = selectedAddress.Id;
                        orderFromDb.IdUser = selectedUser.Id;
                        orderFromDb.IdStatus = selectedStatus.Id;
                        orderFromDb.Code = tbCode.Text;
                    }

                    db.SaveChanges();

                    MessageBox.Show(editableOrder == null
                        ? "Заказ успешно добавлен!"
                        : "Заказ успешно обновлён!");

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}
