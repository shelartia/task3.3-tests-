using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Combo.Controllers;
using System.Web.Mvc;
using Combo.Models;
using Moq;
using Unity.Mvc4;
using Microsoft.Practices.Unity;


namespace NUnitTests.Controllers
{
    [TestFixture]
    class GroupControllerTest
    {
        private Mock<IGroupsRepository> mockGroupRepository;
        private List<GroupModel> mockGroupModels;
        [SetUp]
        public void SetUpTests()
        {
            mockGroupRepository = new Mock<IGroupsRepository>();
            mockGroupModels = new List<GroupModel>();
            mockGroupModels.Add(new GroupModel() { Id = "0", GroupName = "KM12", Speciality = "InformationTechologies" });
            mockGroupModels.Add(new GroupModel() { Id = "1", GroupName = "KM13", Speciality = "InformationTechologies" });
            mockGroupModels.Add(new GroupModel() { Id = "2", GroupName = "KM14", Speciality = "InformationTechologies" });
                       
        }

        #region Index Action Tests
        [Test, Description("Index calls IGroupsRepository GetAllGroups()") ]
        public void Index_Action_Calls_GetAllGroups()
        {
           // Arrange
           mockGroupRepository.Setup(x => x.GetAllGroups()).Returns(mockGroupModels);
           var controller = new GroupController(mockGroupRepository.Object);
           
            // Act
            var result = controller.Index();

            // Assert            
            mockGroupRepository.Verify(x => x.GetAllGroups(), Times.Once());
            
        }

        [Test]
        public void Index_Action_Returns_ViewResult()
        {
            // Arrange
            mockGroupRepository.Setup(x => x.GetAllGroups()).Returns(mockGroupModels);
            var controller = new GroupController(mockGroupRepository.Object);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(typeof(ViewResult),result);
            
        }

