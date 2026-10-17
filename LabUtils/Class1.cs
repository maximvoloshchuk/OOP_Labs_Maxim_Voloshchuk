using System;

namespace LabUtils
{
    public static class ConsoleInput
    {
        public static T Read<T>(string prompt) where T : IParsable<T>
        {
            while (true)
            {
                Console.Write(prompt);
                if (T.TryParse(Console.ReadLine(), null, out var value))
                    return value;
                Console.WriteLine("Ошибка! Введите корректное значение.");
            }
        }
    }

    public static class ArrayUtils
    {
        public static void Print(int[,] matrix, string title = "Матрица")
        {
            if (matrix == null || matrix.Length == 0)
            {
                Console.WriteLine($"{title}: пусто");
                return;
            }
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            Console.WriteLine($"{title}:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                    Console.Write($"{matrix[i, j],5}");
                Console.WriteLine();
            }
        }

        public static void Print(int[][] jagged, string title = "Рваный массив")
        {
            if (jagged == null || jagged.Length == 0)
            {
                Console.WriteLine($"{title}: пусто");
                return;
            }
            Console.WriteLine($"{title}:");
            for (int i = 0; i < jagged.Length; i++)
            {
                Console.Write($"[{i}] ");
                if (jagged[i] == null) { Console.WriteLine("null"); continue; }
                for (int j = 0; j < jagged[i].Length; j++)
                    Console.Write($"{jagged[i][j],5}");
                Console.WriteLine();
            }
        }
    }
}