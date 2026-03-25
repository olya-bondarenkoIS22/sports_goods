using Microsoft.EntityFrameworkCore;
using sport.Models;
using sport.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Resources;
using System.Text;
using System.Windows.Forms;

namespace sport
{
    public partial class Form2 : Form
    {
        public Models.User CurrentUser { get; private set; }
        public bool IsGuest { get; private set; }

        private string currentUserRole;
        private SportingGood selectedGood;

        public Form2(Models.User user, bool quest)
        {
            InitializeComponent();

            dgvProducts.CellPainting += DgvProducts_CellPainting;
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.RowHeadersVisible = false;

            var colPhoto = new DataGridViewImageColumn();
            colPhoto.Name = "colPhoto";
            colPhoto.ImageLayout = DataGridViewImageCellLayout.Zoom;
            colPhoto.Width = 200;
            colPhoto.FillWeight = 25;

            var colInfoProduct = new DataGridViewTextBoxColumn();
            colInfoProduct.Name = "colInfoProduct";
            colInfoProduct.FillWeight = 55;
            colInfoProduct.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            colInfoProduct.DefaultCellStyle.Font = new Font("Times New Roman", 12, FontStyle.Regular);
            colInfoProduct.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5); // Отступы: слева, сверху, справа, снизу
            colInfoProduct.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;

            var colInfoDiscount = new DataGridViewTextBoxColumn();
            colInfoDiscount.Name = "colInfoDiscount";
            colInfoDiscount.FillWeight = 20;
            colInfoDiscount.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            colInfoDiscount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colInfoDiscount.DefaultCellStyle.Font = new Font("Times New Roman", 12, FontStyle.Regular);
            colInfoDiscount.DefaultCellStyle.Padding = new Padding(5);

            dgvProducts.Columns.AddRange(
                colPhoto,
                colInfoProduct,
                colInfoDiscount
            );

            dgvProducts.EnableHeadersVisualStyles = false;
            dgvProducts.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            dgvProducts.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
            dgvProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            dgvProducts.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProducts.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);

            dgvProducts.ColumnHeadersDefaultCellStyle.SelectionBackColor = SystemColors.Control;
            dgvProducts.ColumnHeadersDefaultCellStyle.SelectionForeColor = SystemColors.ControlText;

            CurrentUser = user;
            IsGuest = quest;

            LoadRole();

            if (IsGuest)
            {
                bttnOrders.Visible = false;
                btnCreate.Visible = false;
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
            }
            if (currentUserRole == "Менеджер")
            {
                btnCreate.Visible = false;
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
            }
            lblUserName.Text = IsGuest ? "Гость" : CurrentUser.FullName;

