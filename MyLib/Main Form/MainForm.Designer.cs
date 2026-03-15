using System.Windows.Forms;

namespace Main_Form
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblUserFIO;

        // Новые элементы
        private System.Windows.Forms.ComboBox cmbSortBy;
        private System.Windows.Forms.ComboBox cmbFilterSupplier;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnOrders;
        private System.Windows.Forms.Button btnAddProduct;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblUserFIO = new System.Windows.Forms.Label();
            this.cmbSortBy = new System.Windows.Forms.ComboBox();
            this.cmbFilterSupplier = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnOrders = new System.Windows.Forms.Button();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // flowLayoutPanel1
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 50);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(960, 500);
            this.flowLayoutPanel1.TabIndex = 0;

            // btnExit
            this.btnExit.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.btnExit.Location = new System.Drawing.Point(852, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(120, 30);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Выйти";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);

            // lblUserFIO
            this.lblUserFIO.AutoSize = true;
            this.lblUserFIO.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.lblUserFIO.Location = new System.Drawing.Point(12, 18);
            this.lblUserFIO.Name = "lblUserFIO";
            this.lblUserFIO.Size = new System.Drawing.Size(154, 17);
            this.lblUserFIO.TabIndex = 2;
            this.lblUserFIO.Text = "Пользователь: Гость";

            // cmbSortBy
            this.cmbSortBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSortBy.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.cmbSortBy.FormattingEnabled = true;
            this.cmbSortBy.Items.AddRange(new object[] {
                "Без сортировки",
                "Цена (возрастание)",
                "Цена (убывание)",
                "Количество (возрастание)",
                "Количество (убывание)"});
            this.cmbSortBy.Location = new System.Drawing.Point(12, 12);
            this.cmbSortBy.Name = "cmbSortBy";
            this.cmbSortBy.Size = new System.Drawing.Size(150, 23);
            this.cmbSortBy.TabIndex = 3;
            this.cmbSortBy.SelectedIndex = 0;
            this.cmbSortBy.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);

            // cmbFilterSupplier
            this.cmbFilterSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterSupplier.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.cmbFilterSupplier.FormattingEnabled = true;
            this.cmbFilterSupplier.Location = new System.Drawing.Point(180, 12);
            this.cmbFilterSupplier.Name = "cmbFilterSupplier";
            this.cmbFilterSupplier.Size = new System.Drawing.Size(150, 23);
            this.cmbFilterSupplier.TabIndex = 4;
            this.cmbFilterSupplier.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);

            // txtSearch
            this.txtSearch.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.txtSearch.Location = new System.Drawing.Point(350, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 23);
            this.txtSearch.TabIndex = 5;
            this.txtSearch.Text = "Поиск...";
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.TextChanged += new System.EventHandler(this.FilterChanged);

            // btnOrders
            this.btnOrders.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.btnOrders.Location = new System.Drawing.Point(570, 10);
            this.btnOrders.Name = "btnOrders";
            this.btnOrders.Size = new System.Drawing.Size(100, 30);
            this.btnOrders.TabIndex = 6;
            this.btnOrders.Text = "Заказы";
            this.btnOrders.UseVisualStyleBackColor = true;
            this.btnOrders.Click += new System.EventHandler(this.BtnOrders_Click);

            // btnAddProduct
            this.btnAddProduct.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.btnAddProduct.Location = new System.Drawing.Point(680, 10);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(130, 30);
            this.btnAddProduct.TabIndex = 7;
            this.btnAddProduct.Text = "Добавить товар";
            this.btnAddProduct.UseVisualStyleBackColor = true;
            this.btnAddProduct.Click += new System.EventHandler(this.BtnAddProduct_Click);

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.btnAddProduct);
            this.Controls.Add(this.btnOrders);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.cmbFilterSupplier);
            this.Controls.Add(this.cmbSortBy);
            this.Controls.Add(this.lblUserFIO);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.Name = "MainForm";
            this.Text = "Каталог товаров";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

