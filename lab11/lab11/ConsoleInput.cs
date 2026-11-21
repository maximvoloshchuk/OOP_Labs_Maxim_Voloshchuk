using System;
using System.Globalization;

namespace Lab11
{
    // чтобы программа не ломалась при неправильном вводе
    public static class ConsoleInput
    {
        public static string ReadString(string message)
        {
            Console.Write(message);
            string text = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(text))
            {
                Console.Write("Пустая строка, попробуйте ещё раз: ");
                text = Console.ReadLine();
            }
            return text.Trim();
        }

        public static int ReadInt(string message)
        {
            Console.Write(message);
            int number;
            while (!int.TryParse(Console.ReadLine(), out number))
                Console.Write("Нужно целое число, попробуйте ещё раз: ");
            return number;
        }

        public static double ReadDouble(string message)
        {
            Console.Write(message);
            double number;
            // запятую заменяем на точку, чтобы работал ввод 4,5
            while (!double.TryParse(
                Console.ReadLine().Replace(',', '.'),
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out number))
            {
                Console.Write("Нужно число, попробуйте ещё раз: ");
            }
            return number;
        }
    }
}
