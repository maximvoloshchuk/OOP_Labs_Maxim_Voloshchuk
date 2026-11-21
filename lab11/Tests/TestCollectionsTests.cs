using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab11.Tests
{
    // тесты на класс TestCollections из задания 3
    [TestClass]
    public class TestCollectionsTests
    {
        [TestMethod]
        [DataRow(1)]
        [DataRow(50)]
        [DataRow(1000)]
        public void ConstructorCreatesGivenNumberOfElements(int count)
        {
            TestCollections tests = new TestCollections(count);
            Assert.AreEqual(count, tests.Count);
        }

        [TestMethod]
        public void AddStudentAddsNewStudent()
        {
            TestCollections tests = new TestCollections(10);
            bool isAdded = tests.AddStudent(new Student("Тестов", 20, 2, 4));
            Assert.IsTrue(isAdded);
            Assert.AreEqual(11, tests.Count);
        }

        [TestMethod]
        public void AddStudentDoesNotAddDuplicate()
        {
            TestCollections tests = new TestCollections(10);
            Student student = new Student("Тестов", 20, 2, 4);
            tests.AddStudent(student);
            bool isAdded = tests.AddStudent((Student)student.Clone());
            Assert.IsFalse(isAdded);
            Assert.AreEqual(11, tests.Count);
        }

        [TestMethod]
        public void RemoveStudentRemovesByCopy()
        {
            TestCollections tests = new TestCollections(10);
            Student student = new Student("Тестов", 20, 2, 4);
            tests.AddStudent(student);
            bool isRemoved = tests.RemoveStudent((Student)student.Clone());
            Assert.IsTrue(isRemoved);
            Assert.AreEqual(10, tests.Count);
        }

        [TestMethod]
        public void RemoveStudentDoesNotRemoveMissingStudent()
        {
            TestCollections tests = new TestCollections(10);
            bool isRemoved = tests.RemoveStudent(new Student("Несуществующий", 99, 5, 1));
            Assert.IsFalse(isRemoved);
            Assert.AreEqual(10, tests.Count);
        }

        [TestMethod]
        public void StudentCanBeAddedAgainAfterRemoving()
        {
            TestCollections tests = new TestCollections(10);
            Student student = new Student("Тестов", 20, 2, 4);
            tests.AddStudent(student);
            tests.RemoveStudent(student);
            Assert.IsTrue(tests.AddStudent(student));
        }

        [TestMethod]
        public void FindStudentByNameFindsAddedStudent()
        {
            TestCollections tests = new TestCollections(10);
            tests.AddStudent(new Student("Тестов", 20, 2, 4));
            Student found = tests.FindStudentByName("Тестов");
            Assert.IsNotNull(found);
            Assert.AreEqual("Тестов", found.Name);
        }

        [TestMethod]
        public void FindStudentByNameReturnsNullForMissingName()
        {
            TestCollections tests = new TestCollections(10);
            Assert.IsNull(tests.FindStudentByName("Нетакого"));
        }

        [TestMethod]
        public void RunSearchTestPrintsResultsForAllFourElements()
        {
            TestCollections tests = new TestCollections(1000);
            TextWriter oldOutput = Console.Out;
            StringWriter writer = new StringWriter();
            try
            {
                Console.SetOut(writer);
                tests.RunSearchTest();
            }
            finally
            {
                Console.SetOut(oldOutput);
            }
            string output = writer.ToString();
            StringAssert.Contains(output, "Первый элемент");
            StringAssert.Contains(output, "Центральный элемент");
            StringAssert.Contains(output, "Последний элемент");
            StringAssert.Contains(output, "Элемент не из коллекции");
            StringAssert.Contains(output, "не найден");
        }
    }
}
