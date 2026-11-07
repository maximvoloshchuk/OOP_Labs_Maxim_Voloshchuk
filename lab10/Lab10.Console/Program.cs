using System;
using Lab10.Library; // подключаем нашу библеотеку

namespace Lab10
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== ЧАСТЬ 1: Иерархия ===");

            // создаем массив с разными товарами
            Product[] products = new Product[]
            {
                new Toy("Кукла", 1500.50, 3),
                new DairyProduct("Молоко", 89.90, 3.2),
                new Toy("Лего", 4500.00, 6),
                new DairyProduct("Сыр", 800.00, 45.0)
            };

            Console.WriteLine("Вывод товаров:");
            foreach (Product p in products)
            {
                p.Show();
            }

            Console.WriteLine("\n=== ЧАСТЬ 2: Сортировка и поиск ===");

            // сортировка по цене
            Array.Sort(products);
            Console.WriteLine("\nСортировка по цене:");
            foreach (Product p in products) p.Show();

            // сортировка по имени
            Array.Sort(products, new SortByName());
            Console.WriteLine("\nСортировка по имени:");
            foreach (Product p in products) p.Show();

            // бинарный поиск "Сыр" (массив уже отсортирован по имени)
            Product target = new Toy("Сыр", 0, 0);
            int index = Array.BinarySearch(products, target, new SortByName());

            if (index >= 0)
                Console.WriteLine($"\nПоиск: 'Сыр' найден, индекс {index}");
            else
                Console.WriteLine("\nПоиск: 'Сыр' не найден");

            Console.WriteLine("\n=== ЧАСТЬ 3: IInit и клонирование ===");

            // массив из объектов, реализующих IInit
            IInit[] objects = new IInit[]
            {
                new Toy(),
                new DairyProduct(),
                new Book(),
                new Toy(),
                new Book()
            };

            Console.WriteLine("Заполняем рандомом:");
            foreach (IInit obj in objects)
            {
                obj.RandomInit();
            }

            Console.WriteLine("\nВывод массива:");
            foreach (IInit obj in objects)
            {
                // проверка типа
                if (obj is Product)
                {
                    Product p = (Product)obj;
                    p.Show();
                }
                else if (obj is Book)
                {
                    Book b = (Book)obj;
                    b.ShowBook();
                }
            }

            // клонирование
            Book original = new Book("Война и мир", 1869);
            Book shallowCopy = original.ShallowCopy();
            Book deepCopy = (Book)original.Clone();

            Console.WriteLine("\nДо изменения:");
            Console.Write("Оригинал: "); original.ShowBook();
            Console.Write("Поверхностная копия: "); shallowCopy.ShowBook();
            Console.Write("Глубокая копия: "); deepCopy.ShowBook();

            // меняем оригинал
            original.Title = "Война и мир (Том 1)";

            Console.WriteLine("\nПосле изменения:");
            Console.Write("Оригинал: "); original.ShowBook();
            Console.Write("Поверхностная копия: "); shallowCopy.ShowBook();
            Console.Write("Глубокая копия: "); deepCopy.ShowBook();
        }
    }
}