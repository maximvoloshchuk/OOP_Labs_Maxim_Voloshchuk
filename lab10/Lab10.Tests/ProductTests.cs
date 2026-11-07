using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab10.Library; // Подключаем нашу библиотеку
using System;

namespace Lab10.Tests
{
    [TestClass]
    public class ProductTests
    {
        // 1. Проверка, что цена не может быть отрицательной
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PriceCannotBeNegative()
        {
            // Если передать отрицательную цену, сеттер свойства должен кинуть ошибку
            Toy toy = new Toy("Кукла", -100, 3);
        }

        // 2. Проверка, что возраст игрушки не может быть больше 18
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ToyAgeCannotExceed18()
        {
            Toy toy = new Toy("Машинка", 500, 20);
        }

        // 3. Проверка сортировки по цене (IComparable)
        [TestMethod]
        public void ProductsAreSortedByPrice()
        {
            Product[] products = new Product[]
            {
                new Toy("Дорогая", 5000, 3),
                new DairyProduct("Дешевая", 50, 2.5),
                new Toy("Средняя", 1000, 5)
            };

            Array.Sort(products);

            // После сортировки первым должен идти самый дешевый (50)
            Assert.AreEqual(50, products[0].Price);
            // А последним — самый дорогой (5000)
            Assert.AreEqual(5000, products[2].Price);
        }

        // 4. Проверка сортировки по имени (IComparer)
        [TestMethod]
        public void ProductsAreSortedByName()
        {
            Product[] products = new Product[]
            {
                new Toy("Яблоко", 100, 3),
                new DairyProduct("Апельсин", 200, 2.5),
                new Toy("Банан", 300, 5)
            };

            Array.Sort(products, new SortByName());

            // По алфавиту первым должен идти "Апельсин"
            Assert.AreEqual("Апельсин", products[0].Name);
            // Последним — "Яблоко"
            Assert.AreEqual("Яблоко", products[2].Name);
        }

        // 5. Проверка бинарного поиска
        [TestMethod]
        public void BinarySearchFindsProduct()
        {
            Product[] products = new Product[]
            {
                new Toy("Апельсин", 100, 3),
                new DairyProduct("Банан", 200, 2.5),
                new Toy("Яблоко", 300, 5)
            };

            // Сначала сортируем, чтобы бинарный поиск работал
            Array.Sort(products, new SortByName());

            Product target = new Toy("Банан", 0, 0);
            int index = Array.BinarySearch(products, target, new SortByName());

            // Проверяем, что элемент найден (индекс >= 0) и это действительно "Банан"
            Assert.IsTrue(index >= 0);
            Assert.AreEqual("Банан", products[index].Name);
        }

        // 6. Проверка глубокого клонирования (Clone)
        [TestMethod]
        public void CloneCreatesIndependentCopy()
        {
            Book original = new Book("Оригинал", 2020);
            Book clone = (Book)original.Clone();

            // Меняем название у оригинала
            original.Title = "Измененный";

            // Клон не должен меняться, так как это глубокая копия
            Assert.AreEqual("Оригинал", clone.Title);
        }

        // 7. Проверка поверхностного копирования
        [TestMethod]
        public void ShallowCopyCreatesNewReference()
        {
            Book original = new Book("Оригинал", 2020);
            Book shallowCopy = original.ShallowCopy();

            // 1. Проверяем, что это разные объекты в памяти
            // (если они разные, то адреса не совпадают)
            Assert.AreNotSame(original, shallowCopy);

            // 2. Проверяем, что значения полей на момент копирования одинаковые
            Assert.AreEqual(original.Title, shallowCopy.Title);

            // 3. Доказываем, что при изменении оригинала копия не меняется
            // (потому что строки в C# неизменяемые, и original.Title теперь указывает на новую строку)
            original.Title = "Новое название";
            Assert.AreEqual("Оригинал", shallowCopy.Title);
        }
    }
}