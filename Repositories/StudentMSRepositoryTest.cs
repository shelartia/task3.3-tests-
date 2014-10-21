using Combo.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTests.Repositories
{
    [TestFixture]
    class StudentMSRepositoryTest
    {
        OperationDataContext contextDB;
        StudentMSRepository studentRepository;
        [SetUp]
        public void SetUpTests()
        {

            //set connection string to test database
            ComboDBSettings.connectionStringMS = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=E:\\TasksFromZhorik\\Task3\\Combo\\NUnitTests\\App_data\\MSTestDatabase.mdf;Integrated Security=True";

            studentRepository = new StudentMSRepository();
            //context for test db
            contextDB = new OperationDataContext(ComboDBSettings.connectionStringMS);
        }

        [TearDown]
        public void TearDownTests()
        {

            var students = contextDB.Students.Where(x => x.LastName == "Doe");

            foreach (Student student in students)
            {
                contextDB.Students.DeleteOnSubmit(student);
                contextDB.SubmitChanges();
            }
        }

        [Test]
        public void GetAllStudentsTest()
        {
            // Arrange

            //Act
            int countStudents = studentRepository.GetAllStudents().Count();
            //Assert
            Assert.AreEqual(contextDB.Students.AsEnumerable().Count(), countStudents);

        }

        [Test]
        public void AddTest()
        {
            // Arrange

            int countBefore = contextDB.Students.AsEnumerable().Count();
            StudentModel sm = new StudentModel();
            sm.FirstName = "John";
            sm.LastName = "Doe";
            sm.Address = "Address";
            //Act
            studentRepository.Add(sm);
            //Assert
            Assert.AreEqual(countBefore + 1, contextDB.Students.AsEnumerable().Count());

        }

        [Test]
        public void DeleteTest()
        {
            // Arrange
            Student student = new Student()
            {
                FirstName = "John",
                LastName = "Doe"
            };
            contextDB.Students.InsertOnSubmit(student);
            contextDB.SubmitChanges();


            string id_to_delete = contextDB.Students.AsQueryable<Student>().Where(x => x.LastName == student.LastName).
                                  FirstOrDefault().Id.ToString();
            //Act
            studentRepository.Delete(id_to_delete);
            //Assert
            Assert.AreEqual(0, contextDB.Students.Where(x => x.Id == Convert.ToInt32(id_to_delete)).Count());

        }

        [Test]
        public void GetStudentByIdTest()
        {
            // Arrange
            List<StudentModel> StudentsList = new List<StudentModel>();
            var querym = contextDB.Students.AsEnumerable().OrderBy(s => s.LastName);
            var Students = querym.ToList();
            foreach (var StudentData in Students)
            {
                StudentsList.Add(new StudentModel()
                {
                    Id = StudentData.Id.ToString(),
                    FirstName = StudentData.FirstName,
                    LastName = StudentData.LastName,
                    Address = StudentData.Address
                });
            }

            StudentModel gmTest = StudentsList.Last();
            //Act
            StudentModel gmFounded = studentRepository.GetStudentById(gmTest.Id);
            //Assert
            Assert.AreEqual(gmTest.Id, gmFounded.Id);

        }
    }
}
