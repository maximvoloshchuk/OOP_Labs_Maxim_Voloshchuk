using LabUtils;
using System;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Lab5
{
    internal static class Program
    {
        static int[,] table;
        static int[][] jaggedArray;
        static string text = string.Empty;

        static void Main()
        {
            ShowMainMenu();
        }

        static void ShowMainMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine(
                    @"1. Работа с двумерным массивом
2. Работа с рваным массивом
3. Работа со строкой
4. Автотесты
5. Выход");
                int choice = ConsoleInput.Read<int>("Выберите пункт: ");

                switch (choice)
                {
                    case 1: ShowTableMenu(); break;
                    case 2: ShowJaggedMenu(); break;
                    case 3: ShowStringMenu(); break;
                    case 4:
                        Console.Clear();
                        Tests.RunAll();
                        Console.WriteLine("\nНажмите Enter...");
                        Console.ReadLine();
                        break;
                    case 5: isRunning = false; break;
                    default:
                        Console.WriteLine("Неверный пункт. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void ShowTableMenu()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine(
                    @"1. Создать массив случайно
2. Напечатать массив
3. Добавить K столбцов в конец
4. Назад");
                int choice = ConsoleInput.Read<int>("Выберите пункт: ");

                switch (choice)
                {
                    case 1:
                        int rows = ConsoleInput.Read<int>("Введите количество строк: ");
                        int cols = ConsoleInput.Read<int>("Введите количество столбцов: ");
                        table = Tasks.CreateRandomTable(rows, cols);
                        Console.WriteLine("Массив создан.");
                        Pause();
                        break;
                    case 2:
                        ArrayUtils.Print(table, "Текущий массив");
                        Pause();
                        break;
                    case 3:
                        if (table == null)
                        {
                            Console.WriteLine("Массив пуст! Сначала создайте его.");
                            Pause();
                            break;
                        }
                        int k = ConsoleInput.Read<int>("Введите K: ");
                        try
                        {
                            table = Tasks.AddColumnsToEnd(table, k);
                            ArrayUtils.Print(table, "Новый массив");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка: {ex.Message}");
                        }
                        Console.ReadKey();
                        break;
                    case 4: isRunning = false; break;
                }
            }
        }

        static void ShowJaggedMenu()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine(
                    @"1. Создать массив случайно
2. Напечатать массив
3. Удалить первую строку с числом K
4. Назад");
                int choice = ConsoleInput.Read<int>("Выберите пункт: ");

                switch (choice)
                {
                    case 1:
                        int rows = ConsoleInput.Read<int>("Введите количество строк: ");
                        jaggedArray = Tasks.CreateRandomJagged(rows);
                        Console.WriteLine("Массив создан.");
                        Pause();
                        break;
                    case 2:
                        ArrayUtils.Print(jaggedArray, "Текущий массив");
                        Pause();
                        break;
                    case 3:
                        if (jaggedArray == null)
                        {
                            Console.WriteLine("Массив пуст! Сначала создайте его.");
                            Pause();
                            break;
                        }
                        int k = ConsoleInput.Read<int>("Введите число K: ");
                        try
                        {
                            jaggedArray = Tasks.RemoveFirstRowWithK(jaggedArray, k);
                            ArrayUtils.Print(jaggedArray, "Новый массив");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка: {ex.Message}");
                        }
                        Console.ReadKey();
                        break;
                    case 4: isRunning = false; break;
                }
            }
        }

        static void ShowStringMenu()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine(
                    @"1. Ввести строку с клавиатуры
2. Взять тестовую строку
3. Обработать строку
4. Напечатать результат
5. Назад");
                int choice = ConsoleInput.Read<int>("Выберите пункт: ");

                switch (choice)
                {
                    case 1:
                        Console.Write("Введите строку: ");
                        text = Console.ReadLine() ?? "";
                        break;
                    case 2:
                        text = "В лесу родилась елочка! В лесу она росла. Зимой и летом стройная, зеленая была!";
                        Console.WriteLine("Тестовая строка загружена.");
                        Pause();
                        break;
                    case 3:
                        if (string.IsNullOrEmpty(text))
                        {
                            Console.WriteLine("Строка пуста!");
                        }
                        else
                        {
                            try
                            {
                                text = Tasks.ProcessString(text);
                                Console.WriteLine("Обработано!");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Ошибка: {ex.Message}");
                            }
                        }
                        Pause();
                        break;
                    case 4:
                        Console.WriteLine(text);
                        Pause();
                        break;
                    case 5: isRunning = false; break;
                }
            }
        }
        static void Pause()
        {
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}