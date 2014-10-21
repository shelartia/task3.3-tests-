using Combo.Models;
using Combo;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnitTests.Properties;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace NUnitTests.Repositories
{
   
    [TestFixture]
    class GroupMongoRepositoryTest
    {
        private MongoDatabase MongoDatabase;
        private MongoCollection GroupsCollection;
        private GroupMongoRepository groupRepository;
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
            
            ComboDBSettings.connectionStringMongo = "server=localhost;database=MyDBTest";//Settings.Default.StudentsConnectionString;//

            groupRepository = new GroupMongoRepository();

        }

        
        [Test]
        public void GetAllGroupsTest()
        {
            // Arrange
            
            //Act
            int countGroups = groupRepository.GetAllGroups().Count();
            //Assert
            Assert.AreEqual(GroupsCollection.Count(), countGroups);
            
        } 

        [Test]
        public void AddTest()
        {
            // Arrange
            
            long countBefore = GroupsCollection.Count();
            GroupModel gm = new GroupModel();
            gm.GroupName = "KM14";
            gm.Speciality = "IT";
            //Act
            groupRepository.Add(gm);
            //Assert
            Assert.AreEqual(countBefore + 1, GroupsCollection.Count());
       
        }


        [Test]
        public void DeleteTest()
        {
            // Arrange
            var Groups = GroupsCollection.FindAllAs<GroupModel>();

            string id_to_delete = Groups.Last().Id;
            //Act
            groupRepository.Delete(id_to_delete);
            //Assert
            Assert.IsNull((GroupModel)GroupsCollection.FindOneAs(typeof(GroupModel), Query.EQ("_id", id_to_delete)));

        }

        [Test]
        public void GetGroupByIdTest()
        {
            // Arrange
            var Groups = GroupsCollection.FindAllAs<GroupModel>();

            GroupModel gmTest = Groups.Last();
            //Act
            GroupModel gmFounded = groupRepository.GetGroupById(gmTest.Id);
            //Assert
            Assert.AreEqual(gmTest.GroupName, gmFounded.GroupName);

        }

    }
}
