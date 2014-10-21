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
    class GroupMSRepositoryTest
    {
        OperationDataContext contextDB;
        GroupMSRepository groupRepository;
        [SetUp]
        public void SetUpTests()
        {

            //set connection string to test database
            ComboDBSettings.connectionStringMS = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=E:\\TasksFromZhorik\\Task3\\Combo\\NUnitTests\\App_data\\MSTestDatabase.mdf;Integrated Security=True";
            
            groupRepository = new GroupMSRepository();
            //context for test db
            contextDB = new OperationDataContext(ComboDBSettings.connectionStringMS);
        }

        [TearDown]
        public void TearDownTests()
        {

            var groups = contextDB.Groups.Where(x => x.GroupName == "KM14TEST");
            
            foreach (Group group in groups)
            { 
                contextDB.Groups.DeleteOnSubmit(group);
                contextDB.SubmitChanges();
            }
        }

        [Test]
        public void GetAllGroupsTest()
        {
            // Arrange
            
            //Act
            int countGroups = groupRepository.GetAllGroups().Count();
            //Assert
            Assert.AreEqual(contextDB.Groups.AsEnumerable().Count(), countGroups);

        }


        [Test]
        public void AddTest()
        {
            // Arrange

            int countBefore = contextDB.Groups.AsEnumerable().Count();
            GroupModel gm = new GroupModel();
            gm.GroupName = "KM14TEST";
            gm.Speciality = "IT-Testing";
            //Act
            groupRepository.Add(gm);
            //Assert
            Assert.AreEqual(countBefore + 1, contextDB.Groups.AsEnumerable().Count());

        }

        [Test]
        public void DeleteTest()
        {
            // Arrange
            Group group = new Group()
            {
                GroupName = "GroupTest",
                Speciality = "TestingDelete"
            };
            contextDB.Groups.InsertOnSubmit(group);
            contextDB.SubmitChanges();
          

            string id_to_delete = contextDB.Groups.AsQueryable<Group>().Where(x => x.GroupName == group.GroupName).
                                  FirstOrDefault().Id.ToString();
            //Act
            groupRepository.Delete(id_to_delete);
            //Assert
            Assert.AreEqual(0,contextDB.Groups.Where(x => x.Id == Convert.ToInt32(id_to_delete)).Count());

        }

        [Test]
        public void GetGroupByIdTest()
        {
            // Arrange
            List<GroupModel> GroupsList = new List<GroupModel>();
            var querym = contextDB.Groups.AsEnumerable().OrderBy(Group => Group.GroupName);
            var Groups = querym.ToList();
            foreach (var GroupData in Groups)
            {
                GroupsList.Add(new GroupModel()
                {
                    Id = GroupData.Id.ToString(),
                    GroupName = GroupData.GroupName,
                    Speciality = GroupData.Speciality
                });
            }

            GroupModel gmTest = GroupsList.Last();
            //Act
            GroupModel gmFounded = groupRepository.GetGroupById(gmTest.Id);
            //Assert
            Assert.AreEqual(gmTest.Id, gmFounded.Id);

        }


    }
}
