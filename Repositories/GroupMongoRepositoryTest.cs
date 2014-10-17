using Combo.Models;
using Combo;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnitTests.Properties;

namespace NUnitTests.Repositories
{
    /*
    [TestFixture]
    class GroupMongoRepositoryTest
    {
        
        [Test]
        public void GetAllGroupsTest()
        {
            // Arrange
            GroupMongoRepository groupRepository;
            groupRepository = new GroupMongoRepository();
            //Act
            int countGroups = groupRepository.GetAllGroups().Count();
            //Assert
            Assert.AreEqual(groupRepository.GroupsCollection.Count(), countGroups);
            
        }

        [Test]
        public void AddTest()
        {
            // Arrange
            GroupMongoRepository groupRepository;
            groupRepository = new GroupMongoRepository();
            int countBefore = groupRepository.GetAllGroups().Count();
            GroupModel gm = new GroupModel();
            gm.GroupName = "KM14";
            gm.Speciality = "IT";
            //Act
            groupRepository.Add(gm);
            //Assert
            Assert.AreEqual(countBefore+1, groupRepository.GetAllGroups().Count());
       
        }


        [Test]
        public void DeleteTest()
        {
            // Arrange
            GroupMongoRepository groupRepository;
            groupRepository = new GroupMongoRepository();
            GroupModel gm = new GroupModel();
            gm.GroupName = "KM14";
            gm.Speciality = "IT";
            gm.Id = Guid.NewGuid().ToString();
            groupRepository.Add(gm);
            int countBefore = groupRepository.GetAllGroups().Count();
            //Act
            groupRepository.Delete(gm.Id);
            //Assert
            Assert.AreEqual(countBefore - 1, groupRepository.GetAllGroups().Count());

        }

        [Test]
        public void GetGroupByIdTest()
        {
            // Arrange
            GroupMongoRepository groupRepository;
            groupRepository = new GroupMongoRepository();
            GroupModel gm = new GroupModel();
            gm.GroupName = "KM14";
            gm.Speciality = "IT";
            gm.Id = Guid.NewGuid().ToString();
            groupRepository.Add(gm);
            //Act
            GroupModel gmTest = groupRepository.GetGroupById(gm.Id);
            //Assert
            Assert.AreEqual(gm.Id, gmTest.Id);

        }

    }*/
}