            dgvProducts.Columns["colPhoto"].HeaderText = "Изображение";
            dgvProducts.Columns["colInfoProduct"].HeaderText = "Товар";
            dgvProducts.Columns["colInfoDiscount"].HeaderText = "Скидка";
            ConfigureDgvProducts();
            LoadProducts();
        }

        private void LoadRole()
        {
            if (!IsGuest)
            {
                currentUserRole = CurrentUser.IdRoleNavigation.Role1;
            }
        }
        private void ConfigureDgvProducts()
        {
            // Настройка выделения строк
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;

            // Добавляем обработчики для выделения строки при клике
            //dgvProducts.CellClick += DgvProducts_CellClick;
            dgvProducts.SelectionChanged += DgvProducts_SelectionChanged;
        }
        //private void DgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0)
        //    {
        //        // Снимаем выделение со всех строк
        //        dgvProducts.ClearSelection();
        //        // Выделяем строку, по которой кликнули
        //        dgvProducts.Rows[e.RowIndex].Selected = true;
        //        // Устанавливаем текущую ячейку
        //        dgvProducts.CurrentCell = dgvProducts.Rows[e.RowIndex].Cells[e.ColumnIndex];

        //        //wselectedGood = (SportingGood)dgvProducts.SelectedRows[0].DataBoundItem;`
        //    }
        //}
        private void DgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            //selectedGood = (SportingGood)dgvProducts.SelectedRows[0].DataBoundItem;
            // Когда выделение меняется, обновляем selectedGood
            if (dgvProducts.SelectedRows.Count > 0)
            {
                selectedGood = dgvProducts.SelectedRows[0].Tag as SportingGood;
            }
            else
            {
                selectedGood = null;
            }
        }
        //private void DgvProducts_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        //{
        //    // Если строка выделена, обновляем selectedGood
        //    if (e.StateChanged == DataGridViewElementStates.Selected)
        //    {
        //        if (e.Row.Selected)
        //        {
        //            selectedGood = (SportingGood)e.Row.DataBoundItem;
        //        }
        //    }
        //}
        //private SportingGood GetSelectedGood()
        //{
        //    return selectedGood;
        //}
        private void LoadProducts()
        {
            try
            {
                using (var db = new SportingGoodsStoreContext())
                {
                    var products = db.SportingGoods
                        .Include(p => p.Category)
                        .Include(p => p.Manufacturer)
                        .Include(p => p.Supplier)
                        .Include(p => p.UnitsOfMeasurement)
                        .OrderBy(p => p.Id)
                        .ToList();

                    dgvProducts.SuspendLayout();
                    dgvProducts.Rows.Clear();

                    foreach (var product in products)
                    {
                        int rowIndex = dgvProducts.Rows.Add();
                        var row = dgvProducts.Rows[rowIndex];

                        // Сохраняем объект товара в Tag строки для удобства
                        row.Tag = product;
                        row.Cells["colPhoto"].Value = LoadProductImage(product.AddPhotoUrlToSportingGoods);
                        row.Cells["colInfoProduct"].Value = FormatProductInfo(product);
                        row.Cells["colInfoDiscount"].Value = $"{product.Discount}%";

                        ApplyRowStyles(row, product);
                    }

                    dgvProducts.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    dgvProducts.ResumeLayout();

                    // Снимаем выделение после загрузки
                    dgvProducts.ClearSelection();
                    selectedGood = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Image LoadProductImage(string photoUrl)
        {
            if (!String.IsNullOrEmpty(photoUrl) && System.IO.File.Exists(photoUrl))
            {
                return Image.FromFile(photoUrl);
            }

            return Resources.picture;
        }

        private void ApplyRowStyles(DataGridViewRow row, SportingGood product)
        {
            if (product.Discount > 15)
            {
                row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#2EC4B6");
                row.DefaultCellStyle.ForeColor = Color.White;
            }

            if (product.QuantityInStock <= 0)
            {
                row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#E9F5FF");
                row.DefaultCellStyle.ForeColor = Color.Black;
            }
        }


        private string FormatProductInfo(SportingGood product)
        {
            decimal oldPrice = product.Price ?? 0;
            decimal discount = product.Discount ?? 0;
            decimal newPrice = oldPrice * (100 - discount) / 100;

            if (discount > 0)
            {
                return $"{product.Article} | {product.Name}" + Environment.NewLine +
                       $"Категория: {product.Category.Category1}" + Environment.NewLine +
                       $"Описание: {product.Description}" + Environment.NewLine +
                       $"Производитель: {product.Manufacturer.Manufacturer1}" + Environment.NewLine +
                       $"Поставщик: {product.Supplier.Supplier1}" + Environment.NewLine +
                       $"Старая цена: {oldPrice:C}" + Environment.NewLine +
                       $"Новая цена: {newPrice:C}";
            }
            else
            {
                return $"{product.Article} | {product.Name}" + Environment.NewLine +
                       $"Категория: {product.Category.Category1}" + Environment.NewLine +
                       $"Описание: {product.Description}" + Environment.NewLine +
                       $"Производитель: {product.Manufacturer.Manufacturer1}" + Environment.NewLine +
                       $"Поставщик: {product.Supplier.Supplier1}" + Environment.NewLine +
                       $"Цена: {oldPrice:C}";
            }
        }
        private void DgvProducts_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == dgvProducts.Columns["colInfoProduct"].Index && e.RowIndex >= 0)
            {
                // Рисуем фон
                e.PaintBackground(e.CellBounds, true);

                var row = dgvProducts.Rows[e.RowIndex];
                string cellText = e.Value?.ToString() ?? "";
                string[] lines = cellText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                using (var font = new Font("Times New Roman", 12, FontStyle.Regular))
                {
                    // Получаем отступы ячейки
                    Padding cellPadding = e.CellStyle.Padding;

                    // Начальная позиция с учетом отступов
                    float currentY = e.CellBounds.Top + cellPadding.Top;
                    int leftMargin = e.CellBounds.Left + cellPadding.Left;

                    // Проверяем есть ли скидка
                    bool hasDiscount = false;
                    if (row.Cells["colInfoDiscount"].Value != null)
                    {
                        string discountStr = row.Cells["colInfoDiscount"].Value.ToString().Replace("%", "");
                        hasDiscount = int.TryParse(discountStr, out int discount) && discount > 0;
                    }

                    for (int i = 0; i < lines.Length; i++)
                    {
                        Color textColor = e.CellStyle.ForeColor;

                        // Если есть скидка и это строка с новой ценой - делаем красной
                        if (hasDiscount && lines[i].Contains("Новая цена:"))
                        {
                            textColor = Color.Red;
                        }

                        // Рисуем текст
                        TextRenderer.DrawText(e.Graphics, lines[i], font,
                            new Rectangle(leftMargin, (int)currentY,
                                e.CellBounds.Width - cellPadding.Left - cellPadding.Right, font.Height + 2),
                            textColor,
                            TextFormatFlags.Top | TextFormatFlags.Left | TextFormatFlags.WordBreak);

                        // Если есть скидка и это строка со старой ценой - рисуем линию зачеркивания
                        if (hasDiscount && lines[i].Contains("Старая цена:"))
                        {
                            SizeF textSize = e.Graphics.MeasureString(lines[i], font);

                            // Рисуем черную линию зачеркивания
                            using (var pen = new Pen(Color.Black, 1))
                            {
                                int lineY = (int)currentY + (font.Height / 2);
                                e.Graphics.DrawLine(pen, leftMargin, lineY, leftMargin + textSize.Width, lineY);
                            }
                        }

                        currentY += font.Height + 2;
                    }
                }

                e.Handled = true;
            }
        }
        private void BttnLogout_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
        }

        private void BttnOrders_Click(object sender, EventArgs e)
        {
            if (!IsGuest)
            {
                Form3 ordersForm = new Form3(CurrentUser, IsGuest, currentUserRole);
                ordersForm.ShowDialog();
            }
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            if (!IsGuest)
            {
                Form4 createForm = new Form4(CurrentUser, IsGuest);
                createForm.ShowDialog();
                LoadProducts();
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Получаем выбранный товар
            //SportingGood _selectedGood = (SportingGood)dgvProducts.SelectedRows[0].DataBoundItem;
            //Int32 selectedRow = .RowIndex;
            Console.WriteLine(dgvProducts.SelectedRows.ToString() + " | " + selectedGood);

            // Проверяем, выбран ли товар
            if (selectedGood == null)
            {
                MessageBox.Show("Пожалуйста, выберите товар для редактирования.\n\n" +
                    "Для выбора товара нажмите на любую ячейку строки.",
                    "Товар не выбран",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Проверяем права доступа
            if (!IsGuest)
            {
                // Открываем форму редактирования с выбранным товаром
                Form4 editForm = new Form4(CurrentUser, IsGuest, selectedGood);
                editForm.ShowDialog();

                // Обновляем список товаров после редактирования
                LoadProducts();
            }
            else
            {
                MessageBox.Show("Только авторизованные пользователи могут редактировать товары.",
                    "Доступ запрещен",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void dgvProducts_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Int32 selectedRow = e.RowIndex;

            //selectedGood = (SportingGood)dgvProducts.Rows[selectedRow].DataBoundItem;
            //Console.WriteLine(selectedRow + " | " + selectedGood);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedGood == null)
            {
                MessageBox.Show("Пожалуйста, выберите товар для удаления.\n\n" +
                    "Для выбора товара нажмите на любую ячейку строки.",
                    "Товар не выбран",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Проверка прав доступа
            if (IsGuest)
            {
                MessageBox.Show("Только авторизованные пользователи могут удалять товары.",
                    "Доступ запрещен",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            // Подтверждение удаления
            var result = MessageBox.Show(
                $"Вы уверены, что хотите удалить товар \"{selectedGood.Name}\"?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                using (var db = new SportingGoodsStoreContext())
                {
                    var good = db.SportingGoods.Find(selectedGood.Id);

                    if (good == null)
                    {
                        MessageBox.Show("Товар не найден в базе данных.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    db.SportingGoods.Remove(good);
                    db.SaveChanges();
                }

                MessageBox.Show("Товар успешно удалён.",
                    "Успех",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadProducts(); // обновляем список
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении товара: {ex.InnerException?.Message ?? ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
