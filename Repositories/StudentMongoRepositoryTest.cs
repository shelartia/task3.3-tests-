using Combo.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTests.Repositories
{
    /*
    [TestFixture]
    class StudentMongoRepositoryTest
    {
        [Test]
        public void GetAllStudentsTest()
        {
            // Arrange
            StudentMongoRepository StudentRepository;
            StudentRepository = new StudentMongoRepository();
            //Act
            int countStudents = StudentRepository.GetAllStudents().Count();
            //Assert
            Assert.AreEqual(StudentRepository.StudentsCollection.Count(), countStudents);

        }

        [Test]
        public void AddTest()
        {
            // Arrange
            StudentMongoRepository StudentRepository;
            StudentRepository = new StudentMongoRepository();
            int countBefore = StudentRepository.GetAllStudents().Count();
            StudentModel gm = new StudentModel();
            gm.FirstName = "Jack";
            gm.LastName = "Robbinson";
            //Act
            StudentRepository.Add(gm);
            //Assert
            Assert.AreEqual(countBefore + 1, StudentRepository.GetAllStudents().Count());

        }


        [Test]
        public void DeleteTest()
        {
            // Arrange
            StudentMongoRepository StudentRepository;
            StudentRepository = new StudentMongoRepository();
            StudentModel gm = new StudentModel();
            gm.FirstName = "Jack";
            gm.LastName = "Robbinson";
            gm.Id = Guid.NewGuid().ToString();
            StudentRepository.Add(gm);
            int countBefore = StudentRepository.GetAllStudents().Count();
            //Act
            StudentRepository.Delete(gm.Id);
            //Assert
            Assert.AreEqual(countBefore - 1, StudentRepository.GetAllStudents().Count());

        }

        [Test]
        public void GetStudentByIdTest()
        {
            // Arrange
            StudentMongoRepository StudentRepository;
            StudentRepository = new StudentMongoRepository();
            StudentModel gm = new StudentModel();
            gm.FirstName = "Jack";
            gm.LastName = "Robbinson";
            gm.Id = Guid.NewGuid().ToString();
            StudentRepository.Add(gm);
            //Act
            StudentModel gmTest = StudentRepository.GetStudentById(gm.Id);
            //Assert
            Assert.AreEqual(gm.Id, gmTest.Id);

        }
    }*/
}
