using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab5
{
    public static class Tasks
    {
        // ДВУМЕРНЫЙ МАССИВ
        public static int[,] CreateRandomTable(int rows, int cols)
        {
            if (rows <= 0 || cols <= 0) throw new ArgumentException("Размеры должны быть > 0");
            int[,] result = new int[rows, cols];
            Random rnd = new Random();
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result[i, j] = rnd.Next(1, 10);
            return result;
        }

        public static int[,] AddColumnsToEnd(int[,] matrix, int k)
        {
            if (matrix == null) throw new ArgumentNullException(nameof(matrix));
            if (k <= 0) throw new ArgumentException("K должно быть > 0");

            int rows = matrix.GetLength(0);
            int oldCols = matrix.GetLength(1);
            int[,] newArray = new int[rows, oldCols + k];
            Random rnd = new Random();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < oldCols; j++)
                    newArray[i, j] = matrix[i, j];
                for (int j = oldCols; j < oldCols + k; j++)
                    newArray[i, j] = rnd.Next(1, 10);
            }
            return newArray;
        }

        // РВАНЫЙ МАССИВ
        public static int[][] CreateRandomJagged(int rows)
        {
            if (rows <= 0) throw new ArgumentException("Количество строк должно быть > 0");
            int[][] result = new int[rows][];
            Random rnd = new Random();
            for (int i = 0; i < rows; i++)
            {
                int len = rnd.Next(2, 6);
                result[i] = new int[len];
                for (int j = 0; j < len; j++)
                    result[i][j] = rnd.Next(1, 10);
            }
            return result;
        }

        public static int[][] RemoveFirstRowWithK(int[][] jagged, int k)
        {
            if (jagged == null) throw new ArgumentNullException(nameof(jagged));

            int rowToRemove = -1;
            for (int i = 0; i < jagged.Length; i++)
            {
                for (int j = 0; j < jagged[i].Length; j++)
                {
                    if (jagged[i][j] == k) { rowToRemove = i; break; }
                }
                if (rowToRemove != -1) break;
            }

            if (rowToRemove == -1) return jagged;

            int[][] newJagged = new int[jagged.Length - 1][];
            int newIndex = 0;
            for (int i = 0; i < jagged.Length; i++)
            {
                if (i == rowToRemove) continue;
                newJagged[newIndex++] = jagged[i];
            }
            return newJagged;
        }

        // СТРОКА
        public static string ProcessString(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) throw new InvalidOperationException("Строка пуста.");

            string[] sentences = Regex.Split(text, @"(?<=[.!?])\s*");
            StringBuilder result = new StringBuilder();

            foreach (string sentence in sentences)
            {
                if (string.IsNullOrWhiteSpace(sentence)) continue;

                if (sentence.TrimEnd().EndsWith("!"))
                {
                    char lastChar = sentence[sentence.Length - 1];
                    string body = sentence.Substring(0, sentence.Length - 1).TrimEnd();
                    string[] words = body.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    Array.Reverse(words);

                    string reversedSentence = string.Join(" ", words);

                    if (reversedSentence.Length > 0)
                    {
                        reversedSentence = char.ToUpper(reversedSentence[0]) + reversedSentence.Substring(1).ToLower();
                    }

                    result.Append(reversedSentence + lastChar + " ");
                }
                else
                {
                    result.Append(sentence + " ");
                }
            }
            return result.ToString().TrimEnd();
        }
    }
}