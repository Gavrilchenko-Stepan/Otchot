using MyLib;
using MyLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main_Form
{
    internal static class Program
    {
        public static User CurrentUser { get; set; }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Показываем форму входа как диалог
            LoginForm loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // Если вход успешен (или выбран гость), запускаем главную форму
                Application.Run(new MainForm(CurrentUser, new ProductRepository(new Database())));
            }
        }
    }
}
