using Microsoft.EntityFrameworkCore;
using sport.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Text;
using System.Windows.Forms;

namespace sport
{
    public partial class Form3 : Form
    {
        public Models.User CurrentUser { get; private set; }
        public bool IsGuest { get; private set; }

        private Order selectedOrder;
        public Form3(Models.User user, bool quest, string currentRole)
        {
            InitializeComponent();

            dgvOrders.AutoGenerateColumns = false;
            dgvOrders.RowHeadersVisible = false;

            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            var colDate = new DataGridViewTextBoxColumn();
            colDate.Name = "colDate";
            colDate.FillWeight = 20;
            colDate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;


            var colUserDelivery = new DataGridViewTextBoxColumn();
            colUserDelivery.Name = "colUserDelivery";
            colUserDelivery.FillWeight = 50;
            colUserDelivery.DefaultCellStyle.WrapMode = DataGridViewTriState.True; ;

            var colCode = new DataGridViewTextBoxColumn();
            colCode.Name = "colCode";
            colCode.FillWeight = 10;
            colCode.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            var colStatus = new DataGridViewTextBoxColumn();
            colStatus.Name = "colStatus";
            colStatus.FillWeight = 20;
            colStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvOrders.Columns.AddRange(
           [
               colDate, colUserDelivery, colCode, colStatus
           ]);

            dgvOrders.EnableHeadersVisualStyles = false;
            dgvOrders.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            dgvOrders.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
            dgvOrders.ColumnHeadersDefaultCellStyle.Font = new Font(dgvOrders.Font, FontStyle.Bold);
            dgvOrders.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvOrders.ColumnHeadersDefaultCellStyle.SelectionBackColor = SystemColors.Control;
            dgvOrders.ColumnHeadersDefaultCellStyle.SelectionForeColor = SystemColors.ControlText;

            CurrentUser = user;
            IsGuest = quest;

            if (currentRole == "Менеджер")
            {
                btnCreate.Visible = false;
                btnUpdate.Visible = false;
                panel1.Visible = false;
                dgvOrders.Dock = DockStyle.Fill;
            }

            dgvOrders.Columns["colDate"].HeaderText = "Дата";
            dgvOrders.Columns["colUserDelivery"].HeaderText = "Информация";
            dgvOrders.Columns["colCode"].HeaderText = "Код";
            dgvOrders.Columns["colStatus"].HeaderText = "Статус";

            LoadOrders();
        }

        private void LoadOrders()
        {
            try
            {
                using (var db = new SportingGoodsStoreContext())
                {
                    var orders = db.Orders
                        .Include(i => i.AddressesOfPickUpPoint)
                        .Include(i => i.User)
                        .Include(i => i.Status)
                        .OrderBy(p => p.Id)
                        //.Where(i => i.IdUser == CurrentUser.Id) // Фильтр для текущего пользователя
                        .ToList();

                    dgvOrders.SuspendLayout();
                    dgvOrders.Rows.Clear();

                    foreach (var order in orders)
                    {
                        int rowIndex = dgvOrders.Rows.Add();
                        var row = dgvOrders.Rows[rowIndex];

                        row.Cells["colDate"].Value = FormatOrderDate(order);

                        row.Cells["colUserDelivery"].Value = FormatUserDelivery(order);

                        row.Cells["colCode"].Value = order.Code.ToString();
                        row.Cells["colCode"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        row.Cells["colStatus"].Value = $"{order.Status.Status1}";
                        row.Cells["colStatus"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        row.Tag = order;

                        ApplyRowStyles(row, order);
                    }

                    dgvOrders.ResumeLayout();
                    dgvOrders.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyRowStyles(DataGridViewRow row, Order order)
        {
            var statusCell = row.Cells["colStatus"];

            if (order.Status.Status1 == "Новый")
            {
                statusCell.Style.ForeColor = Color.Green;
                statusCell.Style.Font = new Font(dgvOrders.Font, FontStyle.Bold);
            }
            else if (order.Status.Status1 == "Доставляется")
            {
                statusCell.Style.ForeColor = ColorTranslator.FromHtml("#4361EE");
                statusCell.Style.Font = new Font(dgvOrders.Font, FontStyle.Regular);
            }
            else if (order.Status.Status1 == "Завершен")
            {
                statusCell.Style.ForeColor = Color.Red;
                statusCell.Style.Font = new Font(dgvOrders.Font, FontStyle.Regular);
            }

        }

        private static string FormatUserDelivery(Order order)
        {

            string deliveryAddress = order.AddressesOfPickUpPoint.Address;
            return $"Пользователь: {order.User.FullName ?? "Не указан"}" + Environment.NewLine +
                $"Адрес доставки: {deliveryAddress}" + Environment.NewLine;
        }

        private static string FormatOrderDate(Order order)
        {
            try
            {
                // Для DateOnly используем форматирование через $"{value:dd.MM.yyyy}"
                string orderDate = $"{order.OrderDate:dd.MM.yyyy}";
                string deliveryDate = $"{order.DeliveryDate:dd.MM.yyyy}";

                return $"Дата заказа: {orderDate}" + Environment.NewLine +
                       $"Дата доставки: {deliveryDate}";
            }
            catch
            {
                // Если не сработало, просто преобразуем в строку
                return $"Дата заказа: {order.OrderDate}" + Environment.NewLine +
                       $"Дата доставки: {order.DeliveryDate}";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Проверка выбранного заказа
            if (selectedOrder == null)
            {
                MessageBox.Show("Пожалуйста, выберите заказ для удаления.",
                    "Заказ не выбран",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Проверка прав
            if (IsGuest)
            {
                MessageBox.Show("Только авторизованные пользователи могут удалять заказы.",
                    "Доступ запрещен",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Подтверждение
            var result = MessageBox.Show(
                $"Вы уверены, что хотите удалить заказ №{selectedOrder.Id}?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                using (var db = new SportingGoodsStoreContext())
                {
                    var order = db.Orders.Find(selectedOrder.Id);

                    if (order == null)
                    {
                        MessageBox.Show("Заказ не найден в базе данных.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    db.Orders.Remove(order);
                    db.SaveChanges();
                }

                MessageBox.Show("Заказ успешно удалён.",
                    "Успех",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadOrders(); // обновляем список
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении заказа: {ex.InnerException?.Message ?? ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count > 0)
                selectedOrder = dgvOrders.SelectedRows[0].Tag as Order;
            else
                selectedOrder = null;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Form5 createForm = new Form5();
            createForm.ShowDialog();
            LoadOrders();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Проверяем, выбран ли товар
            if (selectedOrder == null)
            {
                MessageBox.Show("Пожалуйста, выберите заказ для редактирования.\n\n" +
                    "Для выбора товара нажмите на любую ячейку строки.",
                    "Заказ не выбран",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Проверяем права доступа
            if (!IsGuest)
            {
                // Открываем форму редактирования с выбранным товаром
                Form5 editForm = new Form5(selectedOrder);
                editForm.ShowDialog();

                // Обновляем список товаров после редактирования
                LoadOrders();
            }
            else
            {
                MessageBox.Show("Только авторизованные пользователи могут редактировать заказы.",
                    "Доступ запрещен",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }
}
