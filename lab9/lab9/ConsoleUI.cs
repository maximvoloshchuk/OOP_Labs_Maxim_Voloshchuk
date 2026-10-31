using System;

namespace Lab9
{
    public static class ConsoleUI
    {
        public static int ReadInt(string prompt)
        {
            Console.Write(prompt);
            return int.Parse(Console.ReadLine() ?? "");
        }

        public static void PrintMoney(Money money, string label = "")
        {
            Console.Write(label);
            money.Print();
        }

        public static void PrintHeader(string text)
        {
            Console.WriteLine($"\n--- {text} ---");
        }
    }
}