        [Test]
        public void Index_Action_Returns_IndexView()
        {
            // Arrange
            mockGroupRepository.Setup(x => x.GetAllGroups()).Returns(mockGroupModels);
            var controller = new GroupController(mockGroupRepository.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [Test]
        public void Index_Action_Returns_View_With_GroupModelViewModel()
        {
            // Arrange
            mockGroupRepository.Setup(x => x.GetAllGroups()).Returns(mockGroupModels);
            var controller = new GroupController(mockGroupRepository.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsInstanceOfType(typeof(IEnumerable<GroupModel>), result.Model);
        }

        [Test]
        public void Index_Action_Returns_View_With_ViewModel_Containing_Same_Data()
        {
            // Arrange
            mockGroupRepository.Setup(x => x.GetAllGroups()).Returns(mockGroupModels);
            var controller = new GroupController(mockGroupRepository.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;
            var viewModel = result.Model as IEnumerable<GroupModel>;

            // Assert
            Assert.AreSame(mockGroupModels, viewModel);
        }
        #endregion

        #region Create Action Tests
        [Test]
        public void Create_Action_Returns_ViewResult()
        {
            // Arrange
            
            var controller = new GroupController(mockGroupRepository.Object);

            // Act
            var result = controller.Create();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(typeof(ViewResult), result);

        }
       
        [Test]
        public void Create_Action_Returns_CreateView()
        {
            // Arrange
            
            var controller = new GroupController(mockGroupRepository.Object);

            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            
            Assert.AreEqual("Create", result.ViewName);
        }

        [Test]
        public void Create_Post_Action_Returns_ViewResult_When_Invalid()
        {
            //Arrange
            var controller = new GroupController(mockGroupRepository.Object);
            controller.ViewData.ModelState.Clear();
            controller.ModelState.AddModelError("Code", "model is invalid");
            var vm = new GroupModel();

            //Act
            var result = controller.Create(vm);

            //Assert
            Assert.IsInstanceOfType(typeof(ViewResult),result );
        }

        [Test]
        public void Create_Post_Action_Returns_Same_Viewmodel_When_Invalid()
        {
            //Arrange
            mockGroupRepository.Setup(x => x.Add(It.IsAny<GroupModel>()));
            var controller = new GroupController(mockGroupRepository.Object);
            controller.ViewData.ModelState.Clear();
            controller.ModelState.AddModelError("Code", "model is invalid");
            //The field GroupName is required
            var vm = new GroupModel { Id = "999", GroupName = "", Speciality = "test" };

            //Act
            var result = controller.Create(vm) as ViewResult;

            //Assert
            Assert.AreEqual(vm, result.Model);
        }

        [Test]
        public void Create_Post_Action_Calls_Correct_Methods_When_Valid()
        {
            //Arrange
            mockGroupRepository.Setup(x => x.Add(It.IsAny<GroupModel>()));
            var controller = new GroupController(mockGroupRepository.Object);

            controller.ViewData.ModelState.Clear();
            var vm = new GroupModel { Id = "999", GroupName = "KM99", Speciality = "IT" };

            //Act
            controller.Create(vm);

            //Assert
            mockGroupRepository.Verify(x => x.Add(It.IsAny<GroupModel>()), Times.Once());
        }

        [Test]
        public void Create_Post_Action_Returns_RedirectToAction_When_Valid()
        {
            //Arrange
            mockGroupRepository.Setup(x => x.Add(It.IsAny<GroupModel>()));
            var controller = new GroupController(mockGroupRepository.Object);

            controller.ViewData.ModelState.Clear();
            var vm = new GroupModel { Id = "999", GroupName = "KM99", Speciality = "IT" };

            //Act
            var result = controller.Create(vm);

            //Assert
            Assert.IsInstanceOfType(typeof(RedirectToRouteResult), result);
        }

        [Test]
        public void Create_Post_Action_Returns_Index_When_Valid()
        {
            //Arrange
            mockGroupRepository.Setup(x => x.Add(It.IsAny<GroupModel>()));
            var controller = new GroupController(mockGroupRepository.Object);

            controller.ViewData.ModelState.Clear();
            var vm = new GroupModel { Id = "999", GroupName = "KM99", Speciality = "IT" };

            //Act
            var result = controller.Create(vm) as RedirectToRouteResult;
            
            var routeValue = result.RouteValues["action"];

            //Assert
            Assert.AreEqual("Index",routeValue );
        }
 
        #endregion

        #region Delete Action Tests
        [Test]
        public void Delete_Get_Action_Calls_GetGroupById()
        {
            // Arrange
            mockGroupRepository.Setup(x => x.GetGroupById(It.IsAny<string>())).Returns(mockGroupModels[0]);
            var controller = new GroupController(mockGroupRepository.Object);

            // Act
            var result = controller.Delete("1");

            // Assert            
            mockGroupRepository.Verify(x => x.GetGroupById(It.IsAny<string>()), Times.Once());

        }

        [Test]
        public void Delete_Get_Action_Returns_ViewResult_If_Branch_Found()
        {
            // Arrange
            mockGroupRepository.Setup(x => x.GetGroupById(It.IsAny<string>())).Returns(mockGroupModels[0]);
            var controller = new GroupController(mockGroupRepository.Object);

            // Act
            var result = controller.Delete("1");

            // Assert            
            Assert.IsInstanceOfType(typeof(ViewResult), result);

        }

        [Test]
        public void Delete_Get_Action_Returns_DeleteView_If_Branch_Found()
        {
            // Arrange
            mockGroupRepository.Setup(x => x.GetGroupById(It.IsAny<string>())).Returns(mockGroupModels[0]);
            var controller = new GroupController(mockGroupRepository.Object);

            // Act
            var result = controller.Delete("1") as ViewResult;

            // Assert            
            Assert.AreEqual("Delete", result.ViewName);

        }

        [Test]
        public void Delete_Get_Action_Returns_Correct_ViewModel_If_Found()
        {
            // Arrange
            mockGroupRepository.Setup(x => x.GetGroupById(It.IsAny<string>())).Returns(mockGroupModels[0]);
            var controller = new GroupController(mockGroupRepository.Object);
            
            //Act
            var result = controller.Delete("1") as ViewResult;

            //Assert
            Assert.AreEqual(mockGroupModels[0], result.Model);
        }

        [Test]
        public void Delete_Get_Action_Returns_404_If_Branch_Not_Found()
        {
            // Arrange
            mockGroupRepository.Setup(x => x.GetGroupById(It.IsAny<string>())).Returns((GroupModel)null);
            var controller = new GroupController(mockGroupRepository.Object);

            //Act
            var result = controller.Delete("1");

            //Assert
            Assert.IsInstanceOfType(typeof(HttpNotFoundResult),result );
        }

        [Test]
        public void Delete_Post_Action_Calls_BranchService_DeleteBranch()
        {
            // Arrange
            mockGroupRepository.Setup(x => x.Delete(It.IsAny<string>())).Returns(true);
            var controller = new GroupController(mockGroupRepository.Object);

            //Act
            controller.DeleteConfirmed("1");

            //Assert
            mockGroupRepository.Verify(x => x.Delete(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void Delete_Post_Action_Returns_RedirectToAction()
        {
            // Arrange
            mockGroupRepository.Setup(x => x.Delete(It.IsAny<string>())).Returns(true);
            var controller = new GroupController(mockGroupRepository.Object);

            //Act
            var result = controller.DeleteConfirmed("1");

            //Assert
            Assert.IsInstanceOfType(typeof(RedirectToRouteResult),result);
        }

        [Test]
        public void Delete_Post_Action_Returns_RedirectToAction_Index()
        {
            // Arrange
            mockGroupRepository.Setup(x => x.Delete(It.IsAny<string>())).Returns(true);
            var controller = new GroupController(mockGroupRepository.Object);

            //Act
            var result = controller.DeleteConfirmed("1") as RedirectToRouteResult;
            var routeValue = result.RouteValues["action"];

            //Assert
            Assert.AreEqual("Index",routeValue);
        }
        #endregion

    }
}
