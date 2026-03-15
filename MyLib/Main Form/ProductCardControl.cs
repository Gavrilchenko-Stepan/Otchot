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

            lblCategory.Text = product.Category;
            lblName.Text = product.Name;
            lblDiscount.Text = product.CurrentDiscount + "%";

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

            string description = product.Description;
            if (string.IsNullOrEmpty(description))
                description = "—";

            lblDescription.Text = "Описание товара: " + description;
            lblManufacturer.Text = "Производитель: " + product.Manufacturer;
            lblSupplier.Text = "Поставщик: " + product.Supplier;
            lblUnit.Text = "Единица измерения: " + product.Unit;
            lblStock.Text = "Количество на складе: " + product.StockQuantity;

            UpdatePriceDisplay(product);
            ApplyBackgroundColor(product);
            LoadProductImage(product.Photo);
        }

        private void UpdatePriceDisplay(Product product)
        {
            RemoveOldPriceLabels();

            if (product.HasDiscount)
            {
                Label lblOldPrice = new Label();
                lblOldPrice.Text = product.Price.ToString("F2") + " ₽";
                lblOldPrice.Font = new Font("Times New Roman", 10F, FontStyle.Strikeout);
                lblOldPrice.ForeColor = Color.Red;
                lblOldPrice.AutoSize = true;
                lblOldPrice.Location = new Point(lblPrice.Right + 5, lblPrice.Top);
                lblOldPrice.Tag = "priceLabel";
                lblOldPrice.BackColor = Color.Transparent;

                Label lblNewPrice = new Label();
                lblNewPrice.Text = product.FinalPrice.ToString("F2") + " ₽";
                lblNewPrice.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
                lblNewPrice.ForeColor = Color.Black;
                lblNewPrice.AutoSize = true;
                lblNewPrice.Location = new Point(lblOldPrice.Right + 10, lblPrice.Top - 2);
                lblNewPrice.Tag = "priceLabel";
                lblNewPrice.BackColor = Color.Transparent;

                panel2.Controls.Add(lblOldPrice);
                panel2.Controls.Add(lblNewPrice);
            }
            else
            {
                Label lblPriceValue = new Label();
                lblPriceValue.Text = product.Price.ToString("F2") + " ₽";
                lblPriceValue.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
                lblPriceValue.ForeColor = Color.Black;
                lblPriceValue.AutoSize = true;
                lblPriceValue.Location = new Point(lblPrice.Right + 5, lblPrice.Top - 2);
                lblPriceValue.Tag = "priceLabel";
                lblPriceValue.BackColor = Color.Transparent;

                panel2.Controls.Add(lblPriceValue);
            }
        }

        private void RemoveOldPriceLabels()
        {
            var toRemove = new System.Collections.Generic.List<Control>();
            foreach (Control ctrl in panel2.Controls)
            {
                if (ctrl.Tag != null && ctrl.Tag.ToString() == "priceLabel")
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
                backColor = Color.FromArgb(46, 139, 87); // #2E8B57
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
                if (ctrl.Tag == null || ctrl.Tag.ToString() != "priceLabel")
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
            if (CardClicked != null)
                CardClicked(this, _product);
        }
    }
}
