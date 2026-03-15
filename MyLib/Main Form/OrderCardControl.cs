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
    public partial class OrderCardControl : UserControl
    {
        private Order _order;
        public event EventHandler<Order> CardClicked;

        public OrderCardControl()
        {
            InitializeComponent();

            // Делаем всю карточку кликабельной
            this.Click += OrderCardControl_Click;
            panel1.Click += (s, e) => OrderCardControl_Click(this, e);
            panel2.Click += (s, e) => OrderCardControl_Click(this, e);

            // Все дочерние элементы тоже кликабельны
            foreach (Control ctrl in panel1.Controls)
                ctrl.Click += (s, e) => OrderCardControl_Click(this, e);
            foreach (Control ctrl in panel2.Controls)
                ctrl.Click += (s, e) => OrderCardControl_Click(this, e);
        }

        /// <summary>
        /// Заполняет карточку данными заказа
        /// </summary>
        /// <param name="order">Заказ</param>
        /// <param name="pickupAddress">Адрес пункта выдачи (уже готовый текст)</param>
        public void SetOrder(Order order, string pickupAddress, string pickupAddress1)
        {
            _order = order;

            // Заполняем текстовые поля
            lblOrderArticle.Text = "Артикул заказа: " + order.OrderNumber;
            lblStatus.Text = "Статус заказа: " + order.Status;
            lblPickupAddress.Text = "Адрес пункта выдачи: " + pickupAddress;
            lblOrderDate.Text = "Дата заказа: " + order.OrderDate.ToString("dd.MM.yyyy");

            // Дата доставки – просто меняем текст существующей метки
            if (order.DeliveryDate.HasValue)
                lblDeliveryDate.Text = "Дата доставки: " + order.DeliveryDate.Value.ToString("dd.MM.yyyy");
            else
                lblDeliveryDate.Text = "Дата доставки: не указана";
        }

        private void OrderCardControl_Click(object sender, EventArgs e)
        {
            // Вызываем событие при клике на карточку
            CardClicked?.Invoke(this, _order);
        }
    }
}
