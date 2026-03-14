using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLib.Models;

namespace Main_Form
{
    public partial class ProductCardControl : UserControl
    {
        private Product _product;
        public event EventHandler<Product> CardClicked;

        public ProductCardControl()
        {
            InitializeComponent();
            this.Click += ProductCardControl_Click;

            foreach (Control ctrl in this.Controls)
            {
                ctrl.Click += (s, e) => ProductCardControl_Click(this, e);
            }

            panel2.Click += (s, e) => ProductCardControl_Click(this, e);
            panel1.Click += (s, e) => ProductCardControl_Click(this, e);
            pbFoto.Click += (s, e) => ProductCardControl_Click(this, e);

            foreach (Control ctrl in panel2.Controls)
            {
                ctrl.Click += (s, e) => ProductCardControl_Click(this, e);
            }

            foreach (Control ctrl in panel1.Controls)
            {
                ctrl.Click += (s, e) => ProductCardControl_Click(this, e);
            }
        }

        public void SetProduct(Product product)
        {
            _product = product;

            // Категория товара
            lblCategory.Text = product.Category;

            // Наименование товара
            lblName.Text = product.Name;

            // Скидка (только число с процентом)
            lblDiscount.Text = $"{product.CurrentDiscount}%";

            if (product.DiscountMoreThan15)
            {
                lblDiscount.ForeColor = Color.Yellow;
                lblDiscount.Font = new Font(lblDiscount.Font, FontStyle.Bold);
            }
            else
            {
                lblDiscount.ForeColor = Color.Black;
                lblDiscount.Font = new Font(lblDiscount.Font, FontStyle.Regular);
            }

            // Описание, производитель, поставщик
            lblDescription.Text = $"Описание товара: {product.Description ?? "—"}";
            lblManufacturer.Text = $"Производитель: {product.Manufacturer}";
            lblSupplier.Text = $"Поставщик: {product.Supplier}";

            // Единица измерения и количество
            lblUnit.Text = $"Единица измерения: {product.Unit}";
            lblStock.Text = $"Количество на складе: {product.StockQuantity}";

            // Обновление цены
            UpdatePriceDisplay(product);

            // Подсветка фона
            ApplyBackgroundColor(product);

            // Загрузка фото
            LoadProductImage(product.Photo);
        }

        private void UpdatePriceDisplay(Product product)
        {
            RemoveOldPriceLabels();

            if (product.HasDiscount)
            {
                Label lblOldPrice = new Label
                {
                    Text = $"{product.Price:F2} ₽",
                    Font = new Font("Times New Roman", 10F, FontStyle.Strikeout),
                    ForeColor = Color.Red,
                    AutoSize = true,
                    Location = new Point(lblPrice.Right + 5, lblPrice.Top),
                    Tag = "priceLabel",
                    BackColor = Color.Transparent
                };

                Label lblNewPrice = new Label
                {
                    Text = $"{product.FinalPrice:F2} ₽",
                    Font = new Font("Times New Roman", 12F, FontStyle.Bold),
                    ForeColor = Color.Black,
                    AutoSize = true,
                    Location = new Point(lblOldPrice.Right + 10, lblPrice.Top - 2),
                    Tag = "priceLabel",
                    BackColor = Color.Transparent
                };

                panel2.Controls.Add(lblOldPrice);
                panel2.Controls.Add(lblNewPrice);
            }
            else
            {
                Label lblPriceValue = new Label
                {
                    Text = $"{product.Price:F2} ₽",
                    Font = new Font("Times New Roman", 12F, FontStyle.Bold),
                    ForeColor = Color.Black,
                    AutoSize = true,
                    Location = new Point(lblPrice.Right + 5, lblPrice.Top - 2),
                    Tag = "priceLabel",
                    BackColor = Color.Transparent
                };

                panel2.Controls.Add(lblPriceValue);
            }
        }

        private void RemoveOldPriceLabels()
        {
            var toRemove = new List<Control>();
            foreach (Control ctrl in panel2.Controls)
            {
                if (ctrl.Tag?.ToString() == "priceLabel")
                {
                    toRemove.Add(ctrl);
                }
            }

            foreach (Control ctrl in toRemove)
            {
                panel2.Controls.Remove(ctrl);
                ctrl.Dispose();
            }
        }

        private void ApplyBackgroundColor(Product product)
        {
            Color backColor;

            if (product.OutOfStock)
            {
                backColor = Color.LightBlue;
            }
            else if (product.DiscountMoreThan15)
            {
                backColor = ColorTranslator.FromHtml("#2E8B57");
            }
            else
            {
                backColor = Color.White;
            }

            this.BackColor = backColor;
            panel2.BackColor = backColor;
            panel1.BackColor = backColor;

            foreach (Control ctrl in panel2.Controls)
            {
                if (ctrl.Tag?.ToString() != "priceLabel")
                {
                    ctrl.BackColor = backColor;
                }
            }

            foreach (Control ctrl in panel1.Controls)
            {
                ctrl.BackColor = backColor;
            }
        }

        private void LoadProductImage(string photoPath)
        {
            if (!string.IsNullOrEmpty(photoPath) && File.Exists(photoPath))
            {
                try
                {
                    using (var fs = new FileStream(photoPath, FileMode.Open, FileAccess.Read))
                    {
                        pbFoto.Image = Image.FromStream(fs);
                    }
                }
                catch
                {
                    SetDefaultImage();
                }
            }
            else
            {
                SetDefaultImage();
            }
        }

        private void SetDefaultImage()
        {
            string defaultImagePath = Path.Combine(Application.StartupPath, "picture.png");
            if (File.Exists(defaultImagePath))
            {
                try
                {
                    using (var fs = new FileStream(defaultImagePath, FileMode.Open, FileAccess.Read))
                    {
                        pbFoto.Image = Image.FromStream(fs);
                    }
                }
                catch
                {
                    pbFoto.Image = null;
                }
            }
            else
            {
                pbFoto.Image = null;
            }
        }

        private void ProductCardControl_Click(object sender, EventArgs e)
        {
            CardClicked?.Invoke(this, _product);
        }
    }
}
