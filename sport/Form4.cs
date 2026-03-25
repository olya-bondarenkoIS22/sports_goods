using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using sport.Models;

namespace sport
{
    public partial class Form4 : Form
    {
        public Models.User CurrentUser { get; private set; }
        public bool IsGuest { get; private set; }

        private SportingGood editingGood;
        private bool isEditMode;

        public Form4(Models.User user, bool quest)
        {
            InitializeComponent();
            CurrentUser = user;
            IsGuest = quest;
            isEditMode = false;
        }
        public Form4(Models.User user, bool quest, SportingGood goodToEdit)
        {
            InitializeComponent();
            CurrentUser = user;
            IsGuest = quest;
            editingGood = goodToEdit;
            isEditMode = true;
        }


        private void bttnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new SportingGoodsStoreContext())
                {
                    if (string.IsNullOrWhiteSpace(tbArticle.Text) ||
                        string.IsNullOrWhiteSpace(tbNme.Text) ||
                        string.IsNullOrWhiteSpace(tbPrice.Text) ||
                        string.IsNullOrWhiteSpace(tbQuantityInStock.Text))
                    {
                        MessageBox.Show("Пожалуйста, заполните все обязательные поля (Артикул, Название, Цена, Количество на складе).",
                            "Ошибка валидации",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    if (cbCategory.SelectedItem == null ||
                        cbManufacturer.SelectedItem == null ||
                        cbSupplier.SelectedItem == null ||
                        cbMeasurement.SelectedItem == null)
                    {
                        MessageBox.Show("Пожалуйста, выберите значения для Категории, Производителя, Поставщика и Единиц измерения.",
                            "Ошибка валидации",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    SportingGood good = new SportingGood();




                    // Если режим редактирования - получаем существующий товар из базы
                    if (isEditMode && editingGood != null)
                    {
                        good = db.SportingGoods.Find(editingGood.Id);
                        if (good == null)
                        {
                            MessageBox.Show("Товар не найден в базе данных.",
                                "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        good = new SportingGood();
                    }



                    good.Article = tbArticle.Text;

                    Category selectedCategory = (Category)cbCategory.SelectedItem;
                    good.IdCategory = selectedCategory.Id;

                    good.Name = tbNme.Text;

                    Manufacturer selectedManufacturer = (Manufacturer)cbManufacturer.SelectedItem;
                    good.IdManufacturer = selectedManufacturer.Id;

                    Supplier selectedSupplier = (Supplier)cbSupplier.SelectedItem;
                    good.IdSupplier = selectedSupplier.Id;

                    good.Price = Convert.ToInt32(tbPrice.Text);

                    UnitsOfMeasurement selectedUnit = (UnitsOfMeasurement)cbMeasurement.SelectedItem;
                    good.IdUnitOfMeasurement = selectedUnit.Id;

                    good.Discount = string.IsNullOrWhiteSpace(tbDiscount.Text) ? 0 : Convert.ToInt32(tbDiscount.Text);
                    good.QuantityInStock = Convert.ToInt32(tbQuantityInStock.Text);
                    good.Description = tbDescription.Text;
                    good.AddPhotoUrlToSportingGoods = null;

                    if (!isEditMode)
                    {
                        db.SportingGoods.Add(good); // только для нового товара
                    }

                    db.SaveChanges();

                    MessageBox.Show("Товар успешно добавлен в базу данных!",
                        "Успех",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.Close();
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Неверный формат числа. Пожалуйста, введите корректные числа для Цены, Скидки и Количества на складе.",
                    "Ошибка ввода",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Console.WriteLine(ex + " | " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении товара: {ex.InnerException?.Message ?? ex.Message}",
                    "Ошибка базы данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Console.WriteLine(ex + " | " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner exception: " + ex.InnerException.Message);
                }
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            try
            {
                using (var db = new SportingGoodsStoreContext())
                {
                    List<Category> categories = db.Categories.ToList();
                    List<Manufacturer> manufacturers = db.Manufacturers.ToList();
                    List<Supplier> suppliers = db.Suppliers.ToList();
                    List<UnitsOfMeasurement> unitsOfMeasurements = db.UnitsOfMeasurements.ToList();

                    cbCategory.Items.Clear();
                    cbManufacturer.Items.Clear();
                    cbSupplier.Items.Clear();
                    cbMeasurement.Items.Clear();

                    cbCategory.DataSource = categories;
                    cbManufacturer.DataSource = manufacturers;
                    cbSupplier.DataSource = suppliers;
                    cbMeasurement.DataSource = unitsOfMeasurements;

                    if (isEditMode && editingGood != null)
                    {
                        LoadGoodData(db);

                        // Меняем текст кнопки на "Обновить"
                        bttnCreate.Text = "Обновить";
                        // Меняем заголовок формы
                        this.Text = "Редактирование товара";
                    }
                    else
                    {
                        this.Text = "Добавление товара";
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}",
                    "Ошибка загрузки",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Console.WriteLine(ex + " | " + ex.Message);
            }
        }
        private void LoadGoodData(SportingGoodsStoreContext db)
        {
            tbArticle.Text = editingGood.Article;
            tbNme.Text = editingGood.Name;
            tbPrice.Text = editingGood.Price.ToString();
            tbQuantityInStock.Text = editingGood.QuantityInStock.ToString();
            tbDiscount.Text = editingGood.Discount?.ToString() ?? "";
            tbDescription.Text = editingGood.Description;

            // Для int просто присваиваем (если Id = 0, то не выбран)
            if (editingGood.IdCategory > 0)
            {
                cbCategory.SelectedItem = db.Categories.FirstOrDefault(c => c.Id == editingGood.IdCategory);
            }

            if (editingGood.IdManufacturer > 0)
            {
                cbManufacturer.SelectedItem = db.Manufacturers.FirstOrDefault(m => m.Id == editingGood.IdManufacturer);
            }

            if (editingGood.IdSupplier > 0)
            {
                cbSupplier.SelectedItem = db.Suppliers.FirstOrDefault(s => s.Id == editingGood.IdSupplier);
            }

            if (editingGood.IdUnitOfMeasurement > 0)
            {
                cbMeasurement.SelectedItem = db.UnitsOfMeasurements.FirstOrDefault(u => u.Id == editingGood.IdUnitOfMeasurement);
            }
        }
    }
}
