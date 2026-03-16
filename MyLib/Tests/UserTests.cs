using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLib.Models;
using System;

namespace Tests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        [DataRow("Администратор", true, false, false, false)]
        [DataRow("Менеджер", false, true, false, false)]
        [DataRow("Авторизированный клиент", false, false, true, false)]
        [DataRow("Гость", false, false, false, true)]
        [DataRow("Неизвестная роль", false, false, false, false)]
        public void RoleProperties_ReturnCorrectValues(string role,
            bool expectedIsAdmin, bool expectedIsManager,
            bool expectedIsAuthorizedCustomer, bool expectedIsGuest)
        {
            var user = new User { Role = role };

            Assert.AreEqual(expectedIsAdmin, user.IsAdmin);
            Assert.AreEqual(expectedIsManager, user.IsManager);
            Assert.AreEqual(expectedIsAuthorizedCustomer, user.IsAuthorizedCustomer);
            Assert.AreEqual(expectedIsGuest, user.IsGuest);
        }
    }
}
