using System;

namespace Lab11
{
    public class Person : IComparable, IComparable<Person>, ICloneable
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public Person()
        {
            Name = "Unknown";
            Age = 0;
        }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public virtual void Show()
        {
            Console.WriteLine(ToString());
        }

        public virtual object Clone()
        {
            return new Person(Name, Age);
        }

        // сортировка по фамилии
        public int CompareTo(object obj)
        {
            Person other = obj as Person;
            if (other == null)
                throw new ArgumentException("Объект не является Person");
            return CompareTo(other);
        }

        public int CompareTo(Person other)
        {
            if (other == null)
                return 1;
            return string.Compare(Name, other.Name, StringComparison.Ordinal);
        }

        // сравниваем содержимое, а не ссылки (нужно для Contains и ключей словаря)
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Person other = (Person)obj;
            return Name == other.Name && Age == other.Age;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Age);
        }

        public override string ToString()
        {
            return Name + ", " + Age;
        }
    }
}
