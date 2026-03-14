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

        public MainForm(User user, ProductRepository productRepo)
        {
            InitializeComponent();
            _currentUser = user;
            _productRepository = productRepo;

            lblUserFIO.Text = $"Пользователь: {user.FullName} ({user.Role})";
            this.Text = "Каталог товаров";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
            try
            {
                var products = _productRepository.GetAllProducts();
                flowLayoutPanel1.Controls.Clear();

                foreach (var product in products)
                {
                    var card = new ProductCardControl();
                    card.SetProduct(product);

                    card.CardClicked += (s, p) => ShowProductInfo(p);

                    flowLayoutPanel1.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки товаров: {ex.Message}");
            }
        }

        private void ShowProductInfo(Product product)
        {
            string priceInfo = product.HasDiscount
                ? $"{product.Price:F2} ₽ → {product.FinalPrice:F2} ₽ (скидка {product.CurrentDiscount}%)"
                : $"{product.Price:F2} ₽";

            MessageBox.Show(
                $"Товар: {product.Name}\n" +
                $"Артикул: {product.Article}\n" +
                $"Категория: {product.Category}\n" +
                $"Цена: {priceInfo}\n" +
                $"На складе: {product.StockQuantity} шт.\n" +
                $"Производитель: {product.Manufacturer}\n" +
                $"Поставщик: {product.Supplier}\n" +
                $"Описание: {product.Description ?? "—"}",
                "Информация о товаре",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
