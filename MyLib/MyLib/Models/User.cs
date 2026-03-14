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

        // Проверка ролей
        public bool IsAdmin => Role == "Admin";
        public bool IsManager => Role == "Manager";
        public bool IsAuthorizedCustomer => Role == "AuthorizedCustomer";
        public bool IsGuest => false;
    }
}
