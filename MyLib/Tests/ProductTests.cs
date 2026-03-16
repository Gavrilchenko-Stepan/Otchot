using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLib.Models;
using System;

namespace Tests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        [DataRow(1000.0, 20, 800.0)]
        [DataRow(1500.5, 10, 1350.45)]
        [DataRow(2000.0, 0, 2000.0)]
        [DataRow(99.99, 50, 49.995)]
        public void FinalPrice_CalculatesCorrectly(double price, int discount, double expected)
        {
            var product = new Product
            {
                Price = (decimal)price,
                CurrentDiscount = discount
            };

            decimal finalPrice = product.FinalPrice;

            Assert.AreEqual((decimal)expected, finalPrice);
        }

        [TestMethod]
        [DataRow(10, true)]
        [DataRow(0, false)]
        [DataRow(15, true)]
        public void HasDiscount_ReturnsExpected(int discount, bool expected)
        {
            var product = new Product { CurrentDiscount = discount };

            Assert.AreEqual(expected, product.HasDiscount);
        }

        [TestMethod]
        [DataRow(0, true)]
        [DataRow(5, false)]
        public void OutOfStock_ReturnsExpected(int stock, bool expected)
        {
            var product = new Product { StockQuantity = stock };

            Assert.AreEqual(expected, product.OutOfStock);
        }

        [TestMethod]
        [DataRow(16, true)]
        [DataRow(15, false)]
        [DataRow(20, true)]
        public void DiscountMoreThan15_ReturnsExpected(int discount, bool expected)
        {
            var product = new Product { CurrentDiscount = discount };

            Assert.AreEqual(expected, product.DiscountMoreThan15);
        }
    }
}
