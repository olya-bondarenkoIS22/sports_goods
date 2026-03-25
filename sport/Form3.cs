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
        public Form3(Models.User user, bool quest)
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
    }
}
