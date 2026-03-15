using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLib;
using MyLib.Models;

namespace Main_Form
{
    public partial class LoginForm : Form
    {
        private readonly Database _database;
        private readonly UserRepository _userRepository;

        public LoginForm()
        {
            InitializeComponent();
            _database = new Database();
            _userRepository = new UserRepository(_database);
            this.Text = "Авторизация";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                User user = _userRepository.Authenticate(login, password);

                if (user != null)
                {
                    OpenMainForm(user);
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка авторизации: " + ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuest_Click(object sender, EventArgs e)
        {
            User guest = _userRepository.GetGuestUser();
            OpenMainForm(guest);
        }

        private void OpenMainForm(User user)
        {
            var productRepo = new ProductRepository(_database);
            var mainForm = new MainForm(user, productRepo);
            mainForm.Show();
            this.Hide();
        }
    }
}
