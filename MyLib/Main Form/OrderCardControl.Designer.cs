namespace Main_Form
{
    partial class OrderCardControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblOrderArticle;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblPickupAddress;
        private System.Windows.Forms.Label lblOrderDate;
        private System.Windows.Forms.Label lblDeliveryDate;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblOrderArticle = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblPickupAddress = new System.Windows.Forms.Label();
            this.lblOrderDate = new System.Windows.Forms.Label();
            this.lblDeliveryDate = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblOrderArticle
            // 
            this.lblOrderArticle.AutoSize = true;
            this.lblOrderArticle.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.lblOrderArticle.Location = new System.Drawing.Point(16, 2);
            this.lblOrderArticle.Name = "lblOrderArticle";
            this.lblOrderArticle.Size = new System.Drawing.Size(113, 17);
            this.lblOrderArticle.TabIndex = 0;
            this.lblOrderArticle.Text = "Артикул заказа:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lblStatus.Location = new System.Drawing.Point(16, 27);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(79, 15);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Статус заказа:";
            // 
            // lblPickupAddress
            // 
            this.lblPickupAddress.AutoSize = true;
            this.lblPickupAddress.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lblPickupAddress.Location = new System.Drawing.Point(16, 52);
            this.lblPickupAddress.Name = "lblPickupAddress";
            this.lblPickupAddress.Size = new System.Drawing.Size(120, 15);
            this.lblPickupAddress.TabIndex = 2;
            this.lblPickupAddress.Text = "Адрес пункта выдачи:";
            // 
            // lblOrderDate
            // 
            this.lblOrderDate.AutoSize = true;
            this.lblOrderDate.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lblOrderDate.Location = new System.Drawing.Point(16, 77);
            this.lblOrderDate.Name = "lblOrderDate";
            this.lblOrderDate.Size = new System.Drawing.Size(67, 15);
            this.lblOrderDate.TabIndex = 3;
            this.lblOrderDate.Text = "Дата заказа:";
            // 
            // lblDeliveryDate
            // 
            this.lblDeliveryDate.AutoSize = true;
            this.lblDeliveryDate.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lblDeliveryDate.Location = new System.Drawing.Point(10, 37);
            this.lblDeliveryDate.Name = "lblDeliveryDate";
            this.lblDeliveryDate.Size = new System.Drawing.Size(81, 15);
            this.lblDeliveryDate.TabIndex = 4;
            this.lblDeliveryDate.Text = "Дата доставки:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblDeliveryDate);
            this.panel1.Location = new System.Drawing.Point(441, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(152, 100);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblPickupAddress);
            this.panel2.Controls.Add(this.lblOrderDate);
            this.panel2.Controls.Add(this.lblOrderArticle);
            this.panel2.Controls.Add(this.lblStatus);
            this.panel2.Location = new System.Drawing.Point(13, 10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(409, 100);
            this.panel2.TabIndex = 6;
            // 
            // OrderCardControl
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "OrderCardControl";
            this.Size = new System.Drawing.Size(607, 116);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}
