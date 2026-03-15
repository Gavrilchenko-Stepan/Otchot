using MyLib;
using MyLib.Models;
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
    public partial class OrdersForm : Form
    {
        private readonly User _currentUser;
        private readonly OrderRepository _orderRepo;
        private readonly OrderItemRepository _orderItemRepo;
        private readonly PickupPointRepository _pickupRepo;
        private readonly UserRepository _userRepo;
        private List<Order> _allOrders;
        private List<PickupPoint> _pickupPoints;
        private List<User> _customers;
        private Order _selectedOrder;
        private OrderCardControl _selectedOrderCard; // текущая выделенная карточка

        public OrdersForm(User user)
        {
            InitializeComponent();
            _currentUser = user;
            var db = new Database();
            _orderRepo = new OrderRepository(db);
            _orderItemRepo = new OrderItemRepository(db);
            _pickupRepo = new PickupPointRepository(db);
            _userRepo = new UserRepository(db);
            this.Text = "Управление заказами";

            btnAddOrder.Visible = _currentUser.IsAdmin;
            btnEditOrder.Visible = _currentUser.IsAdmin;
            btnDeleteOrder.Visible = _currentUser.IsAdmin;
        }

        private void OrdersForm_Load(object sender, EventArgs e)
        {
            LoadPickupPoints();
            LoadCustomers();
            LoadOrders();
        }

        private void LoadPickupPoints() => _pickupPoints = _pickupRepo.GetAllPickupPoints();
        private void LoadCustomers() => _customers = _userRepo.GetUsersByRole("Авторизированный клиент");
        private void LoadOrders()
        {
            _allOrders = _orderRepo.GetAllOrders();
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            IEnumerable<Order> filtered = _allOrders;

            if (cmbStatusFilter.SelectedItem != null && cmbStatusFilter.SelectedItem.ToString() != "Все")
            {
                string selectedStatus = cmbStatusFilter.SelectedItem.ToString();
                filtered = filtered.Where(o => o.Status == selectedStatus);
            }

            string search = txtSearch.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(search) && search != "поиск...")
            {
                filtered = filtered.Where(o =>
                    o.OrderNumber.ToString().Contains(search) ||
                    (GetCustomerName(o.CustomerId)?.ToLower().Contains(search) ?? false));
            }

            flowLayoutPanelOrders.Controls.Clear();
            _selectedOrderCard = null;

            foreach (var order in filtered)
            {
                var card = new OrderCardControl();
                string pickupAddress = GetPickupAddress(order.PickupPointId) ?? "Не указан";
                card.SetOrder(order, pickupAddress);

                Order currentOrder = order;

                // Одиночный клик – выделение
                card.CardClicked += (s, ord) =>
                {
                    if (_selectedOrderCard != null && _selectedOrderCard != card)
                        _selectedOrderCard.SetSelected(false);
                    card.SetSelected(true);
                    _selectedOrderCard = card;
                    _selectedOrder = ord;
                };

                // Двойной клик – редактирование (только для администратора)
                if (_currentUser.IsAdmin)
                {
                    card.CardDoubleClicked += (s, ord) => OpenEditOrder(ord);
                }

                flowLayoutPanelOrders.Controls.Add(card);
            }
        }

        private string GetCustomerName(int? customerId) =>
            customerId.HasValue ? _customers.FirstOrDefault(c => c.Id == customerId)?.FullName : null;

        private string GetPickupAddress(int? pointId) =>
            pointId.HasValue ? _pickupPoints.FirstOrDefault(p => p.Id == pointId)?.Address : null;

        private void OpenEditOrder(Order order)
        {
            using (var form = new OrderEditForm(order))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadOrders();
            }
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            using (var form = new OrderEditForm(null))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadOrders();
            }
        }

        private void btnEditOrder_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Выберите заказ в списке и дважды кликните на его карточку для редактирования.",
                "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            if (_selectedOrder == null)
            {
                MessageBox.Show("Выберите заказ для удаления (одинарный клик на его карточку).", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show($"Удалить заказ №{_selectedOrder.OrderNumber}?",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    _orderRepo.DeleteOrder(_selectedOrder.Id);
                    LoadOrders();
                    _selectedOrder = null;
                    _selectedOrderCard = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении: " + ex.Message, "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilter();
        private void txtSearch_TextChanged(object sender, EventArgs e) => ApplyFilter();
    }
}
