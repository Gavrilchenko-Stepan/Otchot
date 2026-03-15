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

            if (_currentUser.IsAdmin)
            {
                btnAddOrder.Visible = true;
                btnEditOrder.Visible = true;
                btnDeleteOrder.Visible = true;
            }
            else
            {
                btnAddOrder.Visible = false;
                btnEditOrder.Visible = false;
                btnDeleteOrder.Visible = false;
            }
        }

        private void OrdersForm_Load(object sender, EventArgs e)
        {
            LoadPickupPoints();
            LoadCustomers();
            LoadOrders();
        }

        private void LoadPickupPoints()
        {
            _pickupPoints = _pickupRepo.GetAllPickupPoints();
        }

        private void LoadCustomers()
        {
            _customers = _userRepo.GetUsersByRole("Авторизированный клиент");
        }

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
                    (GetCustomerName(o.CustomerId) != null && GetCustomerName(o.CustomerId).ToLower().Contains(search)));
            }

            flowLayoutPanelOrders.Controls.Clear();
            foreach (var order in filtered)
            {
                var card = new OrderCardControl();

                string customerName = GetCustomerName(order.CustomerId);
                if (customerName == null)
                    customerName = "Неизвестно";

                string pickupAddress = GetPickupAddress(order.PickupPointId);
                if (pickupAddress == null)
                    pickupAddress = "Не указан";

                card.SetOrder(order, customerName, pickupAddress);

                Order currentOrder = order;
                card.CardClicked += (s, ord) => ShowOrderDetails(ord);

                if (_currentUser.IsAdmin)
                {
                    card.CardClicked += (s, ord) => OpenEditOrder(ord);
                }

                flowLayoutPanelOrders.Controls.Add(card);
            }
        }

        private string GetCustomerName(int? customerId)
        {
            if (!customerId.HasValue)
                return null;

            foreach (var customer in _customers)
            {
                if (customer.Id == customerId.Value)
                    return customer.FullName;
            }
            return null;
        }

        private string GetPickupAddress(int? pointId)
        {
            if (!pointId.HasValue)
                return null;

            foreach (var point in _pickupPoints)
            {
                if (point.Id == pointId.Value)
                    return point.Address;
            }
            return null;
        }

        private void ShowOrderDetails(Order order)
        {
            var items = _orderItemRepo.GetItemsByOrderId(order.Id);
            string details = "Заказ №" + order.OrderNumber + "\n" +
                             "Статус: " + order.Status + "\n" +
                             "Дата заказа: " + order.OrderDate.ToString("dd.MM.yyyy") + "\n";

            if (order.DeliveryDate.HasValue)
                details += "Дата доставки: " + order.DeliveryDate.Value.ToString("dd.MM.yyyy") + "\n";
            else
                details += "Дата доставки: не указана\n";

            details += "Пункт выдачи: " + GetPickupAddress(order.PickupPointId) + "\n";
            details += "Клиент: " + GetCustomerName(order.CustomerId) + "\n";
            details += "Код: " + order.PickupCode + "\n\nТовары:\n";

            foreach (var item in items)
            {
                details += item.ProductArticle + " – " + item.Quantity + " шт.\n";
            }

            MessageBox.Show(details, "Информация о заказе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OpenEditOrder(Order order)
        {
            using (var form = new OrderEditForm(order))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadOrders();
                }
            }
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            using (var form = new OrderEditForm(null))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadOrders();
                }
            }
        }

        private void btnEditOrder_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Выберите заказ в списке и нажмите на него для редактирования.");
        }

        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Функция удаления будет реализована позже.");
        }

        private void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }
    }
}
