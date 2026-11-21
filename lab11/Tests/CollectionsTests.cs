using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab11.Tests
{
    // проверяем, что стандартные коллекции работают с нашими классами по содержимому
    [TestClass]
    public class CollectionsTests
    {
        [TestMethod]
        public void StackContainsFindsCopyOfElement()
        {
            Stack stack = new Stack();
            Student student = new Student("Иванов", 18, 1, 4);
            stack.Push(student);
            Assert.IsTrue(stack.Contains(student.Clone()));
        }

        [TestMethod]
        public void StackContainsDoesNotFindMissingElement()
        {
            Stack stack = new Stack();
            stack.Push(new Student("Иванов", 18, 1, 4));
            Assert.IsFalse(stack.Contains(new Student("Другой", 20, 2, 3)));
        }

        [TestMethod]
        public void StackPopReturnsLastPushedElement()
        {
            Stack stack = new Stack();
            stack.Push(new Person("Первый", 20));
            stack.Push(new Person("Второй", 21));
            Person top = (Person)stack.Pop();
            Assert.AreEqual("Второй", top.Name);
            Assert.AreEqual(1, stack.Count);
        }

        [TestMethod]
        public void StackSortThroughArrayOrdersByName()
        {
            Stack stack = new Stack();
            stack.Push(new Person("Сидоров", 20));
            stack.Push(new Person("Иванов", 21));
            stack.Push(new Person("Петров", 22));
            object[] items = stack.ToArray();
            Array.Sort(items);
            Assert.AreEqual("Иванов", ((Person)items[0]).Name);
            Assert.AreEqual("Петров", ((Person)items[1]).Name);
            Assert.AreEqual("Сидоров", ((Person)items[2]).Name);
        }

        [TestMethod]
        public void ListSortOrdersByName()
        {
            List<Person> people = new List<Person>();
            people.Add(new Person("Сидоров", 20));
            people.Add(new Person("Иванов", 21));
            people.Add(new Person("Петров", 22));
            people.Sort();
            Assert.AreEqual("Иванов", people[0].Name);
            Assert.AreEqual("Петров", people[1].Name);
            Assert.AreEqual("Сидоров", people[2].Name);
        }

        [TestMethod]
        public void ListContainsAndRemoveWorkByContent()
        {
            List<Person> people = new List<Person>();
            Student student = new Student("Иванов", 18, 1, 4);
            people.Add(student);
            Assert.IsTrue(people.Contains((Person)student.Clone()));
            Assert.IsTrue(people.Remove((Person)student.Clone()));
            Assert.AreEqual(0, people.Count);
        }

        [TestMethod]
        public void LinkedListContainsFindsCopyButNotOtherStudent()
        {
            LinkedList<Student> students = new LinkedList<Student>();
            Student student = new Student("Иванов", 18, 1, 4);
            students.AddLast(student);
            Assert.IsTrue(students.Contains((Student)student.Clone()));
            Assert.IsFalse(students.Contains(new Student("Другой", 20, 2, 3)));
        }

        [TestMethod]
        public void DictionaryContainsKeyFindsCopyOfPersonKey()
        {
            Dictionary<Person, Student> students = new Dictionary<Person, Student>();
            Student student = new Student("Иванов", 18, 1, 4);
            students.Add(student.BasePerson, student);
            Assert.IsTrue(students.ContainsKey(new Person("Иванов", 18)));
            Assert.IsFalse(students.ContainsKey(new Person("Иванов", 19)));
        }

        [TestMethod]
        public void DictionaryContainsValueFindsCopyOfStudent()
        {
            Dictionary<Person, Student> students = new Dictionary<Person, Student>();
            Student student = new Student("Иванов", 18, 1, 4);
            students.Add(student.BasePerson, student);
            Assert.IsTrue(students.ContainsValue((Student)student.Clone()));
            Assert.IsFalse(students.ContainsValue(new Student("Иванов", 18, 1, 3)));
        }

        [TestMethod]
        public void DictionaryWithStringKeyContainsKeyWorksByToString()
        {
            Dictionary<string, Student> students = new Dictionary<string, Student>();
            Student student = new Student("Иванов", 18, 1, 4);
            students.Add(student.ToString(), student);
            Student copy = (Student)student.Clone();
            Assert.IsTrue(students.ContainsKey(copy.ToString()));
        }
    }
}
