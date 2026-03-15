using System.Windows.Forms;

namespace Main_Form
{
    partial class ProductCardControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblManufacturer;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.PictureBox pbFoto;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDiscount;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblManufacturer = new System.Windows.Forms.Label();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblName = new System.Windows.Forms.Label();
            this.lblStock = new System.Windows.Forms.Label();
            this.lblUnit = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.pbFoto = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFoto)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();

            // lblCategory
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.lblCategory.Location = new System.Drawing.Point(3, 3);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(122, 16);
            this.lblCategory.TabIndex = 1;
            this.lblCategory.Text = "Категория товара";

            // lblDescription
            this.lblDescription.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lblDescription.Location = new System.Drawing.Point(3, 21);
            this.lblDescription.MaximumSize = new System.Drawing.Size(300, 40);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(300, 24);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Описание товара:";

            // lblManufacturer
            this.lblManufacturer.AutoSize = true;
            this.lblManufacturer.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lblManufacturer.Location = new System.Drawing.Point(3, 43);
            this.lblManufacturer.Name = "lblManufacturer";
            this.lblManufacturer.Size = new System.Drawing.Size(89, 14);
            this.lblManufacturer.TabIndex = 3;
            this.lblManufacturer.Text = "Производитель:";

            // lblSupplier
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lblSupplier.Location = new System.Drawing.Point(3, 58);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(67, 14);
            this.lblSupplier.TabIndex = 4;
            this.lblSupplier.Text = "Поставщик:";

            // panel2
            this.panel2.Controls.Add(this.lblName);
            this.panel2.Controls.Add(this.lblStock);
            this.panel2.Controls.Add(this.lblUnit);
            this.panel2.Controls.Add(this.lblPrice);
            this.panel2.Controls.Add(this.lblCategory);
            this.panel2.Controls.Add(this.lblDescription);
            this.panel2.Controls.Add(this.lblSupplier);
            this.panel2.Controls.Add(this.lblManufacturer);
            this.panel2.Location = new System.Drawing.Point(174, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(307, 119);
            this.panel2.TabIndex = 6;

            // lblName
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.lblName.Location = new System.Drawing.Point(147, 3);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(150, 16);
            this.lblName.TabIndex = 8;
            this.lblName.Text = "Наименование товара";

            // lblStock
            this.lblStock.AutoSize = true;
            this.lblStock.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lblStock.Location = new System.Drawing.Point(3, 101);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(118, 14);
            this.lblStock.TabIndex = 7;
            this.lblStock.Text = "Количество на складе:";

            // lblUnit
            this.lblUnit.AutoSize = true;
            this.lblUnit.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lblUnit.Location = new System.Drawing.Point(3, 86);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(108, 14);
            this.lblUnit.TabIndex = 6;
            this.lblUnit.Text = "Единица измерения:";

            // lblPrice
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lblPrice.Location = new System.Drawing.Point(3, 72);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(35, 14);
            this.lblPrice.TabIndex = 5;
            this.lblPrice.Text = "Цена:";

            // pbFoto
            this.pbFoto.Location = new System.Drawing.Point(12, 4);
            this.pbFoto.Name = "pbFoto";
            this.pbFoto.Size = new System.Drawing.Size(156, 119);
            this.pbFoto.TabIndex = 7;
            this.pbFoto.TabStop = false;

            // panel1
            this.panel1.Controls.Add(this.lblDiscount);
            this.panel1.Location = new System.Drawing.Point(481, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(89, 119);
            this.panel1.TabIndex = 8;

            // lblDiscount
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Font = new System.Drawing.Font("Times New Roman", 15.75F);
            this.lblDiscount.Location = new System.Drawing.Point(7, 50);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(75, 23);
            this.lblDiscount.TabIndex = 0;
            this.lblDiscount.Text = "Скидка";

            // ProductCardControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pbFoto);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.Name = "ProductCardControl";
            this.Size = new System.Drawing.Size(574, 127);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFoto)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
