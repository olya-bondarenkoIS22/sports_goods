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
    public partial class Form2 : Form
    {
        public Models.User CurrentUser { get; private set; }
        public bool IsGuest { get; private set; }
        public Form2(Models.User user, bool quest)
        {
            InitializeComponent();

            var colInfoProduct = new DataGridViewTextBoxColumn();
            colInfoProduct.Name = "colInfoProduct";
            colInfoProduct.FillWeight = 35;
            colInfoProduct.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            var colInfoSupAndMan = new DataGridViewTextBoxColumn();
            colInfoSupAndMan.Name = "colInfoSupAndMan";
            colInfoSupAndMan.FillWeight = 35;
            colInfoSupAndMan.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            var colOldPrice = new DataGridViewTextBoxColumn();
            colOldPrice.Name = "colOldPrice";
            colOldPrice.FillWeight = 10;
            colOldPrice.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            colOldPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            var colNewPrice = new DataGridViewTextBoxColumn();
            colNewPrice.Name = "colNewPrice";
            colNewPrice.FillWeight = 10;
            colNewPrice.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            colNewPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            var colInfoDiscount = new DataGridViewTextBoxColumn();
            colInfoDiscount.Name = "colInfoDiscount";
            colInfoDiscount.FillWeight = 10;
            colInfoDiscount.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            colInfoDiscount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvProducts.Columns.AddRange(
                colInfoProduct,
                colInfoSupAndMan,
                colOldPrice,     
                colNewPrice,     
                colInfoDiscount
            );

            CurrentUser = user;
            IsGuest = quest;

            if (IsGuest)
            {
                bttnOrders.Enabled = false;
            }
            lblUserName.Text = IsGuest ? "Гость" : CurrentUser.FullName;

            dgvProducts.Columns["colInfoProduct"].HeaderText = "Товар";
            dgvProducts.Columns["colInfoSupAndMan"].HeaderText = "Производитель и поставщик";
            dgvProducts.Columns["colOldPrice"].HeaderText = "Старая цена";   
            dgvProducts.Columns["colNewPrice"].HeaderText = "Новая цена";     
            dgvProducts.Columns["colInfoDiscount"].HeaderText = "Скидка";

            LoadProducts();
        }

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
                        .ToList();

                    dgvProducts.SuspendLayout();
                    dgvProducts.Rows.Clear();

                    foreach (var product in products)
                    {
                        int rowIndex = dgvProducts.Rows.Add();
                        var row = dgvProducts.Rows[rowIndex];

                        row.Cells["colInfoProduct"].Value = FormatProductInfo(product);
                        row.Cells["colInfoSupAndMan"].Value = FormatSupAndMan(product);

                        decimal price = product.Price ?? 0;

                        row.Cells["colOldPrice"].Value = $"{price:F2}";

                        if (product.Discount > 0)
                        {
                            decimal discountedPrice = price * (1 - (decimal)product.Discount / 100);
                            row.Cells["colNewPrice"].Value = $"{discountedPrice:F2}";
                        }
                        else
                        {
                            row.Cells["colNewPrice"].Value = $"{price:F2}";
                        }

                        row.Cells["colInfoDiscount"].Value = $"{product.Discount}%";

                        ApplyRowStyles(row, product);
                    }
                    dgvProducts.ResumeLayout();
                    dgvProducts.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

            if (product.Discount > 0)
            {
                var oldPriceCell = row.Cells["colOldPrice"];
                oldPriceCell.Style.Font = new Font(dgvProducts.Font, FontStyle.Strikeout);
                oldPriceCell.Style.ForeColor = Color.Black;

                var newPriceCell = row.Cells["colNewPrice"];
                newPriceCell.Style.Font = new Font(dgvProducts.Font, FontStyle.Bold);
                newPriceCell.Style.ForeColor = Color.Black;
            }
        }

        private string FormatSupAndMan(SportingGood product)
        {
            return $"Производитель: {product.Manufacturer.Manufacturer1}" + Environment.NewLine +
                $"Поставщик: {product.Supplier.Supplier1}";
        }

        private string FormatProductInfo(SportingGood product)
        {
            return $"{product.Article} | {product.Name} " + Environment.NewLine +
                $"Кaтегория: {product.Category.Category1}" + Environment.NewLine +
                $"Описание: {product.Description}";
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
                Form3 ordersForm = new Form3(CurrentUser, IsGuest);
                ordersForm.ShowDialog();
            }
        }
    }
}
