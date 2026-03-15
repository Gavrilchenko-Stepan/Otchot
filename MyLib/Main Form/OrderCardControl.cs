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
        private Color _originalBackColor;
        public event EventHandler<Order> CardClicked;        // одиночный клик (выделение)
        public event EventHandler<Order> CardDoubleClicked;  // двойной клик (действие)

        public OrderCardControl()
        {
            InitializeComponent();

            this.Click += OrderCardControl_Click;
            this.DoubleClick += OrderCardControl_DoubleClick;

            panel1.Click += (s, e) => OrderCardControl_Click(this, e);
            panel1.DoubleClick += (s, e) => OrderCardControl_DoubleClick(this, e);
            panel2.Click += (s, e) => OrderCardControl_Click(this, e);
            panel2.DoubleClick += (s, e) => OrderCardControl_DoubleClick(this, e);

            foreach (Control ctrl in panel1.Controls)
            {
                ctrl.Click += (s, e) => OrderCardControl_Click(this, e);
                ctrl.DoubleClick += (s, e) => OrderCardControl_DoubleClick(this, e);
            }
            foreach (Control ctrl in panel2.Controls)
            {
                ctrl.Click += (s, e) => OrderCardControl_Click(this, e);
                ctrl.DoubleClick += (s, e) => OrderCardControl_DoubleClick(this, e);
            }
        }

        public void SetOrder(Order order, string pickupAddress)
        {
            _order = order;
            lblOrderArticle.Text = "Артикул заказа: " + order.OrderNumber;
            lblStatus.Text = "Статус заказа: " + order.Status;
            lblPickupAddress.Text = "Адрес пункта выдачи: " + pickupAddress;
            lblOrderDate.Text = "Дата заказа: " + order.OrderDate.ToString("dd.MM.yyyy");
            lblDeliveryDate.Text = order.DeliveryDate.HasValue
                ? "Дата доставки: " + order.DeliveryDate.Value.ToString("dd.MM.yyyy")
                : "Дата доставки: не указана";

            _originalBackColor = this.BackColor;
        }

        public void SetSelected(bool selected)
        {
            this.BackColor = selected ? Color.LightYellow : _originalBackColor;
        }

        private void OrderCardControl_Click(object sender, EventArgs e)
        {
            CardClicked?.Invoke(this, _order);
        }

        private void OrderCardControl_DoubleClick(object sender, EventArgs e)
        {
            CardDoubleClicked?.Invoke(this, _order);
        }
    }
}
