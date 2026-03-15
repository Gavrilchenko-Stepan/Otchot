using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLib.Models;
using MyLib;

namespace Main_Form
{
    public partial class MainForm : Form
    {
        private readonly User _currentUser;
        private readonly ProductRepository _productRepository;
        private List<Product> _allProducts;

        public MainForm(User user, ProductRepository productRepo)
        {
            InitializeComponent();
            _currentUser = user;
            _productRepository = productRepo;

            lblUserFIO.Text = "Пользователь: " + user.FullName + " (" + user.Role + ")";
            this.Text = "Каталог товаров";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadSuppliers();

            bool canUseFilters = _currentUser.IsManager || _currentUser.IsAdmin;

            cmbSortBy.Visible = canUseFilters;
            cmbFilterSupplier.Visible = canUseFilters;
            txtSearch.Visible = canUseFilters;
            btnOrders.Visible = _currentUser.IsManager || _currentUser.IsAdmin;
            btnAddProduct.Visible = _currentUser.IsAdmin;
        }

        private void LoadProducts()
        {
            try
            {
                _allProducts = _productRepository.GetAllProducts();
                ApplyFiltersAndSort();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки товаров: " + ex.Message);
            }
        }

        private void LoadSuppliers()
        {
            var suppliers = new List<string>();
            foreach (var product in _allProducts)
            {
                if (!string.IsNullOrEmpty(product.Supplier) && !suppliers.Contains(product.Supplier))
                {
                    suppliers.Add(product.Supplier);
                }
            }
            suppliers.Sort();

            cmbFilterSupplier.Items.Clear();
            cmbFilterSupplier.Items.Add("Все поставщики");
            foreach (string supplier in suppliers)
            {
                cmbFilterSupplier.Items.Add(supplier);
            }
            cmbFilterSupplier.SelectedIndex = 0;
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            ApplyFiltersAndSort();
        }

        private void ApplyFiltersAndSort()
        {
            if (_allProducts == null) return;

            IEnumerable<Product> filtered = _allProducts;

            string search = txtSearch.Text.Trim().ToLower();
            if (search != "поиск..." && !string.IsNullOrEmpty(search))
            {
                filtered = filtered.Where(p =>
                    (p.Name != null && p.Name.ToLower().Contains(search)) ||
                    (p.Description != null && p.Description.ToLower().Contains(search)) ||
                    (p.Category != null && p.Category.ToLower().Contains(search)) ||
                    (p.Manufacturer != null && p.Manufacturer.ToLower().Contains(search)) ||
                    (p.Supplier != null && p.Supplier.ToLower().Contains(search)));
            }

            if (cmbFilterSupplier.SelectedItem != null && cmbFilterSupplier.SelectedItem.ToString() != "Все поставщики")
            {
                string supplier = cmbFilterSupplier.SelectedItem.ToString();
                filtered = filtered.Where(p => p.Supplier == supplier);
            }

            string sort = cmbSortBy.SelectedItem.ToString();
            if (sort == "Цена (возрастание)")
                filtered = filtered.OrderBy(p => p.Price);
            else if (sort == "Цена (убывание)")
                filtered = filtered.OrderByDescending(p => p.Price);
            else if (sort == "Количество (возрастание)")
                filtered = filtered.OrderBy(p => p.StockQuantity);
            else if (sort == "Количество (убывание)")
                filtered = filtered.OrderByDescending(p => p.StockQuantity);
            else
                filtered = filtered.OrderBy(p => p.Name);

            DisplayProducts(filtered.ToList());
        }

        private void DisplayProducts(List<Product> products)
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (var product in products)
            {
                var card = new ProductCardControl();
                card.SetProduct(product);

                Product currentProduct = product;
                card.CardClicked += (s, p) =>
                {
                    if (_currentUser.IsAdmin)
                        OpenEditProduct(currentProduct);
                    else
                        ShowProductInfo(currentProduct);
                };

                flowLayoutPanel1.Controls.Add(card);
            }
        }

        private void ShowProductInfo(Product product)
        {
            string priceInfo;
            if (product.HasDiscount)
                priceInfo = product.Price.ToString("F2") + " ₽ → " + product.FinalPrice.ToString("F2") + " ₽ (скидка " + product.CurrentDiscount + "%)";
            else
                priceInfo = product.Price.ToString("F2") + " ₽";

            string description = product.Description;
            if (string.IsNullOrEmpty(description))
                description = "—";

            MessageBox.Show(
                "Товар: " + product.Name + "\n" +
                "Артикул: " + product.Article + "\n" +
                "Категория: " + product.Category + "\n" +
                "Цена: " + priceInfo + "\n" +
                "На складе: " + product.StockQuantity + " шт.\n" +
                "Производитель: " + product.Manufacturer + "\n" +
                "Поставщик: " + product.Supplier + "\n" +
                "Описание: " + description,
                "Информация о товаре",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void OpenEditProduct(Product product)
        {
            using (var form = new ProductEditForm(product))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadProducts();
                }
            }
        }

        private void BtnOrders_Click(object sender, EventArgs e)
        {
            using (var form = new OrdersForm(_currentUser))
            {
                form.ShowDialog();
            }
        }

        private void BtnAddProduct_Click(object sender, EventArgs e)
        {
            using (var form = new ProductEditForm(null))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadProducts();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
