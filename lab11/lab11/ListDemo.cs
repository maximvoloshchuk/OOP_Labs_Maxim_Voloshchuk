using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab11
{
    // Задание 2: обобщенная коллекция List<T>
    public class ListDemo
    {
        private List<Person> people = new List<Person>();
        private List<Person> clonedPeople;

        public ListDemo()
        {
            people.Add(new Student("Иванов", 18, 1, 4));
            people.Add(new Student("Петров", 19, 2, 5));
            people.Add(new Student("Сидоров", 21, 5, 4.5));
            people.Add(new Person("Семенов", 23));
        }

        public void Run()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("\n--- Задание 2: List<T> ---");
                Console.WriteLine("1. Добавить Person");
                Console.WriteLine("2. Добавить Student");
                Console.WriteLine("3. Удалить по индексу");
                Console.WriteLine("4. Печать списка (foreach)");
                Console.WriteLine("5. Запросы");
                Console.WriteLine("6. Клонировать список");
                Console.WriteLine("7. Отсортировать список");
                Console.WriteLine("8. Найти по фамилии");
                Console.WriteLine("0. Назад");
                int choice = ConsoleInput.ReadInt("Выбор: ");
                switch (choice)
                {
                    case 1:
                        AddPerson();
                        break;
                    case 2:
                        AddStudent();
                        break;
                    case 3:
                        RemovePerson();
                        break;
                    case 4:
                        PrintPeople();
                        break;
                    case 5:
                        RunQueries();
                        break;
                    case 6:
                        CloneList();
                        break;
                    case 7:
                        SortList();
                        break;
                    case 8:
                        FindPerson();
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

        private void AddPerson()
        {
            string name = ConsoleInput.ReadString("Фамилия: ");
            int age = ConsoleInput.ReadInt("Возраст: ");
            people.Add(new Person(name, age));
        }

        private void AddStudent()
        {
            string name = ConsoleInput.ReadString("Фамилия: ");
            int age = ConsoleInput.ReadInt("Возраст: ");
            int course = ConsoleInput.ReadInt("Курс: ");
            double rating = ConsoleInput.ReadDouble("Рейтинг: ");
            people.Add(new Student(name, age, course, rating));
        }

        private void RemovePerson()
        {
            if (people.Count == 0)
            {
                Console.WriteLine("Список пуст");
                return;
            }
            int index = ConsoleInput.ReadInt("Индекс (с 0): ");
            if (index < 0 || index >= people.Count)
            {
                Console.WriteLine("Нет элемента с таким индексом");
                return;
            }
            people.RemoveAt(index);
            Console.WriteLine("Удалено");
        }

        private void PrintPeople()
        {
            if (people.Count == 0)
            {
                Console.WriteLine("Список пуст");
                return;
            }
            Console.WriteLine("Элементов в списке: " + people.Count);
            int index = 0;
            foreach (Person person in people)
            {
                Console.Write(index + ") ");
                person.Show();
                index++;
            }
        }

        private void RunQueries()
        {
            // запрос 1: количество студентов
            int studentsCount = people.OfType<Student>().Count();
            Console.WriteLine("Студентов в списке: " + studentsCount);

            // запрос 2: студенты с рейтингом не ниже заданного
            double minRating = ConsoleInput.ReadDouble("Минимальный рейтинг: ");
            IEnumerable<Student> goodStudents = people
                .OfType<Student>()
                .Where(student => student.Rating >= minRating);
            Console.WriteLine("Студенты с рейтингом >= " + minRating + ":");
            foreach (Student student in goodStudents)
                student.Show();

            // запрос 3: печать обычных людей (не студентов)
            Console.WriteLine("Обычные Person:");
            IEnumerable<Person> simplePeople = people
                .Where(item => item.GetType() == typeof(Person));
            foreach (Person person in simplePeople)
                person.Show();
        }

        private void CloneList()
        {
            // копируем каждый элемент, чтобы клон был независимым
            clonedPeople = new List<Person>();
            foreach (Person person in people)
                clonedPeople.Add((Person)person.Clone());

            Console.WriteLine("Клон списка:");
            foreach (Person person in clonedPeople)
                person.Show();

            // проверка что клон не зависит от оригинала
            if (clonedPeople.Count > 0)
                clonedPeople.RemoveAt(0);
            Console.WriteLine("После удаления из клона: в оригинале "
                + people.Count + ", в клоне " + clonedPeople.Count);
        }

        private void SortList()
        {
            people.Sort();
            Console.WriteLine("Отсортировано по фамилии");
            PrintPeople();
        }

        private void FindPerson()
        {
            string name = ConsoleInput.ReadString("Фамилия для поиска: ");
            List<Person> found = people.FindAll(person => person.Name == name);
            if (found.Count == 0)
            {
                Console.WriteLine("Не найдено");
                return;
            }
            foreach (Person person in found)
                person.Show();
        }
    }
}
