using MyLib;
using MyLib.Models;
using Org.BouncyCastle.Asn1.Ocsp;
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
        private static bool _isOpen = false;

        private Product _product;
        private ProductRepository _productRepo;
        private bool isNew;
        private string _currentPhotoPath;

        public ProductEditForm(Product product)
        {
            if (_isOpen)
            {
                MessageBox.Show("Окно редактирования уже открыто.", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            InitializeComponent();
            _isOpen = true;
            this.FormClosed += (s, e) => _isOpen = false;

            _productRepo = new ProductRepository(new Database());
            isNew = product == null;
            _product = isNew ? new Product() : product;

            if (!isNew) LoadProductData();

            lblId.Visible = !isNew;
            txtId.Visible = !isNew;

            this.Text = isNew ? "Добавление товара" : "Редактирование товара";

            LoadCategoriesAndManufacturers();
        }

        private void LoadCategoriesAndManufacturers()
        {
            var products = _productRepo.GetAllProducts();
            var categories = products.Select(p => p.Category).Where(c => !string.IsNullOrEmpty(c)).Distinct().OrderBy(c => c).ToArray();
            cmbCategory.Items.Clear();
            cmbCategory.Items.AddRange(categories);

            var manufacturers = products.Select(p => p.Manufacturer).Where(m => !string.IsNullOrEmpty(m)).Distinct().OrderBy(m => m).ToArray();
            cmbManufacturer.Items.Clear();
            cmbManufacturer.Items.AddRange(manufacturers);
        }

        private void LoadProductData()
        {
            txtId.Text = _product.Id.ToString();
            txtArticle.Text = _product.Article;
            txtName.Text = _product.Name;
            txtUnit.Text = _product.Unit;
            nudPrice.Value = _product.Price;
            txtSupplier.Text = _product.Supplier;
            if (cmbManufacturer.Items.Contains(_product.Manufacturer))
                cmbManufacturer.SelectedItem = _product.Manufacturer;
            if (cmbCategory.Items.Contains(_product.Category))
                cmbCategory.SelectedItem = _product.Category;
            nudDiscount.Value = _product.CurrentDiscount;
            nudStock.Value = _product.StockQuantity;
            txtDescription.Text = _product.Description;

            // Загрузка фото
            string photoPath = _product.Photo;
            if (!string.IsNullOrEmpty(photoPath))
            {
                string fullPath = Path.Combine(Application.StartupPath, "ProductPhotos", photoPath);
                if (File.Exists(fullPath))
                {
                    try
                    {
                        pbFoto.Image = Image.FromFile(fullPath);
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
            else
            {
                SetDefaultImage();
            }
        }

        private void SetDefaultImage()
        {
            string defaultPath = Path.Combine(Application.StartupPath, "ProductPhotos", "picture.png");
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

        private Image ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                using (var wrapMode = new System.Drawing.Imaging.ImageAttributes())
                {
                    wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtArticle.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Артикул и наименование обязательны.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _product.Article = txtArticle.Text.Trim();
            _product.Name = txtName.Text.Trim();
            _product.Unit = txtUnit.Text.Trim();
            _product.Price = nudPrice.Value;
            _product.Supplier = txtSupplier.Text.Trim();
            _product.Manufacturer = cmbManufacturer.SelectedItem?.ToString() ?? "";
            _product.Category = cmbCategory.SelectedItem?.ToString() ?? "";
            _product.CurrentDiscount = (int)nudDiscount.Value;
            _product.StockQuantity = (int)nudStock.Value;
            _product.Description = txtDescription.Text.Trim();

            // Обработка фото
            if (!string.IsNullOrEmpty(_currentPhotoPath))
            {
                // Удаляем старое фото, если оно существует и не является заглушкой
                if (!isNew && !string.IsNullOrEmpty(_product.Photo))
                {
                    string oldFullPath = Path.Combine(Application.StartupPath, "ProductPhotos", _product.Photo);
                    if (File.Exists(oldFullPath) && oldFullPath != _currentPhotoPath)
                    {
                        File.Delete(oldFullPath);
                    }
                }

                string fileName = Path.GetFileName(_currentPhotoPath);
                string photoDir = Path.Combine(Application.StartupPath, "ProductPhotos");
                if (!Directory.Exists(photoDir))
                    Directory.CreateDirectory(photoDir);
                string destPath = Path.Combine(photoDir, fileName);

                using (var originalImage = Image.FromFile(_currentPhotoPath))
                using (var resizedImage = ResizeImage(originalImage, 300, 200))
                {
                    resizedImage.Save(destPath);
                }

                // Сохраняем только имя файла
                _product.Photo = fileName;
            }
            else if (isNew && pbFoto.Image == null)
            {
                _product.Photo = null;
            }

            try
            {
                if (isNew)
                    _productRepo.AddProduct(_product);
                else
                    _productRepo.UpdateProduct(_product);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                        using (var originalImage = Image.FromFile(_currentPhotoPath))
                        {
                            pbFoto.Image = ResizeImage(originalImage, 300, 200);
                        }
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
