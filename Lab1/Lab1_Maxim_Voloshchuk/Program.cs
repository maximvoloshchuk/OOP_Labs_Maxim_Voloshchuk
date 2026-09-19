using System;

namespace Lab1_Maxim_Voloshchuk
{
    class Program
    {
        public static void Main(string[] args)
        {
            Task1();
            Task2();
            Task3();
        }

        private static void Task1()
        {
            Console.WriteLine("Task 1");
            var m = ReadInt("Enter m: ");
            var n = ReadInt("Enter n: ");

            EvaluateFirstExpression(m, n);
            EvaluateSecondExpression(m, n);
            EvaluateThirdExpression(m, n);
            EvaluateFourthExpression();
        }

        private static void EvaluateFirstExpression(int m, int n)
        {
            if (n == 0)
            {
                Console.WriteLine("Expression 1 cannot be computed: division by zero");
                return;
            }
            var result = m / -n++;
            Console.WriteLine($"m / -n++ = {result}, m = {m}, n = {n}");
        }

        private static void EvaluateSecondExpression(int m, int n)
        {
            if (n == 0)
            {
                Console.WriteLine("Expression 2 cannot be computed: division by zero");
                return;
            }
            var result = m / n < n--;
            Console.WriteLine($"m / n < n-- = {result}, m = {m}, n = {n}");
        }

        private static void EvaluateThirdExpression(int m, int n)
        {
            var result = m + n++ > n + m;
            Console.WriteLine($"m + n++ > n + m = {result}, m = {m}, n = {n}");
        }

        private static void EvaluateFourthExpression()
        {
            var x = ReadDouble("\nEnter x: ");
            var xToFifth = Math.Pow(x, 5);
            var result = xToFifth * Math.Sqrt(Math.Abs(x - 1)) + Math.Abs(25 - xToFifth);
            Console.WriteLine($"x^5 * sqrt(|x-1|) + |25 - x^5| = {result}");
        }

        private static void Task2()
        {
            Console.WriteLine("\n---------");
            Console.WriteLine("\nTask 2");
            var x1 = ReadDouble("Enter X1: ");
            var y1 = ReadDouble("Enter Y1: ");

            var isInRegion = x1 >= -7
                && x1 <= 0
                && y1 <= 0
                && y1 >= -x1 / 7 - 1;

            Console.WriteLine($"Point ({x1}, {y1}) belongs to the region: {isInRegion}");
        }

        private static void Task3()
        {
            Console.WriteLine("\n---------");
            Console.WriteLine("\nTask 3");
            const double a = 1000;
            const double b = 0.0001;

            var floatResult = CalculateWithFloat(a, b);
            var doubleResult = CalculateWithDouble(a, b);
            var difference = Math.Abs((double)floatResult - doubleResult);

            Console.WriteLine($"Float result:  {floatResult}");
            Console.WriteLine($"Double result: {doubleResult}");
            Console.WriteLine($"Difference:    {difference}");
        }

        private static float CalculateWithFloat(double a, double b)
        {
            var aFloat = (float)a;
            var bFloat = (float)b;
            var aPlusB = aFloat + bFloat;
            var aCubed = aFloat * aFloat * aFloat;
            var threeASquaredB = 3 * aFloat * aFloat * bFloat;
            var numerator = aPlusB * aPlusB * aPlusB - (aCubed + threeASquaredB);
            var threeABSquared = 3 * aFloat * bFloat * bFloat;
            var bCubed = bFloat * bFloat * bFloat;
            var denominator = threeABSquared + bCubed;
            return numerator / denominator;
        }

        private static double CalculateWithDouble(double a, double b)
        {
            var aPlusB = a + b;
            var aCubed = a * a * a;
            var threeASquaredB = 3 * a * a * b;
            var numerator = aPlusB * aPlusB * aPlusB - (aCubed + threeASquaredB);
            var threeABSquared = 3 * a * b * b;
            var bCubed = b * b * b;
            var denominator = threeABSquared + bCubed;
            return numerator / denominator;
        }

        private static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (int.TryParse(input, out var value))
                {
                    return value;
                }
                Console.WriteLine("Invalid input, please enter an integer");
            }
        }

        private static double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (double.TryParse(input, out var value))
                {
                    return value;
                }
                Console.WriteLine("Invalid input, please enter a number");
            }
        }
    }
}