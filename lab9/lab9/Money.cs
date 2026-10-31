using System;

namespace Lab9
{
    public class Money
    {
        // Приватные поля
        private int _rubles;
        private int _kopeks;

        // Счетчик созданных объектов
        private static int _objectCount = 0;

        public int Rubles
        {
            get => _rubles;
            set
            {
                if (value < 0)
                {
                    // Если сюда зашли, метод выйдет через throw
                    throw new ArgumentException("Рубли не могут быть отрицательными.");
                }
                _rubles = value;
            }
        }

        public int Kopeks
        {
            get => _kopeks;
            set
            {
                if (value < 0 || value > 99)
                {
                    throw new ArgumentException("Копейки должны быть от 0 до 99.");
                }
                _kopeks = value;
            }
        }

        // Только чтение снаружи, менять счетчик нельзя.
        public static int ObjectCount => _objectCount;

        public Money() : this(0, 0)
        {
        }

        public Money(int rubles, int kopeks)
        {
            if (rubles < 0)
            {
                throw new ArgumentException("Сумма не может быть отрицательной.");
            }

            Rubles = rubles;
            Kopeks = kopeks;
            _objectCount++;
        }

        public void Print()
        {
            // D2 - чтобы 5 копеек печатались как 05
            Console.WriteLine($"{_rubles} руб. {_kopeks:D2} коп.");
        }

        // Статический вариант
        public static Money Subtract(Money first, Money second)
        {
            // Переводим обе суммы в копейки
            long totalFirst = first.Rubles * 100L + first.Kopeks;
            long totalSecond = second.Rubles * 100L + second.Kopeks;
            long difference = totalFirst - totalSecond;

            // По условию отрицательный результат запрещен
            if (difference < 0)
            {
                difference = 0;
            }

            return new Money((int)(difference / 100), (int)(difference % 100));
        }

        // Метод экземпляра
        public Money Subtract(Money other)
        {
            return Subtract(this, other);
        }

        // ++ добавляет одну копейку. Сами переносим 100 копеек в рубль
        public static Money operator ++(Money money)
        {
            long total = money.Rubles * 100L + money.Kopeks + 1;
            return new Money((int)(total / 100), (int)(total % 100));
        }

        // -- вычитает копейку, но не дает уйти в минус
        public static Money operator --(Money money)
        {
            long total = money.Rubles * 100L + money.Kopeks - 1;

            if (total < 0)
            {
                total = 0;
            }

            return new Money((int)(total / 100), (int)(total % 100));
        }

        // Явное приведение: (int)wallet вернет только рубли
        public static explicit operator int(Money money)
        {
            return money.Rubles;
        }

        // Неявное приведение: double d = wallet; вернет копейки с плавающей точкой
        public static implicit operator double(Money money)
        {
            return money.Kopeks / 100.0;
        }

        // wallet + 5 — левосторонняя
        public static Money operator +(Money money, int kopeksToAdd)
        {
            long total = money.Rubles * 100L + money.Kopeks + kopeksToAdd;

            if (total < 0)
            {
                total = 0;
            }

            return new Money((int)(total / 100), (int)(total % 100));
        }

        // 5 + wallet — правосторонняя
        public static Money operator +(int kopeksToAdd, Money money)
        {
            return money + kopeksToAdd;
        }

        // wallet - 5 — левосторонняя
        public static Money operator -(Money money, int kopeksToSub)
        {
            long total = money.Rubles * 100L + money.Kopeks - kopeksToSub;

            if (total < 0)
            {
                total = 0;
            }

            return new Money((int)(total / 100), (int)(total % 100));
        }

        // 5 - wallet — правосторонняя
        public static Money operator -(int kopeksToSub, Money money)
        {
            return money - kopeksToSub;
        }
    }
}