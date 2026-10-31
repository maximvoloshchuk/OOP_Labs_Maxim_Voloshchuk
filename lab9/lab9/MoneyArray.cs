using System;

namespace Lab9
{
    public class MoneyArray
    {
        private Money[] _money;
        private static int _objectCount = 0;
        private static readonly Random _random = new Random(); // Вынесли рандом на уровень класса

        public static int ObjectCount => _objectCount;
        public int Length => _money.Length;

        public MoneyArray()
        {
            _money = new Money[0];
            _objectCount++;
        }

        // Конструктор с рандомным заполнением
        public MoneyArray(int size)
        {
            if (size < 0)
            {
                throw new ArgumentException("Размер массива не может быть отрицательным.");
            }

            _money = new Money[size];
            for (int i = 0; i < size; i++)
            {
                _money[i] = new Money(_random.Next(0, 1000), _random.Next(0, 100));
            }
            _objectCount++;
        }

        // Конструктор с заполнением с клавиатуры
        public MoneyArray(int size, bool fromKeyboard)
        {
            if (size < 0)
            {
                throw new ArgumentException("Размер массива не может быть отрицательным.");
            }

            _money = new Money[size];
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine($"Ввод кошелька №{i + 1}:");
                while (true)
                {
                    try
                    {
                        Console.Write("  Рубли: ");
                        int rubles = int.Parse(Console.ReadLine() ?? "");
                        Console.Write("  Копейки: ");
                        int kopeks = int.Parse(Console.ReadLine() ?? "");

                        _money[i] = new Money(rubles, kopeks);
                        break; // Если ошибок нет, выходим из цикла while
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message} Попробуйте снова.");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Ошибка: нужно вводить числа. Попробуйте снова.");
                    }
                }
            }
            _objectCount++;
        }

        public void Print()
        {
            if (_money.Length == 0)
            {
                Console.WriteLine("Массив пуст.");
                return;
            }

            for (int i = 0; i < _money.Length; i++)
            {
                Console.Write($"[{i}] ");
                _money[i].Print();
            }
        }

        public Money this[int index]
        {
            get
            {
                if (index < 0 || index >= _money.Length)
                {
                    throw new ArgumentException("Индекс выходит за границы массива.");
                }
                return _money[index];
            }
            set
            {
                if (index < 0 || index >= _money.Length)
                {
                    throw new ArgumentException("Индекс выходит за границы массива.");
                }
                _money[index] = value;
            }
        }

        public Money GetMax()
        {
            if (_money.Length == 0)
            {
                throw new InvalidOperationException("Массив пуст.");
            }

            Money max = _money[0];
            long maxTotal = max.Rubles * 100L + max.Kopeks;

            for (int i = 1; i < _money.Length; i++)
            {
                Money current = _money[i];
                long currentTotal = current.Rubles * 100L + current.Kopeks;

                if (currentTotal > maxTotal)
                {
                    max = current;
                    maxTotal = currentTotal;
                }
            }

            return max;
        }
    }
}