namespace Main_Form
{
    partial class OrderEditForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtOrderNumber;
        private System.Windows.Forms.DateTimePicker dtpOrderDate;
        private System.Windows.Forms.DateTimePicker dtpDeliveryDate;
        private System.Windows.Forms.CheckBox chkDeliveryDate;
        private System.Windows.Forms.ComboBox cmbPickupPoint;
        private System.Windows.Forms.TextBox txtPickupCode;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblOrderNumber;
        private System.Windows.Forms.Label lblOrderDate;
        private System.Windows.Forms.Label lblDeliveryDate;
        private System.Windows.Forms.Label lblPickupPoint;
        private System.Windows.Forms.Label lblPickupCode;
        private System.Windows.Forms.Label lblStatus;

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
            this.txtOrderNumber = new System.Windows.Forms.TextBox();
            this.dtpOrderDate = new System.Windows.Forms.DateTimePicker();
            this.dtpDeliveryDate = new System.Windows.Forms.DateTimePicker();
            this.chkDeliveryDate = new System.Windows.Forms.CheckBox();
            this.cmbPickupPoint = new System.Windows.Forms.ComboBox();
            this.txtPickupCode = new System.Windows.Forms.TextBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblOrderNumber = new System.Windows.Forms.Label();
            this.lblOrderDate = new System.Windows.Forms.Label();
            this.lblDeliveryDate = new System.Windows.Forms.Label();
            this.lblPickupPoint = new System.Windows.Forms.Label();
            this.lblPickupCode = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.txtOrderNumber.Location = new System.Drawing.Point(150, 27);
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.Size = new System.Drawing.Size(150, 23);
            this.txtOrderNumber.TabIndex = 1;
            // 
            // dtpOrderDate
            // 
            this.dtpOrderDate.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.dtpOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpOrderDate.Location = new System.Drawing.Point(150, 67);
            this.dtpOrderDate.Name = "dtpOrderDate";
            this.dtpOrderDate.Size = new System.Drawing.Size(150, 23);
            this.dtpOrderDate.TabIndex = 3;
            // 
            // dtpDeliveryDate
            // 
            this.dtpDeliveryDate.Enabled = false;
            this.dtpDeliveryDate.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.dtpDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDeliveryDate.Location = new System.Drawing.Point(150, 108);
            this.dtpDeliveryDate.Name = "dtpDeliveryDate";
            this.dtpDeliveryDate.Size = new System.Drawing.Size(150, 23);
            this.dtpDeliveryDate.TabIndex = 5;
            // 
            // chkDeliveryDate
            // 
            this.chkDeliveryDate.AutoSize = true;
            this.chkDeliveryDate.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.chkDeliveryDate.Location = new System.Drawing.Point(30, 110);
            this.chkDeliveryDate.Name = "chkDeliveryDate";
            this.chkDeliveryDate.Size = new System.Drawing.Size(110, 20);
            this.chkDeliveryDate.TabIndex = 4;
            this.chkDeliveryDate.Text = "Дата доставки";
            this.chkDeliveryDate.UseVisualStyleBackColor = true;
            this.chkDeliveryDate.CheckedChanged += new System.EventHandler(this.chkDeliveryDate_CheckedChanged);
            // 
            // cmbPickupPoint
            // 
            this.cmbPickupPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPickupPoint.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.cmbPickupPoint.FormattingEnabled = true;
            this.cmbPickupPoint.Location = new System.Drawing.Point(150, 147);
            this.cmbPickupPoint.Name = "cmbPickupPoint";
            this.cmbPickupPoint.Size = new System.Drawing.Size(250, 23);
            this.cmbPickupPoint.TabIndex = 7;
            // 
            // txtPickupCode
            // 
            this.txtPickupCode.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.txtPickupCode.Location = new System.Drawing.Point(150, 187);
            this.txtPickupCode.Name = "txtPickupCode";
            this.txtPickupCode.Size = new System.Drawing.Size(100, 23);
            this.txtPickupCode.TabIndex = 9;
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(150, 227);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(150, 23);
            this.cmbStatus.TabIndex = 11;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.btnSave.Location = new System.Drawing.Point(184, 280);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.btnCancel.Location = new System.Drawing.Point(300, 280);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblOrderNumber
            // 
            this.lblOrderNumber.AutoSize = true;
            this.lblOrderNumber.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.lblOrderNumber.Location = new System.Drawing.Point(30, 30);
            this.lblOrderNumber.Name = "lblOrderNumber";
            this.lblOrderNumber.Size = new System.Drawing.Size(89, 16);
            this.lblOrderNumber.TabIndex = 0;
            this.lblOrderNumber.Text = "Номер заказа:";
            // 
            // lblOrderDate
            // 
            this.lblOrderDate.AutoSize = true;
            this.lblOrderDate.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.lblOrderDate.Location = new System.Drawing.Point(30, 70);
            this.lblOrderDate.Name = "lblOrderDate";
            this.lblOrderDate.Size = new System.Drawing.Size(78, 16);
            this.lblOrderDate.TabIndex = 2;
            this.lblOrderDate.Text = "Дата заказа:";
            // 
            // lblDeliveryDate
            // 
            this.lblDeliveryDate.Location = new System.Drawing.Point(0, 0);
            this.lblDeliveryDate.Name = "lblDeliveryDate";
            this.lblDeliveryDate.Size = new System.Drawing.Size(100, 23);
            this.lblDeliveryDate.TabIndex = 0;
            // 
            // lblPickupPoint
            // 
            this.lblPickupPoint.AutoSize = true;
            this.lblPickupPoint.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.lblPickupPoint.Location = new System.Drawing.Point(30, 150);
            this.lblPickupPoint.Name = "lblPickupPoint";
            this.lblPickupPoint.Size = new System.Drawing.Size(91, 16);
            this.lblPickupPoint.TabIndex = 6;
            this.lblPickupPoint.Text = "Пункт выдачи:";
            // 
            // lblPickupCode
            // 
            this.lblPickupCode.AutoSize = true;
            this.lblPickupCode.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.lblPickupCode.Location = new System.Drawing.Point(30, 190);
            this.lblPickupCode.Name = "lblPickupCode";
            this.lblPickupCode.Size = new System.Drawing.Size(97, 16);
            this.lblPickupCode.TabIndex = 8;
            this.lblPickupCode.Text = "Код получения:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.lblStatus.Location = new System.Drawing.Point(30, 230);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(49, 16);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "Статус:";
            // 
            // OrderEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 341);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtPickupCode);
            this.Controls.Add(this.lblPickupCode);
            this.Controls.Add(this.cmbPickupPoint);
            this.Controls.Add(this.lblPickupPoint);
            this.Controls.Add(this.dtpDeliveryDate);
            this.Controls.Add(this.chkDeliveryDate);
            this.Controls.Add(this.dtpOrderDate);
            this.Controls.Add(this.lblOrderDate);
            this.Controls.Add(this.txtOrderNumber);
            this.Controls.Add(this.lblOrderNumber);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.Name = "OrderEditForm";
            this.Text = "Редактирование заказа";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}