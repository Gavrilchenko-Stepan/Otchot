using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Role { get; set; }              // role
        public string FullName { get; set; }           // full_name
        public string Login { get; set; }              // login
        public string Password { get; set; }           // password

        public bool IsAdmin
        {
            get { return Role == "Администратор"; }
        }

        public bool IsManager
        {
            get { return Role == "Менеджер"; }
        }

        public bool IsAuthorizedCustomer
        {
            get { return Role == "Авторизированный клиент"; }
        }

        public bool IsGuest
        {
            get { return Role == "Гость"; }
        }
    }
}
