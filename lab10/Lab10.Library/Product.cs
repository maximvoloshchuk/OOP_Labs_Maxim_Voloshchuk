using System;

namespace Lab10.Library
{
    // базовый класс, абстрактный (просто "товар" не создать)
    public abstract class Product : IInit, IComparable<Product>
    {
        // protected чтобы наследники видели
        protected string _name;
        protected double _price;

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Имя пустое!");
                _name = value;
            }
        }

        public double Price
        {
            get => _price;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Цена < 0!");
                _price = value;
            }
        }

        // пустой конструктор, чтобы создавать массив
        public Product()
        {
            _name = "Неизвестно";
            _price = 0;
        }

        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }

        // копирование
        public Product(Product other)
        {
            _name = other._name;
            _price = other._price;
        }

        // virtual, для возможности переопределения в наследниках
        public virtual void Show()
        {
            Console.Write($"Название: {_name}, Цена: {_price:F2} руб.");
        }

        public virtual void Init()
        {
            Console.Write("Название: ");
            Name = Console.ReadLine() ?? "Без названия";

            Console.Write("Цена: ");
            Price = double.Parse(Console.ReadLine() ?? "0");
        }

        public virtual void RandomInit()
        {
            Random rnd = new Random();
            string[] names = { "Кукла", "Молоко", "Мяч", "Сыр", "Конструктор", "Йогурт" };
            Name = names[rnd.Next(names.Length)];
            Price = Math.Round(rnd.NextDouble() * 1000, 2);
        }

        // сортировка по умолчанию (по цене)
        public int CompareTo(Product other)
        {
            if (other == null) return 1;
            return this.Price.CompareTo(other.Price);
        }
    }

    // наследник Игрушка
    public class Toy : Product
    {
        private int _minAge;

        public int MinAge
        {
            get => _minAge;
            set
            {
                if (value < 0 || value > 18) throw new ArgumentException("Возраст 0-18");
                _minAge = value;
            }
        }

        public Toy() : base() { _minAge = 3; }
        public Toy(string name, double price, int minAge) : base(name, price) { MinAge = minAge; }
        public Toy(Toy other) : base(other) { _minAge = other._minAge; }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($", Возраст: {_minAge}+ (Игрушка)");
        }

        public override void RandomInit()
        {
            base.RandomInit();
            MinAge = new Random().Next(0, 15);
        }
    }

    // наследник Молочный продукт
    public class DairyProduct : Product
    {
        private double _fatContent;

        public double FatContent
        {
            get => _fatContent;
            set
            {
                if (value < 0 || value > 100) throw new ArgumentException("Жирность 0-100");
                _fatContent = value;
            }
        }

        public DairyProduct() : base() { _fatContent = 3.2; }
        public DairyProduct(string name, double price, double fat) : base(name, price) { FatContent = fat; }
        public DairyProduct(DairyProduct other) : base(other) { _fatContent = other._fatContent; }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($", Жирность: {_fatContent}% (Молочка)");
        }

        public override void RandomInit()
        {
            base.RandomInit();
            FatContent = Math.Round(new Random().NextDouble() * 10, 1);
        }
    }
}