using Combo.Models;
using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTests.Repositories
{
    /*
    [TestFixture]
    class IGroupsRepositoryTest
    {
        private Mock<IGroupsRepository> mockGroupRepository;
        private List<GroupModel> mockGroupModels;
        [TestFixtureSetUp]
        public void SetUpTests()
        {
            mockGroupRepository = new Mock<IGroupsRepository>();
            mockGroupModels = new List<GroupModel>();
            mockGroupModels.Add(new GroupModel() { Id = "0", GroupName = "KM12", Speciality = "Salt Water1" });
            mockGroupModels.Add(new GroupModel() { Id = "1", GroupName = "KM13", Speciality = "Salt Water2" });
            mockGroupModels.Add(new GroupModel() { Id = "2", GroupName = "KM14", Speciality = "Salt Water3" });

        }
        [Test]
        public void GetAllGroupsTest()
        {
            // Arrange:Setup mock behavior for the GetAll() method in our repository
            
            mockGroupRepository.Setup(x => x.GetAllGroups()).Returns(mockGroupModels);
            //Act
            int countGroups = mockGroupRepository.Object.GetAllGroups().Count();
            //Assert
            Assert.AreEqual(mockGroupModels.Count(), countGroups);

        }

        [Test]
        public void GetGroupByIdTest()
        {
            // Arrange:Setup mock behavior for the GetAll() method in our repository
            GroupModel expectedGroup = mockGroupModels.Find(group => group.Id == "1");
            mockGroupRepository.Setup(x => x.GetGroupById("1")).Returns(mockGroupModels.Find(group => group.Id == "1"));
            //Act
            GroupModel actualGroup = mockGroupRepository.Object.GetGroupById("1");
            //Assert
            Assert.AreEqual(expectedGroup.Id, actualGroup.Id);

        }

        [Test]
        public void AddTest()
        {
            // Arrange:Setup mock behavior for the GetAll() method in our repository
            GroupModel newGroup = new GroupModel() { Id = "3", GroupName = "KM15", Speciality = "IT" };
            mockGroupRepository.Setup(x => x.Add(It.IsAny<GroupModel>())).Callback((GroupModel  g)=>{ mockGroupModels.Add(newGroup);});
            mockGroupRepository.Setup(x => x.GetAllGroups()).Returns(mockGroupModels);
            //Act
            mockGroupRepository.Object.Add(newGroup);
            //Assert
            Assert.AreEqual(mockGroupModels.Count(), mockGroupRepository.Object.GetAllGroups().Count());

        }

        [Test]
        public void DeleteTest()
        {
            // Arrange:Setup mock behavior for the GetAll() method in our repository
            mockGroupRepository.Setup(x => x.Delete(It.IsAny<string>())).Returns(true).Callback((string Id) =>
                                       { mockGroupModels.RemoveAt(0); });
            mockGroupRepository.Setup(x => x.GetAllGroups()).Returns(mockGroupModels);
            //Act
            mockGroupRepository.Object.Delete("0");
            //Assert
            Assert.AreEqual(mockGroupModels.Count(), mockGroupRepository.Object.GetAllGroups().Count());

        }
    }*/
}
