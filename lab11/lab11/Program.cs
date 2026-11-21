using System;
using System.Text;

namespace Lab11
{
    public class Program
    {
        private static TestCollections testCollections;

        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            StackDemo stackDemo = new StackDemo();
            ListDemo listDemo = new ListDemo();

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("\n=== Практическая работа 11. Коллекции ===");
                Console.WriteLine("1. Задание 1 (Stack)");
                Console.WriteLine("2. Задание 2 (List<T>)");
                Console.WriteLine("3. Задание 3 (LinkedList<T>, Dictionary<K,T>)");
                Console.WriteLine("0. Выход");
                int choice = ConsoleInput.ReadInt("Выбор: ");
                switch (choice)
                {
                    case 1:
                        stackDemo.Run();
                        break;
                    case 2:
                        listDemo.Run();
                        break;
                    case 3:
                        RunTestCollections();
                        break;
                    case 0:
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Нет такого пункта");
                        break;
                }
            }
        }

        private static void RunTestCollections()
        {
            // коллекции на 1000 элементов создаем один раз при входе в задание
            if (testCollections == null)
                testCollections = new TestCollections(1000);

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("\n--- Задание 3: TestCollections ---");
                Console.WriteLine("1. Количество элементов в коллекциях");
                Console.WriteLine("2. Добавить студента");
                Console.WriteLine("3. Удалить студента по фамилии");
                Console.WriteLine("4. Замерить время поиска");
                Console.WriteLine("0. Назад");
                int choice = ConsoleInput.ReadInt("Выбор: ");
                switch (choice)
                {
                    case 1:
                        testCollections.PrintCounts();
                        break;
                    case 2:
                        AddStudent();
                        break;
                    case 3:
                        RemoveStudent();
                        break;
                    case 4:
                        testCollections.RunSearchTest();
                        break;
                    case 0:
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Нет такого пункта");
                        break;
                }
            }
        }

        private static void AddStudent()
        {
            string name = ConsoleInput.ReadString("Фамилия: ");
            int age = ConsoleInput.ReadInt("Возраст: ");
            int course = ConsoleInput.ReadInt("Курс: ");
            double rating = ConsoleInput.ReadDouble("Рейтинг: ");
            Student student = new Student(name, age, course, rating);
            if (testCollections.AddStudent(student))
                Console.WriteLine("Добавлен");
            else
                Console.WriteLine("Такой студент уже есть");
        }

        private static void RemoveStudent()
        {
            string name = ConsoleInput.ReadString("Фамилия: ");
            Student student = testCollections.FindStudentByName(name);
            if (student == null)
            {
                Console.WriteLine("Студент не найден");
                return;
            }
            testCollections.RemoveStudent(student);
            Console.WriteLine("Удалён: " + student);
        }
    }
}
