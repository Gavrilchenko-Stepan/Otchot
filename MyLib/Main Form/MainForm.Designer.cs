using System.Windows.Forms;

namespace Main_Form
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblUserFIO;
        private System.Windows.Forms.ComboBox cmbSortBy;
        private System.Windows.Forms.ComboBox cmbFilterSupplier;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnOrders;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Button btnDeleteProduct;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label lblSortBy;
        private System.Windows.Forms.Label lblFilterSupplier;
        private System.Windows.Forms.Label lblSearch;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblUserFIO = new System.Windows.Forms.Label();
            this.cmbSortBy = new System.Windows.Forms.ComboBox();
            this.cmbFilterSupplier = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnOrders = new System.Windows.Forms.Button();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.btnDeleteProduct = new System.Windows.Forms.Button();
            this.lblSortBy = new System.Windows.Forms.Label();
            this.lblFilterSupplier = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Chartreuse;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 100);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(960, 450);
            this.flowLayoutPanel1.TabIndex = 11;
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.btnExit.Location = new System.Drawing.Point(882, 22);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 30);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "Выйти";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblUserFIO
            // 
            this.lblUserFIO.AutoSize = true;
            this.lblUserFIO.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.lblUserFIO.Location = new System.Drawing.Point(12, 70);
            this.lblUserFIO.Name = "lblUserFIO";
            this.lblUserFIO.Size = new System.Drawing.Size(154, 17);
            this.lblUserFIO.TabIndex = 10;
            this.lblUserFIO.Text = "Пользователь: Гость";
            // 
            // cmbSortBy
            // 
            this.cmbSortBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSortBy.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.cmbSortBy.Items.AddRange(new object[] {
            "Без сортировки",
            "Цена (возрастание)",
            "Цена (убывание)",
            "Количество (возрастание)",
            "Количество (убывание)"});
            this.cmbSortBy.Location = new System.Drawing.Point(87, 20);
            this.cmbSortBy.Name = "cmbSortBy";
            this.cmbSortBy.Size = new System.Drawing.Size(130, 23);
            this.cmbSortBy.TabIndex = 2;
            this.cmbSortBy.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
            // 
            // cmbFilterSupplier
            // 
            this.cmbFilterSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterSupplier.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.cmbFilterSupplier.Location = new System.Drawing.Point(237, 20);
            this.cmbFilterSupplier.Name = "cmbFilterSupplier";
            this.cmbFilterSupplier.Size = new System.Drawing.Size(130, 23);
            this.cmbFilterSupplier.TabIndex = 4;
            this.cmbFilterSupplier.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(381, 22);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(150, 23);
            this.txtSearch.TabIndex = 6;
            this.txtSearch.Text = "Поиск...";
            this.txtSearch.TextChanged += new System.EventHandler(this.FilterChanged);
            // 
            // btnOrders
            // 
            this.btnOrders.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.btnOrders.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.btnOrders.Location = new System.Drawing.Point(548, 20);
            this.btnOrders.Name = "btnOrders";
            this.btnOrders.Size = new System.Drawing.Size(90, 30);
            this.btnOrders.TabIndex = 7;
            this.btnOrders.Text = "Заказы";
            this.btnOrders.UseVisualStyleBackColor = false;
            this.btnOrders.Click += new System.EventHandler(this.BtnOrders_Click);
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.btnAddProduct.Location = new System.Drawing.Point(644, 20);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(110, 30);
            this.btnAddProduct.TabIndex = 8;
            this.btnAddProduct.Text = "Добавить товар";
            this.btnAddProduct.Click += new System.EventHandler(this.BtnAddProduct_Click);
            // 
            // btnDeleteProduct
            // 
            this.btnDeleteProduct.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.btnDeleteProduct.Location = new System.Drawing.Point(760, 22);
            this.btnDeleteProduct.Name = "btnDeleteProduct";
            this.btnDeleteProduct.Size = new System.Drawing.Size(110, 30);
            this.btnDeleteProduct.TabIndex = 12;
            this.btnDeleteProduct.Text = "Удалить товар";
            this.btnDeleteProduct.Click += new System.EventHandler(this.btnDeleteProduct_Click);
            // 
            // lblSortBy
            // 
            this.lblSortBy.AutoSize = true;
            this.lblSortBy.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lblSortBy.Location = new System.Drawing.Point(84, 5);
            this.lblSortBy.Name = "lblSortBy";
            this.lblSortBy.Size = new System.Drawing.Size(72, 15);
            this.lblSortBy.TabIndex = 15;
            this.lblSortBy.Text = "Сортировка:";
            // 
            // lblFilterSupplier
            // 
            this.lblFilterSupplier.AutoSize = true;
            this.lblFilterSupplier.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lblFilterSupplier.Location = new System.Drawing.Point(234, 5);
            this.lblFilterSupplier.Name = "lblFilterSupplier";
            this.lblFilterSupplier.Size = new System.Drawing.Size(67, 15);
            this.lblFilterSupplier.TabIndex = 14;
            this.lblFilterSupplier.Text = "Поставщик:";
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lblSearch.Location = new System.Drawing.Point(378, 4);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(42, 15);
            this.lblSearch.TabIndex = 13;
            this.lblSearch.Text = "Поиск:";
            // 
            // pbLogo
            // 
            this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
            this.pbLogo.Location = new System.Drawing.Point(12, 12);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(58, 50);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.btnDeleteProduct);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.lblUserFIO);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAddProduct);
            this.Controls.Add(this.btnOrders);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.cmbFilterSupplier);
            this.Controls.Add(this.lblFilterSupplier);
            this.Controls.Add(this.cmbSortBy);
            this.Controls.Add(this.lblSortBy);
            this.Controls.Add(this.pbLogo);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Каталог товаров";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

