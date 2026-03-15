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

            if (order == null)
                _order = new Order();
            else
                _order = order;

            var db = new Database();
            _orderRepo = new OrderRepository(db);
            _pickupRepo = new PickupPointRepository(db);

            LoadPickupPoints();
            LoadStatuses();

            if (_order.Id > 0)
                LoadOrderData();
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
            cmbStatus.Items.Add("Новый");
            cmbStatus.Items.Add("Завершен");
            cmbStatus.Items.Add("Отменен");
            cmbStatus.SelectedIndex = 0;
        }

        private void LoadOrderData()
        {
            txtOrderNumber.Text = _order.OrderNumber.ToString();
            dtpOrderDate.Value = _order.OrderDate;

            if (_order.DeliveryDate.HasValue)
            {
                chkDeliveryDate.Checked = true;
                dtpDeliveryDate.Value = _order.DeliveryDate.Value;
            }
            else
            {
                chkDeliveryDate.Checked = false;
                dtpDeliveryDate.Enabled = false;
            }

            if (_order.PickupPointId.HasValue)
                cmbPickupPoint.SelectedValue = _order.PickupPointId.Value;

            txtPickupCode.Text = _order.PickupCode;
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
                string.IsNullOrWhiteSpace(txtPickupCode.Text) ||
                cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Заполните все обязательные поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int orderNumber;
            if (!int.TryParse(txtOrderNumber.Text, out orderNumber))
            {
                MessageBox.Show("Номер заказа должен быть числом.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _order.OrderNumber = orderNumber;
            _order.OrderDate = dtpOrderDate.Value;

            if (chkDeliveryDate.Checked)
                _order.DeliveryDate = dtpDeliveryDate.Value;
            else
                _order.DeliveryDate = null;

            _order.PickupPointId = (int)cmbPickupPoint.SelectedValue;
            _order.PickupCode = txtPickupCode.Text.Trim();
            _order.Status = cmbStatus.SelectedItem.ToString();

            if (_order.Id == 0)
            {
                _orderRepo.AddOrder(_order);
            }
            else
            {
                _orderRepo.UpdateOrder(_order);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
