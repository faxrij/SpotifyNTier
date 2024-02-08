using Api.Controllers;
using App.Domain.Entities;
using App.Logic.Commands.AddAlbum;
using App.Logic.Commands.DeleteAlbum;
using App.Logic.Commands.UpdateAlbum;
using App.Logic.Queries.GetAlbum.GetAlbumById;
using App.Logic.Queries.GetAlbum.GetAllAlbums;
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
    [TestSubject(typeof(AlbumController))]
    public class AlbumControllerTest
    {
        private Mock<IMediator> _mockMediator;
        private AlbumController _albumController;

        [TestInitialize]
        public void SetUp()
        {
            _mockMediator = new Mock<IMediator>();
            _albumController = new AlbumController(_mockMediator.Object);
        }

        [TestMethod]
        public async Task GetAlbums_ReturnsListOfAlbums()
        {
            // Arrange
            var expectedAlbums = new List<Album>();
            _mockMediator.Setup(m => m.Send(It.IsAny<GetAllAlbumsQuery>(), CancellationToken.None))
                .ReturnsAsync(expectedAlbums);

            // Act
            var result = await _albumController.GetAlbums();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<ActionResult<List<Album>>>(result);
            Assert.AreEqual(expectedAlbums, result.Value);
        }

        [TestMethod]
        public async Task GetAlbum_ReturnsAlbumById()
        {
            // Arrange
            const int id = 1;
            var expectedAlbum = new Album { Id = id };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetAlbumByIdQuery>(), CancellationToken.None))
                .ReturnsAsync(expectedAlbum);

            // Act
            var result = await _albumController.GetAlbum(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<ActionResult<Album>>(result);
            Assert.AreEqual(expectedAlbum, result.Value);
        }

        [TestMethod]
        public async Task CreateAlbum_ReturnsCreatedAlbum()
        {
            // Arrange
            var addAlbumCommand = new AddAlbumCommand();
            var expectedAlbum = new Album();
            _mockMediator.Setup(m => m.Send(It.IsAny<AddAlbumCommand>(), CancellationToken.None))
                .ReturnsAsync(expectedAlbum);

            // Act
            var result = await _albumController.CreateAlbum(addAlbumCommand);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<ActionResult<Album>>(result);
            Assert.AreEqual(expectedAlbum, result.Value);
        }

        [TestMethod]
        public async Task DeleteAlbum_ReturnsTrue()
        {
            // Arrange
            const int id = 1;
            _mockMediator.Setup(m => m.Send(It.IsAny<DeleteAlbumCommand>(), CancellationToken.None))
                .ReturnsAsync(true);

            // Act
            var result = await _albumController.DeleteAlbum(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<ActionResult<bool>>(result);
            Assert.IsTrue(result.Value);
        }

        [TestMethod]
        public async Task UpdateAlbum_ReturnsUpdatedAlbum()
        {
            // Arrange
            var updateAlbumCommand = new UpdateAlbumCommand();
            var expectedAlbum = new Album();
            _mockMediator.Setup(m => m.Send(It.IsAny<UpdateAlbumCommand>(), CancellationToken.None))
                .ReturnsAsync(expectedAlbum);

            // Act
            var result = await _albumController.UpdateAlbum(updateAlbumCommand);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<ActionResult<Album>>(result);
            Assert.AreEqual(expectedAlbum, result.Value);
        }
    }
}
