using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab11.Tests
{
    [TestClass]
    public class PersonTests
    {
        [TestMethod]
        public void EqualsWithSameContentReturnsTrue()
        {
            Person first = new Person("Иванов", 18);
            Person second = new Person("Иванов", 18);
            Assert.IsTrue(first.Equals(second));
        }

        [TestMethod]
        public void EqualsWithDifferentAgeReturnsFalse()
        {
            Person first = new Person("Иванов", 18);
            Person second = new Person("Иванов", 19);
            Assert.IsFalse(first.Equals(second));
        }

        [TestMethod]
        public void EqualsWithNullReturnsFalse()
        {
            Person person = new Person("Иванов", 18);
            Assert.IsFalse(person.Equals(null));
        }

        [TestMethod]
        public void EqualsPersonAndStudentWithSameNameAndAgeReturnsFalse()
        {
            Person person = new Person("Иванов", 18);
            Student student = new Student("Иванов", 18, 1, 4);
            Assert.IsFalse(person.Equals(student));
        }

        [TestMethod]
        public void GetHashCodeForEqualObjectsIsSame()
        {
            Person first = new Person("Петров", 19);
            Person second = new Person("Петров", 19);
            Assert.AreEqual(first.GetHashCode(), second.GetHashCode());
        }

        [TestMethod]
        public void CompareToOrdersByName()
        {
            Person first = new Person("Андреев", 20);
            Person second = new Person("Борисов", 20);
            Assert.IsTrue(first.CompareTo(second) < 0);
            Assert.IsTrue(second.CompareTo(first) > 0);
            Assert.AreEqual(0, first.CompareTo(new Person("Андреев", 30)));
        }

        [TestMethod]
        public void CompareToNullReturnsPositive()
        {
            Person person = new Person("Андреев", 20);
            Assert.IsTrue(person.CompareTo((Person)null) > 0);
        }

        [TestMethod]
        public void CompareToObjectOfOtherTypeThrowsException()
        {
            Person person = new Person("Андреев", 20);
            bool isThrown = false;
            try
            {
                person.CompareTo("строка");
            }
            catch (ArgumentException)
            {
                isThrown = true;
            }
            Assert.IsTrue(isThrown);
        }

        [TestMethod]
        public void CloneReturnsEqualPerson()
        {
            Person person = new Person("Иванов", 18);
            Person copy = (Person)person.Clone();
            Assert.IsTrue(person.Equals(copy));
            Assert.AreNotSame(person, copy);
        }
    }
}
