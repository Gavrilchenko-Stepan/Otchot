using MyLib;
using MyLib.Models;
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
using System.Xml.Linq;

namespace Main_Form
{
    public partial class ProductEditForm : Form
    {
        private Product _product;
        private ProductRepository _productRepo;
        private bool isNew;
        private string _currentPhotoPath;

        public ProductEditForm(Product product)
        {
            InitializeComponent();
            _productRepo = new ProductRepository(new Database());

            isNew = (product == null);

            if (isNew)
                _product = new Product();
            else
                _product = product;

            if (!isNew)
                LoadProductData();

            if (isNew)
                this.Text = "Добавление товара";
            else
                this.Text = "Редактирование товара";
        }

        private void LoadProductData()
        {
            txtArticle.Text = _product.Article;
            txtName.Text = _product.Name;
            txtUnit.Text = _product.Unit;
            nudPrice.Value = _product.Price;
            txtSupplier.Text = _product.Supplier;
            txtManufacturer.Text = _product.Manufacturer;
            txtCategory.Text = _product.Category;
            nudDiscount.Value = _product.CurrentDiscount;
            nudStock.Value = _product.StockQuantity;
            txtDescription.Text = _product.Description;

            if (!string.IsNullOrEmpty(_product.Photo) && File.Exists(_product.Photo))
            {
                try
                {
                    pbFoto.Image = Image.FromFile(_product.Photo);
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
            string defaultPath = Path.Combine(Application.StartupPath, "picture.png");
            if (File.Exists(defaultPath))
            {
                try
                {
                    pbFoto.Image = Image.FromFile(defaultPath);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtArticle.Text) ||
                string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Артикул и наименование обязательны.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _product.Article = txtArticle.Text.Trim();
            _product.Name = txtName.Text.Trim();
            _product.Unit = txtUnit.Text.Trim();
            _product.Price = nudPrice.Value;
            _product.Supplier = txtSupplier.Text.Trim();
            _product.Manufacturer = txtManufacturer.Text.Trim();
            _product.Category = txtCategory.Text.Trim();
            _product.CurrentDiscount = (int)nudDiscount.Value;
            _product.StockQuantity = (int)nudStock.Value;
            _product.Description = txtDescription.Text.Trim();

            if (!string.IsNullOrEmpty(_currentPhotoPath))
            {
                string fileName = Path.GetFileName(_currentPhotoPath);
                string photoDir = Path.Combine(Application.StartupPath, "ProductPhotos");

                if (!Directory.Exists(photoDir))
                    Directory.CreateDirectory(photoDir);

                string destPath = Path.Combine(photoDir, fileName);
                File.Copy(_currentPhotoPath, destPath, true);
                _product.Photo = destPath;
            }
            else if (isNew && pbFoto.Image == null)
            {
                _product.Photo = null;
            }

            if (isNew)
            {
                _productRepo.AddProduct(_product);
            }
            else
            {
                _productRepo.UpdateProduct(_product);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnLoadPhoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _currentPhotoPath = ofd.FileName;
                    try
                    {
                        if (pbFoto.Image != null)
                            pbFoto.Image.Dispose();
                        pbFoto.Image = Image.FromFile(_currentPhotoPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка загрузки изображения: " + ex.Message);
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
