using System;

namespace Lab3
{
    class Program
    {
        public static void Main(string[] args)
        {
            double a = 0.1;
            double b = 0.8;
            int k = 9;
            int n = 40;
            double eps = 0.0001;
            double pi = Math.PI;
            double sinPi4 = Math.Sin(pi / 4);
            double cosPi4 = Math.Cos(pi / 4);
            double step = (b - a) / k;

            Console.WriteLine("|-----------------------------------------------------------------------------------|");
            Console.WriteLine($"|{"X",6} {"|",5} {"SN",12} {"|",10} {"SE",12} {"|",10} {"Y",12} {"|",10}");
            Console.WriteLine("|-----------------------------------------------------------------------------------|");

            // Вычисление значения суммы для заданного n
            for (int i = 0; i <= k; i++)
            {
                double x = a + i * step;

                double SN = 0;
                for(int j = 1; j <= n; j++)
                {
                    SN += Math.Pow(x, j) * Math.Sin(j * pi/4);
                }

                // Вычисление значения суммы для заданной точности epsilon
                double SE = 0;
                int m = 1;
                double pow_x = x;
                while (pow_x >= eps)
                {
                    SE += pow_x * Math.Sin(m * pi / 4);
                    pow_x *= x;
                    m++;
                }

                // Вычисление точного значения Y
                double Y = (x * sinPi4) / (1 - 2 * x * cosPi4 + Math.Pow(x, 2));

                Console.WriteLine($"|{x,8:F4}   |   {SN,15:F15}   |   {SE,15:F15}   |   {Y,15:F15}   |");
                Console.WriteLine("|-----------------------------------------------------------------------------------|");
            }

        }
    }
}