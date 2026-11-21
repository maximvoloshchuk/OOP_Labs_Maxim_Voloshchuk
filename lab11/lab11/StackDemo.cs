using System;
using System.Collections;

namespace Lab11
{
    // Задание 1: необобщенная коллекция Stack
    public class StackDemo
    {
        private Stack people = new Stack();
        private Stack clonedPeople;

        public StackDemo()
        {
            // начальные данные
            people.Push(new Student("Иванов", 18, 1, 4));
            people.Push(new Student("Петров", 19, 2, 5));
            people.Push(new Student("Сидоров", 21, 5, 4.5));
            people.Push(new Person("Семенов", 23));
        }

        public void Run()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("\n--- Задание 1: Stack ---");
                Console.WriteLine("1. Добавить Person");
                Console.WriteLine("2. Добавить Student");
                Console.WriteLine("3. Удалить верхний элемент");
                Console.WriteLine("4. Печать стека (foreach)");
                Console.WriteLine("5. Запросы");
                Console.WriteLine("6. Клонировать стек");
                Console.WriteLine("7. Отсортировать стек");
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
                        CloneStack();
                        break;
                    case 7:
                        SortStack();
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
            people.Push(new Person(name, age));
        }

        private void AddStudent()
        {
            string name = ConsoleInput.ReadString("Фамилия: ");
            int age = ConsoleInput.ReadInt("Возраст: ");
            int course = ConsoleInput.ReadInt("Курс: ");
            double rating = ConsoleInput.ReadDouble("Рейтинг: ");
            people.Push(new Student(name, age, course, rating));
        }

        private void RemovePerson()
        {
            if (people.Count == 0)
            {
                Console.WriteLine("Стек пуст");
                return;
            }
            Person removed = (Person)people.Pop();
            Console.WriteLine("Удалён элемент:");
            removed.Show();
        }

        private void PrintPeople()
        {
            if (people.Count == 0)
            {
                Console.WriteLine("Стек пуст");
                return;
            }
            Console.WriteLine("Элементов в стеке: " + people.Count);
            foreach (Person person in people)
                person.Show();
        }

        private void RunQueries()
        {
            // запрос 1: сколько в стеке студентов
            int studentsCount = 0;
            foreach (Person person in people)
            {
                if (person is Student)
                    studentsCount++;
            }
            Console.WriteLine("Студентов в стеке: " + studentsCount);

            // запрос 2: студенты с рейтингом не ниже заданного
            double minRating = ConsoleInput.ReadDouble("Минимальный рейтинг: ");
            Console.WriteLine("Студенты с рейтингом >= " + minRating + ":");
            foreach (Person person in people)
            {
                Student student = person as Student;
                if (student != null && student.Rating >= minRating)
                    student.Show();
            }

            // запрос 3: печать обычных людей (не студентов)
            Console.WriteLine("Обычные Person:");
            foreach (Person person in people)
            {
                if (person.GetType() == typeof(Person))
                    person.Show();
            }
        }

        private void CloneStack()
        {
            // Clone() у Stack копирует только ссылки, поэтому клонируем каждый элемент
            object[] items = people.ToArray();
            clonedPeople = new Stack();
            // ToArray отдаёт с вершины, поэтому идём с конца, чтобы порядок сохранился
            for (int i = items.Length - 1; i >= 0; i--)
                clonedPeople.Push(((Person)items[i]).Clone());

            Console.WriteLine("Клон стека:");
            foreach (Person person in clonedPeople)
                person.Show();

            // проверяем что клон не зависит от оригинала
            if (clonedPeople.Count > 0)
                clonedPeople.Pop();
            Console.WriteLine("После удаления из клона: в оригинале "
                + people.Count + ", в клоне " + clonedPeople.Count);
        }

        private void SortStack()
        {
            if (people.Count == 0)
            {
                Console.WriteLine("Стек пуст");
                return;
            }
            // у Stack нет Sort, поэтому через массив
            object[] items = people.ToArray();
            Array.Sort(items);
            people.Clear();
            for (int i = items.Length - 1; i >= 0; i--)
                people.Push(items[i]);
            Console.WriteLine("Отсортировано по фамилии (сверху первый по алфавиту)");
            PrintPeople();
        }

        private void FindPerson()
        {
            string name = ConsoleInput.ReadString("Фамилия для поиска: ");
            bool isFound = false;
            foreach (Person person in people)
            {
                if (person.Name == name)
                {
                    person.Show();
                    isFound = true;
                }
            }
            if (!isFound)
                Console.WriteLine("Не найдено");
        }
    }
}
