using Api.Controllers;
using App.Domain.Entities;
using App.Logic.Commands.AddSinger;
using App.Logic.Commands.DeleteSinger;
using App.Logic.Queries.GetSinger.GetAllSingers;
using App.Logic.Queries.GetSinger.GetSingerById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Logic.Commands.UpdateSinger;
using JetBrains.Annotations;

namespace Api.Tests.Controllers
{
    [TestClass]
    [TestSubject(typeof(SingerController))]
    public class SingerControllerTest
    {
        private Mock<IMediator> _mockMediator;
        private SingerController _singerController;

        [TestInitialize]
        public void SetUp()
        {
            _mockMediator = new Mock<IMediator>();
            _singerController = new SingerController(_mockMediator.Object);
        }

        [TestMethod]
        public async Task GetSingers_ReturnsListOfSingers()
        {
            // Arrange
            var expectedSingers = new List<Singer>();
            _mockMediator.Setup(m => m.Send(It.IsAny<GetAllSingersQuery>(), CancellationToken.None))
                .ReturnsAsync(expectedSingers);

            // Act
            var result = await _singerController.GetSingers();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<ActionResult<List<Singer>>>(result);
            Assert.AreEqual(expectedSingers, result.Value);
        }

        [TestMethod]
        public async Task GetSinger_ReturnsSingerById()
        {
            // Arrange
            const int id = 1;
            var expectedSinger = new Singer { Id = id };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetSingerByIdQuery>(), CancellationToken.None))
                .ReturnsAsync(expectedSinger);

            // Act
            var result = await _singerController.GetSinger(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<ActionResult<Singer>>(result);
            Assert.AreEqual(expectedSinger, result.Value);
        }

        [TestMethod]
        public async Task CreateSinger_ReturnsCreatedSinger()
        {
            // Arrange
            var addSingerCommand = new AddSingerCommand();
            var expectedSinger = new Singer();
            _mockMediator.Setup(m => m.Send(It.IsAny<AddSingerCommand>(), CancellationToken.None))
                .ReturnsAsync(expectedSinger);

            // Act
            var result = await _singerController.CreateSinger(addSingerCommand);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<ActionResult<Singer>>(result);
            Assert.AreEqual(expectedSinger, result.Value);
        }

        [TestMethod]
        public async Task DeleteSinger_ReturnsTrue()
        {
            // Arrange
            const int id = 1;
            _mockMediator.Setup(m => m.Send(It.IsAny<DeleteSingerCommand>(), CancellationToken.None))
                .ReturnsAsync(true);

            // Act
            var result = await _singerController.DeleteSinger(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<ActionResult<bool>>(result);
            Assert.IsTrue(result.Value);
        }

        [TestMethod]
        public async Task UpdateSinger_ReturnsUpdatedSinger()
        {
            // Arrange
            var updateSingerCommand = new UpdateSingerCommand();
            var expectedSinger = new Singer();
            _mockMediator.Setup(m => m.Send(It.IsAny<UpdateSingerCommand>(), CancellationToken.None))
                .ReturnsAsync(expectedSinger);

            // Act
            var result = await _singerController.UpdateSinger(updateSingerCommand);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<ActionResult<Singer>>(result);
            Assert.AreEqual(expectedSinger, result.Value);
        }
    }
}
