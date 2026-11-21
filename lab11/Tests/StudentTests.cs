using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab11.Tests
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void EqualsWithSameContentReturnsTrue()
        {
            Student first = new Student("Петров", 19, 2, 5);
            Student second = new Student("Петров", 19, 2, 5);
            Assert.IsTrue(first.Equals(second));
        }

        [TestMethod]
        [DataRow("Другой", 19, 2, 5.0)]
        [DataRow("Петров", 20, 2, 5.0)]
        [DataRow("Петров", 19, 3, 5.0)]
        [DataRow("Петров", 19, 2, 4.0)]
        public void EqualsWithDifferentFieldReturnsFalse(string name, int age, int course, double rating)
        {
            Student original = new Student("Петров", 19, 2, 5);
            Student other = new Student(name, age, course, rating);
            Assert.IsFalse(original.Equals(other));
        }

        [TestMethod]
        public void CloneReturnsEqualStudentButNewObject()
        {
            Student student = new Student("Сидоров", 21, 5, 4.5);
            Student copy = (Student)student.Clone();
            Assert.IsTrue(student.Equals(copy));
            Assert.AreNotSame(student, copy);
        }

        [TestMethod]
        public void ChangingCloneDoesNotChangeOriginal()
        {
            Student student = new Student("Сидоров", 21, 5, 4.5);
            Student copy = (Student)student.Clone();
            copy.Age = 30;
            copy.Rating = 2;
            Assert.AreEqual(21, student.Age);
            Assert.AreEqual(4.5, student.Rating);
        }

        [TestMethod]
        public void BasePersonReturnsPersonWithSameNameAndAge()
        {
            Student student = new Student("Сидоров", 21, 5, 4.5);
            Person basePerson = student.BasePerson;
            Assert.AreEqual(typeof(Person), basePerson.GetType());
            Assert.AreEqual("Сидоров", basePerson.Name);
            Assert.AreEqual(21, basePerson.Age);
        }

        [TestMethod]
        public void ToStringContainsAllFields()
        {
            Student student = new Student("Иванов", 18, 1, 4);
            string text = student.ToString();
            StringAssert.Contains(text, "Иванов");
            StringAssert.Contains(text, "18");
            StringAssert.Contains(text, "курс=1");
            StringAssert.Contains(text, "рейтинг");
        }
    }
}
