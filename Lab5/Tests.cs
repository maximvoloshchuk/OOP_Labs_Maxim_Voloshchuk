using System;

namespace Lab5
{
    public static class Tests
    {
        private static int passed = 0;
        private static int failed = 0;

        private static void Check(bool condition, string message)
        {
            if (condition) { Console.WriteLine($"  PASS: {message}"); passed++; }
            else { Console.WriteLine($"  FAIL: {message}"); failed++; }
        }

        public static void RunAll()
        {
            passed = 0;
            failed = 0;
            Console.WriteLine("=== Автотесты ===\n");

            TestAddColumnsToEnd();
            TestRemoveFirstRowWithK();
            TestProcessString();

            Console.WriteLine($"\nИтог: пройдено {passed}, провалено {failed}.");
        }

        private static void TestAddColumnsToEnd()
        {
            Console.WriteLine("Tasks.AddColumnsToEnd:");

            // Нормальные данные
            int[,] input = { { 1, 2 }, { 3, 4 } };
            int[,] result = Tasks.AddColumnsToEnd(input, 2);
            Check(result.GetLength(1) == 4, "количество столбцов увеличилось на K");
            Check(result[0, 0] == 1 && result[1, 1] == 4, "старые данные сохранены");

            // Ошибочные данные
            try { Tasks.AddColumnsToEnd(null!, 2); Check(false, "null бросает ArgumentNullException"); }
            catch (ArgumentNullException) { Check(true, "null бросает ArgumentNullException"); }

            try { Tasks.AddColumnsToEnd(input, 0); Check(false, "K=0 бросает ArgumentException"); }
            catch (ArgumentException) { Check(true, "K=0 бросает ArgumentException"); }

            Console.WriteLine();
        }

        private static void TestRemoveFirstRowWithK()
        {
            Console.WriteLine("Tasks.RemoveFirstRowWithK:");

            // Нормальные данные
            int[][] input = { new int[] { 1, 2 }, new int[] { 3, 4, 5 }, new int[] { 6 } };
            int[][] result = Tasks.RemoveFirstRowWithK(input, 3);
            Check(result.Length == 2, "размер массива уменьшился на 1");
            Check(result[0][0] == 1 && result[1][0] == 6, "удалена правильная строка");

            // Граничные данные (не найдено)
            int[][] result2 = Tasks.RemoveFirstRowWithK(input, 99);
            Check(result2.Length == 3, "если K не найдено, массив не меняется");

            // Ошибочные данные
            try { Tasks.RemoveFirstRowWithK(null!, 1); Check(false, "null бросает ArgumentNullException"); }
            catch (ArgumentNullException) { Check(true, "null бросает ArgumentNullException"); }

            Console.WriteLine();
        }

        private static void TestProcessString()
        {
            Console.WriteLine("Tasks.ProcessString:");

            // Нормальные данные
            string input = "В лесу родилась елочка! В лесу она росла.";
            string expected = "Елочка родилась лесу в! В лесу она росла.";
            string result = Tasks.ProcessString(input);
            Check(result == expected, "предложение с '!' перевернуто верно");
            Check(result.Contains("."), "предложение с '.' не тронуто");

            // Ошибочные данные
            try { Tasks.ProcessString(""); Check(false, "пустая строка бросает InvalidOperationException"); }
            catch (InvalidOperationException) { Check(true, "пустая строка бросает InvalidOperationException"); }

            Console.WriteLine();
        }
    }
}