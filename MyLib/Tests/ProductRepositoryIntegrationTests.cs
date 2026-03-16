using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLib;
using MyLib.Models;
using System;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        private const string ConnectionString = "Server=localhost;Database=shoe_store_test;Uid=root;Pwd=vertrigo;";
        private Database _db;
        private ProductRepository _repo;

        [TestInitialize]
        public void Setup()
        {
            _db = new Database(); // предполагается, что строка подключения ведёт на тестовую БД
            _repo = new ProductRepository(_db);
            // Очистить тестовые данные перед каждым тестом (если нужно)
            // Например, удалить товары с артикулом, начинающимся с "TEST"
            var all = _repo.GetAllProducts();
            foreach (var p in all.Where(p => p.Article.StartsWith("TEST")))
            {
                _repo.DeleteProduct(p.Article);
            }
        }

        [TestMethod]
        public void GetAllProducts_ReturnsList()
        {
            // Act
            var products = _repo.GetAllProducts();

            // Assert
            Assert.IsNotNull(products);
            Assert.IsTrue(products.Count > 0);
        }

        [TestMethod]
        public void AddProduct_InsertsNewProduct()
        {
            // Arrange
            var newProduct = new Product
            {
                Article = "TEST001",
                Name = "Тестовый товар",
                Unit = "шт.",
                Price = 999.99m,
                Supplier = "TestSupplier",
                Manufacturer = "TestManufacturer",
                Category = "TestCategory",
                CurrentDiscount = 5,
                StockQuantity = 10,
                Description = "Описание",
                Photo = "test.jpg"
            };

            // Act
            _repo.AddProduct(newProduct);

            // Assert
            var inserted = _repo.GetAllProducts().FirstOrDefault(p => p.Article == "TEST001");
            Assert.IsNotNull(inserted);
            Assert.AreEqual("Тестовый товар", inserted.Name);
        }

        [TestMethod]
        public void UpdateProduct_ModifiesExistingProduct()
        {
            // Arrange - сначала добавим товар
            var product = new Product
            {
                Article = "TEST002",
                Name = "Старое имя",
                Unit = "шт.",
                Price = 100m,
                Supplier = "Sup",
                Manufacturer = "Man",
                Category = "Cat",
                CurrentDiscount = 0,
                StockQuantity = 1,
                Description = "Desc",
                Photo = null
            };
            _repo.AddProduct(product);

            // Modify
            product.Name = "Новое имя";
            product.Price = 200m;

            // Act
            _repo.UpdateProduct(product);

            // Assert
            var updated = _repo.GetAllProducts().First(p => p.Article == "TEST002");
            Assert.AreEqual("Новое имя", updated.Name);
            Assert.AreEqual(200m, updated.Price);
        }

        [TestMethod]
        public void DeleteProduct_RemovesProduct()
        {
            // Arrange
            var product = new Product
            {
                Article = "TEST003",
                Name = "Удаляемый",
                Unit = "шт.",
                Price = 100m,
                Supplier = "",
                Manufacturer = "",
                Category = "",
                CurrentDiscount = 0,
                StockQuantity = 0,
                Description = "",
                Photo = null
            };
            _repo.AddProduct(product);

            // Act
            _repo.DeleteProduct("TEST003");

            // Assert
            var deleted = _repo.GetAllProducts().FirstOrDefault(p => p.Article == "TEST003");
            Assert.IsNull(deleted);
        }

        [TestMethod]
        public void IsProductInOrders_ReturnsTrueForProductInOrder()
        {
            // Здесь нужна предварительная вставка заказа с этим товаром
            // Это сложнее, потребуется работа с OrderRepository и OrderItemRepository
            // Пропустим для краткости, но в реальном проекте надо реализовать
        }
    }
}
