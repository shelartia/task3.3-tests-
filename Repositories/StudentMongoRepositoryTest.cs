using Combo.Models;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTests.Repositories
{
    
    [TestFixture]
    class StudentMongoRepositoryTest
    {
        private MongoDatabase MongoDatabase;
        private MongoCollection GroupsCollection;
        private MongoCollection StudentsCollection;
        private StudentMongoRepository studentRepository;
        [SetUp]
        public void SetUpTests()
        {

            // Get the Mongo Client
            var mongoClient = new MongoClient("server=localhost");

            // Get the Mongo Server from the Cliet Instance
            var server = mongoClient.GetServer();

            // Assign the database to mongoDatabase
            MongoDatabase = server.GetDatabase("MyDBTest");

            // get the Groups collection (table) and assign to GroupsCollection
            GroupsCollection = MongoDatabase.GetCollection("GroupM");
            StudentsCollection = MongoDatabase.GetCollection("StudentM");

            ComboDBSettings.connectionStringMongo = "server=localhost;database=MyDBTest";

            studentRepository = new StudentMongoRepository();

        }

        [Test]
        public void GetAllStudentsTest()
        {
            // Arrange
            
            //Act
            int countStudents = studentRepository.GetAllStudents().Count();
            //Assert
            Assert.AreEqual(StudentsCollection.Count(), countStudents);

        }  

        [Test]
        public void AddTest()
        {
            // Arrange
            
            int countBefore = studentRepository.GetAllStudents().Count();
            StudentModel gm = new StudentModel();
            gm.FirstName = "Jack";
            gm.LastName = "Robbinson";
            //Act
            studentRepository.Add(gm);
            //Assert
            Assert.AreEqual(countBefore + 1, StudentsCollection.Count());

        }


        [Test]
        public void DeleteTest()
        {
            // Arrange
            var Students = StudentsCollection.FindAllAs<StudentModel>();

            string id_to_delete = Students.Last().Id;
            //Act
            studentRepository.Delete(id_to_delete);
            //Assert
            Assert.IsNull((StudentModel)StudentsCollection.FindOneAs(typeof(GroupModel), Query.EQ("_id", id_to_delete)));

        }

        [Test]
        public void GetStudentByIdTest()
        {
            // Arrange
            var Students = StudentsCollection.FindAllAs<StudentModel>();

            StudentModel gmTest = Students.Last();
            //Act
            StudentModel gmFounded = studentRepository.GetStudentById(gmTest.Id);
            //Assert
            Assert.AreEqual(gmTest.LastName, gmFounded.LastName);

       } 
    }
}
