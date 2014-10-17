using Combo.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTests.Repositories
{

    [TestFixture]
    class IStudentsRepositoryTest
    {
        private Mock<IStudentsRepository> mockStudentRepository;
        private List<StudentModel> mockStudentModels;
        [TestFixtureSetUp]
        public void SetUpTests()
        {
            mockStudentRepository = new Mock<IStudentsRepository>();
            mockStudentModels = new List<StudentModel>();
            mockStudentModels.Add(new StudentModel() { Id = "0", FirstName = "Jerry", LastName = "Salt" });
            mockStudentModels.Add(new StudentModel() { Id = "1", FirstName = "Garry", LastName = "Saltser" });
            mockStudentModels.Add(new StudentModel() { Id = "2", FirstName = "Sally", LastName = "Salty" });
        }
        [TestFixtureTearDown]
        public void TearDownTests()
        {
            mockStudentRepository = null;
            mockStudentModels = null;
        }
        [Test]
        public void GetAllStudentsTest()
        {
            // Arrange:Setup mock behavior for the GetAll() method in our repository

            mockStudentRepository.Setup(x => x.GetAllStudents()).Returns(mockStudentModels);
            //Act
            int countStudents = mockStudentRepository.Object.GetAllStudents().Count();
            //Assert
            Assert.AreEqual(mockStudentModels.Count(), countStudents);

        }

        [Test]
        public void GetStudentByIdTest()
        {
            // Arrange:Setup mock behavior for the GetAll() method in our repository
            StudentModel expectedStudent = mockStudentModels[0];
            Console.WriteLine("expected Id: " + expectedStudent.Id);
            mockStudentRepository.Setup(x => x.GetStudentById("0")).Returns(mockStudentModels[0]);
            //Act
            StudentModel actualStudent = mockStudentRepository.Object.GetStudentById("0");
            //Assert
            
            Console.WriteLine("actual Id: " + actualStudent.Id);
            Assert.AreEqual(mockStudentModels[0].Id, actualStudent.Id);

        }

        [Test]
        public void AddTest()
        {
            // Arrange:Setup mock behavior for the GetAll() method in our repository
            StudentModel newStudent = new StudentModel() { Id = "3", FirstName = "Kenny", LastName = "Ikonov" };
            mockStudentRepository.Setup(x => x.Add(It.IsAny<StudentModel>())).Callback((StudentModel g) => { mockStudentModels.Add(newStudent); });
            mockStudentRepository.Setup(x => x.GetAllStudents()).Returns(mockStudentModels);
            //Act
            mockStudentRepository.Object.Add(newStudent);
            //Assert
            Assert.AreEqual(mockStudentModels.Count(), mockStudentRepository.Object.GetAllStudents().Count());

        }

        [Test]
        public void DeleteTest()
        {
            // Arrange:Setup mock behavior for the GetAll() method in our repository
            mockStudentRepository.Setup(x => x.Delete(It.IsAny<string>())).Returns(true).Callback((string Id) =>
            { mockStudentModels.RemoveAt(0); });
            mockStudentRepository.Setup(x => x.GetAllStudents()).Returns(mockStudentModels);
            //Act
            mockStudentRepository.Object.Delete("0");
            //Assert
            Assert.AreEqual(mockStudentModels.Count(), mockStudentRepository.Object.GetAllStudents().Count());

        }
    }
}
