using Combo.Controllers;
using Combo.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NUnitTests.Controllers
{
    [TestFixture]
    class StudentControllerTest
    {
        private Mock<IStudentsRepository> mockStudentRepository;
        private List<StudentModel> mockStudentModels;
        [SetUp]
        public void SetUpTests()
        {
            mockStudentRepository = new Mock<IStudentsRepository>();
            mockStudentModels = new List<StudentModel>();
            mockStudentModels.Add(new StudentModel() { Id = "0", FirstName = "Jhon", LastName = "Miloshevich", Address="street" });
            mockStudentModels.Add(new StudentModel() { Id = "1", FirstName = "Kate", LastName = "Winslate", Address = "street" });
            mockStudentModels.Add(new StudentModel() { Id = "2", FirstName = "Bob", LastName = "Gamilton", Address = "street" });

        }

        #region Index Action Tests
        [Test, Description("Index calls IStudentsRepository GetAllStudents()")]
        public void Index_Action_Calls_GetAllStudents()
        {
            // Arrange
            mockStudentRepository.Setup(x => x.GetAllStudents()).Returns(mockStudentModels);
            var controller = new StudentController(mockStudentRepository.Object);

            // Act
            var result = controller.Index();

            // Assert            
            mockStudentRepository.Verify(x => x.GetAllStudents(), Times.Once());

        }

        [Test]
        public void Index_Action_Returns_ViewResult()
        {
            // Arrange
            mockStudentRepository.Setup(x => x.GetAllStudents()).Returns(mockStudentModels);
            var controller = new StudentController(mockStudentRepository.Object);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(typeof(ViewResult), result);

        }

        [Test]
        public void Index_Action_Returns_IndexView()
        {
            // Arrange
            mockStudentRepository.Setup(x => x.GetAllStudents()).Returns(mockStudentModels);
            var controller = new StudentController(mockStudentRepository.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [Test]
        public void Index_Action_Returns_View_With_StudentModelViewModel()
        {
            // Arrange
            mockStudentRepository.Setup(x => x.GetAllStudents()).Returns(mockStudentModels);
            var controller = new StudentController(mockStudentRepository.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsInstanceOfType(typeof(IEnumerable<StudentModel>), result.Model);
        }

        [Test]
        public void Index_Action_Returns_View_With_ViewModel_Containing_Same_Data()
        {
            // Arrange
            mockStudentRepository.Setup(x => x.GetAllStudents()).Returns(mockStudentModels);
            var controller = new StudentController(mockStudentRepository.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;
            var viewModel = result.Model as IEnumerable<StudentModel>;

            // Assert
            Assert.AreSame(mockStudentModels, viewModel);
        }
        #endregion

        #region Create Action Tests
        [Test]
        public void Create_Action_Returns_ViewResult()
        {
            // Arrange

            var controller = new StudentController(mockStudentRepository.Object);

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

            var controller = new StudentController(mockStudentRepository.Object);

            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert

            Assert.AreEqual("Create", result.ViewName);
        }

        [Test]
        public void Create_Post_Action_Returns_ViewResult_When_Invalid()
        {
            //Arrange
            var controller = new StudentController(mockStudentRepository.Object);
            controller.ViewData.ModelState.Clear();
            controller.ModelState.AddModelError("Code", "model is invalid");
            var vm = new StudentModel();

            //Act
            var result = controller.Create(vm);

            //Assert
            Assert.IsInstanceOfType(typeof(ViewResult), result);
        }

        [Test]
        public void Create_Post_Action_Returns_Same_Viewmodel_When_Invalid()
        {
            //Arrange
            mockStudentRepository.Setup(x => x.Add(It.IsAny<StudentModel>()));
            var controller = new StudentController(mockStudentRepository.Object);
            controller.ViewData.ModelState.Clear();
            controller.ModelState.AddModelError("Code", "model is invalid");
            //The field FirstName is required
            var vm = new StudentModel { Id = "999", FirstName = "", LastName = "test" };

            //Act
            var result = controller.Create(vm) as ViewResult;

            //Assert
            Assert.AreEqual(vm, result.Model);
        }

        [Test]
        public void Create_Post_Action_Calls_Correct_Methods_When_Valid()
        {
            //Arrange
            mockStudentRepository.Setup(x => x.Add(It.IsAny<StudentModel>()));
            var controller = new StudentController(mockStudentRepository.Object);

            controller.ViewData.ModelState.Clear();
            var vm = new StudentModel { Id = "999", FirstName = "Larry", LastName = "King" };

            //Act
            controller.Create(vm);

            //Assert
            mockStudentRepository.Verify(x => x.Add(It.IsAny<StudentModel>()), Times.Once());
        }

        [Test]
        public void Create_Post_Action_Returns_RedirectToAction_When_Valid()
        {
            //Arrange
            mockStudentRepository.Setup(x => x.Add(It.IsAny<StudentModel>()));
            var controller = new StudentController(mockStudentRepository.Object);

            controller.ViewData.ModelState.Clear();
            var vm = new StudentModel { Id = "999", FirstName = "Larry", LastName = "King" };

            //Act
            var result = controller.Create(vm);

            //Assert
            Assert.IsInstanceOfType(typeof(RedirectToRouteResult), result);
        }

        [Test]
        public void Create_Post_Action_Returns_Index_When_Valid()
        {
            //Arrange
            mockStudentRepository.Setup(x => x.Add(It.IsAny<StudentModel>()));
            var controller = new StudentController(mockStudentRepository.Object);

            controller.ViewData.ModelState.Clear();
            var vm = new StudentModel { Id = "999", FirstName = "Larry", LastName = "King" };

            //Act
            var result = controller.Create(vm) as RedirectToRouteResult;

            var routeValue = result.RouteValues["action"];

            //Assert
            Assert.AreEqual("Index", routeValue);
        }

        #endregion

        #region Delete Action Tests
        [Test]
        public void Delete_Get_Action_Calls_GetStudentById()
        {
            // Arrange
            mockStudentRepository.Setup(x => x.GetStudentById(It.IsAny<string>())).Returns(mockStudentModels[0]);
            var controller = new StudentController(mockStudentRepository.Object);

            // Act
            var result = controller.Delete("1");

            // Assert            
            mockStudentRepository.Verify(x => x.GetStudentById(It.IsAny<string>()), Times.Once());

        }

        [Test]
        public void Delete_Get_Action_Returns_ViewResult_If_Branch_Found()
        {
            // Arrange
            mockStudentRepository.Setup(x => x.GetStudentById(It.IsAny<string>())).Returns(mockStudentModels[0]);
            var controller = new StudentController(mockStudentRepository.Object);

            // Act
            var result = controller.Delete("1");

            // Assert            
            Assert.IsInstanceOfType(typeof(ViewResult), result);

        }

        [Test]
        public void Delete_Get_Action_Returns_DeleteView_If_Branch_Found()
        {
            // Arrange
            mockStudentRepository.Setup(x => x.GetStudentById(It.IsAny<string>())).Returns(mockStudentModels[0]);
            var controller = new StudentController(mockStudentRepository.Object);

            // Act
            var result = controller.Delete("1") as ViewResult;

            // Assert            
            Assert.AreEqual("Delete", result.ViewName);

        }

        [Test]
        public void Delete_Get_Action_Returns_Correct_ViewModel_If_Found()
        {
            // Arrange
            mockStudentRepository.Setup(x => x.GetStudentById(It.IsAny<string>())).Returns(mockStudentModels[0]);
            var controller = new StudentController(mockStudentRepository.Object);

            //Act
            var result = controller.Delete("1") as ViewResult;

            //Assert
            Assert.AreEqual(mockStudentModels[0], result.Model);
        }

        [Test]
        public void Delete_Get_Action_Returns_404_If_Branch_Not_Found()
        {
            // Arrange
            mockStudentRepository.Setup(x => x.GetStudentById(It.IsAny<string>())).Returns((StudentModel)null);
            var controller = new StudentController(mockStudentRepository.Object);

            //Act
            var result = controller.Delete("1");

            //Assert
            Assert.IsInstanceOfType(typeof(HttpNotFoundResult), result);
        }

        [Test]
        public void Delete_Post_Action_Calls_BranchService_DeleteBranch()
        {
            // Arrange
            mockStudentRepository.Setup(x => x.Delete(It.IsAny<string>())).Returns(true);
            var controller = new StudentController(mockStudentRepository.Object);

            //Act
            controller.DeleteConfirmed("1");

            //Assert
            mockStudentRepository.Verify(x => x.Delete(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void Delete_Post_Action_Returns_RedirectToAction()
        {
            // Arrange
            mockStudentRepository.Setup(x => x.Delete(It.IsAny<string>())).Returns(true);
            var controller = new StudentController(mockStudentRepository.Object);

            //Act
            var result = controller.DeleteConfirmed("1");

            //Assert
            Assert.IsInstanceOfType(typeof(RedirectToRouteResult), result);
        }

        [Test]
        public void Delete_Post_Action_Returns_RedirectToAction_Index()
        {
            // Arrange
            mockStudentRepository.Setup(x => x.Delete(It.IsAny<string>())).Returns(true);
            var controller = new StudentController(mockStudentRepository.Object);

            //Act
            var result = controller.DeleteConfirmed("1") as RedirectToRouteResult;
            var routeValue = result.RouteValues["action"];

            //Assert
            Assert.AreEqual("Index", routeValue);
        }
        #endregion
    }
}
