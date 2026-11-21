using System;

namespace Lab11
{
    public class Student : Person
    {
        public int Course { get; set; }

        public double Rating { get; set; }

        // возвращает подобъект базового класса
        public Person BasePerson => new Person(Name, Age);

        public Student() : base()
        {
            Course = 1;
            Rating = 0;
        }

        public Student(string name, int age, int course, double rating) : base(name, age)
        {
            Course = course;
            Rating = rating;
        }

        public override object Clone()
        {
            return new Student(Name, Age, Course, Rating);
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj))
                return false;
            Student other = (Student)obj;
            return Course == other.Course && Rating == other.Rating;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Age, Course, Rating);
        }

        public override string ToString()
        {
            return base.ToString() + ", курс=" + Course + ", рейтинг=" + Rating;
        }
    }
}
