using Api.Controllers;
using App.Domain.Entities;
using App.Logic.Commands.AddCategory;
using App.Logic.Commands.DeleteCategory;
using App.Logic.Commands.UpdateCategory;
using App.Logic.Queries.GetCategory.GetAllCategories;
using App.Logic.Queries.GetCategory.GetCategoryById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Api.Tests.Controllers
{
    [TestClass]
    [TestSubject(typeof(CategoryController))]
    public class CategoryControllerTest
    {
        private Mock<IMediator> _mockMediator;
        private CategoryController _categoryController;

        [TestInitialize]
        public void SetUp()
        {
            _mockMediator = new Mock<IMediator>();
            _categoryController = new CategoryController(_mockMediator.Object);
        }

        [TestMethod]
        public async Task GetCategories_ReturnsListOfCategories()
        {
            // Arrange
            var expectedCategories = new List<Category>();
            _mockMediator.Setup(m => m.Send(It.IsAny<GetAllCategoriesQuery>(), CancellationToken.None))
                .ReturnsAsync(expectedCategories);

            // Act
            var result = await _categoryController.GetCategories();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<ActionResult<List<Category>>>(result);
            Assert.AreEqual(expectedCategories, result.Value);
        }

        [TestMethod]
        public async Task GetCategory_ReturnsCategoryById()
        {
            // Arrange
            const int id = 1;
            var expectedCategory = new Category { Id = id };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetCategoryByIdQuery>(), CancellationToken.None))
                .ReturnsAsync(expectedCategory);

            // Act
            var result = await _categoryController.GetCategory(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<ActionResult<Category>>(result);
            Assert.AreEqual(expectedCategory, result.Value);
        }

        [TestMethod]
        public async Task CreateCategory_ReturnsCreatedCategory()
        {
            // Arrange
            var addCategoryCommand = new AddCategoryCommand();
            var expectedCategory = new Category();
            _mockMediator.Setup(m => m.Send(It.IsAny<AddCategoryCommand>(), CancellationToken.None))
                .ReturnsAsync(expectedCategory);

            // Act
            var result = await _categoryController.CreateCategory(addCategoryCommand);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<ActionResult<Category>>(result);
            Assert.AreEqual(expectedCategory, result.Value);
        }

        [TestMethod]
        public async Task DeleteCategory_ReturnsTrue()
        {
            // Arrange
            const int id = 1;
            _mockMediator.Setup(m => m.Send(It.IsAny<DeleteCategoryCommand>(), CancellationToken.None))
                .ReturnsAsync(true);

            // Act
            var result = await _categoryController.DeleteCategory(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<ActionResult<bool>>(result);
            Assert.IsTrue(result.Value);
        }

        [TestMethod]
        public async Task UpdateCategory_ReturnsUpdatedCategory()
        {
            // Arrange
            var updateCategoryCommand = new UpdateCategoryCommand();
            var expectedCategory = new Category();
            _mockMediator.Setup(m => m.Send(It.IsAny<UpdateCategoryCommand>(), CancellationToken.None))
                .ReturnsAsync(expectedCategory);

            // Act
            var result = await _categoryController.UpdateCategory(updateCategoryCommand);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<ActionResult<Category>>(result);
            Assert.AreEqual(expectedCategory, result.Value);
        }
    }
}
