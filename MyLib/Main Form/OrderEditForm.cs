using MyLib;
using MyLib.Models;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main_Form
{
    public partial class OrderEditForm : Form
    {
        private Order _order;
        private OrderRepository _orderRepo;
        private PickupPointRepository _pickupRepo;
        private List<PickupPoint> _pickupPoints;

        public OrderEditForm(Order order)
        {
            InitializeComponent();

            _order = order ?? new Order();
            var db = new Database();
            _orderRepo = new OrderRepository(db);
            _pickupRepo = new PickupPointRepository(db);

            LoadPickupPoints();
            LoadStatuses();

            if (_order.Id > 0) LoadOrderData();
        }

        private void LoadPickupPoints()
        {
            _pickupPoints = _pickupRepo.GetAllPickupPoints();
            cmbPickupPoint.DataSource = _pickupPoints;
            cmbPickupPoint.DisplayMember = "Address";
            cmbPickupPoint.ValueMember = "Id";
            cmbPickupPoint.SelectedIndex = -1;
        }

        private void LoadStatuses()
        {
            cmbStatus.Items.AddRange(new[] { "Новый", "Завершен", "Отменен" });
            cmbStatus.SelectedIndex = 0;
        }

        private void LoadOrderData()
        {
            txtOrderNumber.Text = _order.OrderNumber.ToString();
            dtpOrderDate.Value = _order.OrderDate;
            chkDeliveryDate.Checked = _order.DeliveryDate.HasValue;
            if (_order.DeliveryDate.HasValue)
                dtpDeliveryDate.Value = _order.DeliveryDate.Value;
            if (_order.PickupPointId.HasValue)
                cmbPickupPoint.SelectedValue = _order.PickupPointId.Value;
            cmbStatus.SelectedItem = _order.Status;
        }

        private void chkDeliveryDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpDeliveryDate.Enabled = chkDeliveryDate.Checked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOrderNumber.Text) ||
                cmbPickupPoint.SelectedItem == null ||
                cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Заполните все обязательные поля.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtOrderNumber.Text, out int orderNumber))
            {
                MessageBox.Show("Номер заказа должен быть числом.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _order.OrderNumber = orderNumber;
            _order.OrderDate = dtpOrderDate.Value;
            _order.DeliveryDate = chkDeliveryDate.Checked ? dtpDeliveryDate.Value : (DateTime?)null;
            _order.PickupPointId = (int)cmbPickupPoint.SelectedValue;
            _order.Status = cmbStatus.SelectedItem.ToString();

            try
            {
                if (_order.Id == 0)
                    _orderRepo.AddOrder(_order);
                else
                    _orderRepo.UpdateOrder(_order);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
