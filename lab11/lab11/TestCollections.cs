using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lab11
{
    // Задание 3: сравнение времени поиска в разных коллекциях
    public class TestCollections
    {
        private LinkedList<Student> students = new LinkedList<Student>();
        private LinkedList<string> studentStrings = new LinkedList<string>();
        private Dictionary<Person, Student> personStudents = new Dictionary<Person, Student>();
        private Dictionary<string, Student> stringStudents = new Dictionary<string, Student>();

        private Random random = new Random();
        private string[] surnames = { "Иванов", "Петров", "Сидоров", "Смирнов", "Кузнецов", "Попов", "Волков" };

        public int Count => students.Count;

        public TestCollections(int count)
        {
            for (int i = 0; i < count; i++)
                AddStudent(CreateStudent(i));
        }

        // добавляет студента сразу во все четыре коллекции
        public bool AddStudent(Student student)
        {
            string key = student.ToString();
            if (personStudents.ContainsKey(student.BasePerson) || stringStudents.ContainsKey(key))
                return false;

            students.AddLast(student);
            studentStrings.AddLast(key);
            personStudents.Add(student.BasePerson, student);
            stringStudents.Add(key, student);
            return true;
        }

        // удаляет студента из всех четырех коллекций
        public bool RemoveStudent(Student student)
        {
            bool isRemoved = students.Remove(student);
            studentStrings.Remove(student.ToString());
            personStudents.Remove(student.BasePerson);
            stringStudents.Remove(student.ToString());
            return isRemoved;
        }

        public Student FindStudentByName(string name)
        {
            return students.FirstOrDefault(student => student.Name == name);
        }

        public void PrintCounts()
        {
            Console.WriteLine("LinkedList<Student>: " + students.Count);
            Console.WriteLine("LinkedList<string>: " + studentStrings.Count);
            Console.WriteLine("Dictionary<Person, Student>: " + personStudents.Count);
            Console.WriteLine("Dictionary<string, Student>: " + stringStudents.Count);
        }

        public void RunSearchTest()
        {
            // берем элементы из коллекции и делаем их копии,
            // чтобы искать по содержимому, а не по ссылке
            Student first = (Student)students.First.Value.Clone();
            Student middle = (Student)students.ElementAt(students.Count / 2).Clone();
            Student last = (Student)students.Last.Value.Clone();
            Student missing = new Student("Несуществующий", 99, 5, 1);

            // один раз прогоняем вхолостую
            MeasureAll(first, false);

            Console.WriteLine("Время поиска в тиках");
            MeasureAll(first, true, "Первый элемент");
            MeasureAll(middle, true, "Центральный элемент");
            MeasureAll(last, true, "Последний элемент");
            MeasureAll(missing, true, "Элемент не из коллекции");
        }

        private Student CreateStudent(int index)
        {
            // индекс в фамилии, чтобы все элементы были разные
            string name = surnames[random.Next(surnames.Length)] + index;
            int age = random.Next(17, 26);
            int course = random.Next(1, 5);
            double rating = random.Next(20, 51) / 10.0;
            return new Student(name, age, course, rating);
        }

        private void MeasureAll(Student sample, bool isPrinted, string title = "")
        {
            string sampleString = sample.ToString();
            Person samplePerson = sample.BasePerson;

            bool isFound1 = false;
            bool isFound2 = false;
            bool isFound3 = false;
            bool isFound4 = false;
            bool isFound5 = false;

            Stopwatch stopwatch = Stopwatch.StartNew();
            isFound1 = students.Contains(sample);
            stopwatch.Stop();
            long ticks1 = stopwatch.ElapsedTicks;

            stopwatch.Restart();
            isFound2 = studentStrings.Contains(sampleString);
            stopwatch.Stop();
            long ticks2 = stopwatch.ElapsedTicks;

            stopwatch.Restart();
            isFound3 = personStudents.ContainsKey(samplePerson);
            stopwatch.Stop();
            long ticks3 = stopwatch.ElapsedTicks;

            stopwatch.Restart();
            isFound4 = stringStudents.ContainsKey(sampleString);
            stopwatch.Stop();
            long ticks4 = stopwatch.ElapsedTicks;

            stopwatch.Restart();
            isFound5 = personStudents.ContainsValue(sample);
            stopwatch.Stop();
            long ticks5 = stopwatch.ElapsedTicks;

            if (!isPrinted)
                return;

            Console.WriteLine("\n" + title + ":");
            PrintResult("LinkedList<Student>.Contains", isFound1, ticks1);
            PrintResult("LinkedList<string>.Contains", isFound2, ticks2);
            PrintResult("Dictionary<Person, Student>.ContainsKey", isFound3, ticks3);
            PrintResult("Dictionary<string, Student>.ContainsKey", isFound4, ticks4);
            PrintResult("Dictionary<Person, Student>.ContainsValue", isFound5, ticks5);
        }

        private void PrintResult(string method, bool isFound, long ticks)
        {
            string status = isFound ? "найден" : "не найден";
            Console.WriteLine("  " + method.PadRight(44) + status.PadRight(10) + ticks);
        }
    }
}
