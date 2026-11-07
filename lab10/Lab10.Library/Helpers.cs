using System;
using System.Collections.Generic;

namespace Lab10.Library
{
    // Книга (не товар)
    public class Book : IInit, ICloneable
    {
        public string Title { get; set; }
        public int Year { get; set; }

        public Book() { Title = "Книга"; Year = 2000; }
        public Book(string title, int year) { Title = title; Year = year; }

        public void Init()
        {
            Console.Write("Название книги: ");
            Title = Console.ReadLine() ?? "Без названия";
            Console.Write("Год: ");
            Year = int.Parse(Console.ReadLine() ?? "0");
        }

        public void RandomInit()
        {
            Title = "Книга №" + new Random().Next(1, 100);
            Year = new Random().Next(1900, 2025);
        }

        public void ShowBook()
        {
            Console.WriteLine($"Книга: {Title}, Год: {Year}");
        }

        // глубокое копирование
        public object Clone()
        {
            return new Book(this.Title, this.Year);
        }

        // поверхностное (из методички)
        public Book ShallowCopy()
        {
            return (Book)this.MemberwiseClone();
        }
    }

    // сортировка по имени
    public class SortByName : IComparer<Product>
    {
        public int Compare(Product p1, Product p2)
        {
            if (p1 == null || p2 == null) return 0;
            return string.Compare(p1.Name, p2.Name, StringComparison.OrdinalIgnoreCase);
        }
    }
}