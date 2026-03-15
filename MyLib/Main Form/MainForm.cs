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
        private Product _selectedProduct;
        private ProductCardControl _selectedCard; // текущая выделенная карточка

        public MainForm(User user, ProductRepository productRepo)
        {
            InitializeComponent();

            _currentUser = user;
            _productRepository = productRepo;

            lblUserFIO.Text = "Пользователь: " + user.FullName + " (" + user.Role + ")";
            this.Text = "Каталог товаров";

            cmbSortBy.SelectedIndex = 0;

            txtSearch.Enter += (s, e) =>
            {
                if (txtSearch.Text == "Поиск...")
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor = Color.Black;
                }
            };
            txtSearch.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text = "Поиск...";
                    txtSearch.ForeColor = Color.Gray;
                }
            };
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadSuppliers();

            bool canUseFilters = _currentUser.IsManager || _currentUser.IsAdmin;
            cmbSortBy.Visible = canUseFilters;
            cmbFilterSupplier.Visible = canUseFilters;
            txtSearch.Visible = canUseFilters;
            lblSortBy.Visible = canUseFilters;
            lblFilterSupplier.Visible = canUseFilters;
            lblSearch.Visible = canUseFilters;

            btnOrders.Visible = _currentUser.IsManager || _currentUser.IsAdmin;
            btnAddProduct.Visible = _currentUser.IsAdmin;
            btnDeleteProduct.Visible = _currentUser.IsAdmin;
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
            var suppliers = _allProducts
                .Select(p => p.Supplier)
                .Where(s => !string.IsNullOrEmpty(s))
                .Distinct()
                .OrderBy(s => s)
                .ToList();

            cmbFilterSupplier.Items.Clear();
            cmbFilterSupplier.Items.Add("Все поставщики");
            cmbFilterSupplier.Items.AddRange(suppliers.ToArray());
            cmbFilterSupplier.SelectedIndex = 0;
        }

        private void FilterChanged(object sender, EventArgs e) => ApplyFiltersAndSort();

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

            string sort = cmbSortBy.SelectedItem?.ToString() ?? "Без сортировки";
            switch (sort)
            {
                case "Цена (возрастание)":
                    filtered = filtered.OrderBy(p => p.Price);
                    break;
                case "Цена (убывание)":
                    filtered = filtered.OrderByDescending(p => p.Price);
                    break;
                case "Количество (возрастание)":
                    filtered = filtered.OrderBy(p => p.StockQuantity);
                    break;
                case "Количество (убывание)":
                    filtered = filtered.OrderByDescending(p => p.StockQuantity);
                    break;
                default:
                    filtered = filtered.OrderBy(p => p.Name);
                    break;
            }

            DisplayProducts(filtered.ToList());
        }

        private void DisplayProducts(List<Product> products)
        {
            flowLayoutPanel1.Controls.Clear();
            _selectedCard = null;

            foreach (var product in products)
            {
                var card = new ProductCardControl();
                card.SetProduct(product);

                Product currentProduct = product;

                // Одиночный клик – выделение
                card.CardClicked += (s, p) =>
                {
                    if (_selectedCard != null && _selectedCard != card)
                        _selectedCard.SetSelected(false);
                    card.SetSelected(true);
                    _selectedCard = card;
                    _selectedProduct = p;
                };

                // Двойной клик – действие (редактирование/просмотр)
                card.CardDoubleClicked += (s, p) =>
                {
                    if (_currentUser.IsAdmin)
                        OpenEditProduct(p);
                    else
                        ShowProductInfo(p);
                };

                flowLayoutPanel1.Controls.Add(card);
            }
        }

        private void ShowProductInfo(Product product)
        {
            string priceInfo = product.HasDiscount
                ? $"{product.Price:F2} ₽ → {product.FinalPrice:F2} ₽ (скидка {product.CurrentDiscount}%)"
                : $"{product.Price:F2} ₽";

            string description = product.Description ?? "—";

            MessageBox.Show(
                $"Товар: {product.Name}\n" +
                $"Артикул: {product.Article}\n" +
                $"Категория: {product.Category}\n" +
                $"Цена: {priceInfo}\n" +
                $"На складе: {product.StockQuantity} шт.\n" +
                $"Производитель: {product.Manufacturer}\n" +
                $"Поставщик: {product.Supplier}\n" +
                $"Описание: {description}",
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
                    LoadProducts();
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
                    LoadProducts();
            }
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (_selectedProduct == null)
            {
                MessageBox.Show("Выберите товар для удаления (одинарный клик на его карточку).", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_productRepository.IsProductInOrders(_selectedProduct.Article))
            {
                MessageBox.Show("Нельзя удалить товар, который присутствует в заказах.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Удалить товар \"{_selectedProduct.Name}\"?",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    _productRepository.DeleteProduct(_selectedProduct.Article);
                    LoadProducts();
                    _selectedProduct = null;
                    _selectedCard = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении: " + ex.Message, "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
