using System;

namespace Lab9
{
    internal static class Program
    {
        private static void Main()
        {
            // ЧАСТЬ 1: Инкапсуляция и методы
            ConsoleUI.PrintHeader("Часть 1: Инкапсуляция и методы");

            Money firstMoney = new Money(100, 50);
            Money secondMoney = new Money(40, 80);

            ConsoleUI.PrintMoney(firstMoney, "Первый кошелек: ");
            ConsoleUI.PrintMoney(secondMoney, "Второй кошелек: ");

            Money staticResult = Money.Subtract(firstMoney, secondMoney);
            ConsoleUI.PrintMoney(staticResult, "Разница (статический метод): ");

            Money instanceResult = firstMoney.Subtract(secondMoney);
            ConsoleUI.PrintMoney(instanceResult, "Разница (метод экземпляра): ");

            // ЧАСТЬ 2: Перегрузка операторов
            ConsoleUI.PrintHeader("Часть 2: Перегрузка операторов");

            Money wallet = new Money(10, 99);
            ConsoleUI.PrintMoney(wallet, "Исходный кошелек: ");

            wallet++;
            ConsoleUI.PrintMoney(wallet, "После ++ (добавили 1 копейку): ");

            wallet--;
            ConsoleUI.PrintMoney(wallet, "После -- (вычли 1 копейку): ");

            int rublesOnly = (int)wallet;
            double kopeksOnly = (double)wallet;
            Console.WriteLine($"Явное приведение к int (рубли): {rublesOnly}");
            Console.WriteLine($"Неявное приведение к double (копейки): {kopeksOnly:F2}");

            Money afterAddKopeks = wallet + 5;
            ConsoleUI.PrintMoney(afterAddKopeks, "wallet + 5 копеек: ");

            Money afterAddLeft = 10 + wallet;
            ConsoleUI.PrintMoney(afterAddLeft, "10 копеек + wallet: ");

            Money afterSubKopeks = wallet - 200;
            ConsoleUI.PrintMoney(afterSubKopeks, "wallet - 200 копеек: ");

            // ЧАСТЬ 3: Коллекция MoneyArray
            ConsoleUI.PrintHeader("Часть 3: Коллекция MoneyArray");

            // 1. Создаем пустой массив (проверка конструктора без параметров)
            MoneyArray emptyWallets = new MoneyArray();
            Console.WriteLine("Пустая коллекция создана. Ее длина: " + emptyWallets.Length);

            // 2. Спрашиваем размер
            int size = ReadCount("Сколько кошельков вы хотите создать? ");

            Console.WriteLine("Выберите способ заполнения:");
            Console.WriteLine("1 - Случайные числа");
            Console.WriteLine("2 - Ввод с клавиатуры");
            int choice;
            while (true)
            {
                choice = ReadCount("Ваш выбор (1 или 2): ");
                if (choice == 1 || choice == 2)
                {
                    break; // Выходим из цикла, если ввели 1 или 2
                }
                Console.WriteLine("Ошибка: нужно ввести 1 или 2.");
            }

            MoneyArray userWallets;

            if (choice == 2)
            {
                // Вызываем конструктор с параметром bool (ввод с клавиатуры)
                userWallets = new MoneyArray(size, true);
            }
            else
            {
                // Конструктор с рандомом
                userWallets = new MoneyArray(size);
            }

            Console.WriteLine("\nВаша коллекция:");
            userWallets.Print();

            // 3. Поиск максимума
            Money maxWallet = userWallets.GetMax();
            ConsoleUI.PrintMoney(maxWallet, "\nМаксимальный кошелек в коллекции: ");

            // 4. Проверка индексатора (выход за границы)
            Console.WriteLine("\nПроверка защиты индексатора:");
            try
            {
                Money invalid = userWallets[size + 5]; // Специально выходим за границы
                Console.WriteLine(invalid);
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine($"Поймано исключение: {exception.Message}");
            }

            // Вывод счетчика объектов
            Console.WriteLine($"\nВсего создано объектов Money: {Money.ObjectCount}");
            Console.WriteLine($"Всего создано массивов MoneyArray: {MoneyArray.ObjectCount}");
        }

        // Вспомогательный метод для безопасного ввода размера массива
        private static int ReadCount(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value) && value >= 0)
                {
                    return value;
                }
                Console.WriteLine("Ошибка: введите целое число не меньше 0.");
            }
        }
    }
}