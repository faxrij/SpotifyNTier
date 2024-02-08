using Api.Controllers;
using App.Domain.Entities;
using App.Logic.Commands.AddSong;
using App.Logic.Commands.DeleteSong;
using App.Logic.Commands.UpdateSong;
using App.Logic.Queries.GetSong.GetAllSongs;
using App.Logic.Queries.GetSong.GetSongById;
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
    [TestSubject(typeof(SongController))]
    public class SongControllerTest
    {
        private Mock<IMediator> _mockMediator;
        private SongController _songController;

        [TestInitialize]
        public void SetUp()
        {
            _mockMediator = new Mock<IMediator>();
            _songController = new SongController(_mockMediator.Object);
        }

        [TestMethod]
        public async Task GetAllSongs_ReturnsListOfSongs()
        {
            // Arrange
            var expectedSongs = new List<Song>();
            _mockMediator.Setup(m => m.Send(It.IsAny<GetAllSongsQuery>(), CancellationToken.None))
                .ReturnsAsync(expectedSongs);

            // Act
            var result = await _songController.GetAllSongs();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<ActionResult<List<Song>>>(result);
            Assert.AreEqual(expectedSongs, result.Value);
        }

        [TestMethod]
        public async Task GetSongById_ReturnsSongById()
        {
            // Arrange
            const int id = 1;
            var expectedSong = new Song { Id = id };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetSongByIdQuery>(), CancellationToken.None))
                .ReturnsAsync(expectedSong);

            // Act
            var result = await _songController.GetSongById(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<ActionResult<Song>>(result);
            Assert.AreEqual(expectedSong, result.Value);
        }

        [TestMethod]
        public async Task CreateSong_ReturnsCreatedSong()
        {
            // Arrange
            var addSongCommand = new AddSongCommand();
            var expectedSong = new Song();
            _mockMediator.Setup(m => m.Send(It.IsAny<AddSongCommand>(), CancellationToken.None))
                .ReturnsAsync(expectedSong);

            // Act
            var result = await _songController.CreateSong(addSongCommand);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<ActionResult<Song>>(result);
            Assert.AreEqual(expectedSong, result.Value);
        }

        [TestMethod]
        public async Task RemoveSong_ReturnsTrue()
        {
            // Arrange
            const int id = 1;
            _mockMediator.Setup(m => m.Send(It.IsAny<DeleteSongCommand>(), CancellationToken.None))
                .ReturnsAsync(true);

            // Act
            var result = await _songController.RemoveSong(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<ActionResult<bool>>(result);
            Assert.IsTrue(result.Value);
        }

        [TestMethod]
        public async Task UpdateSong_ReturnsUpdatedSong()
        {
            // Arrange
            var updateSongCommand = new UpdateSongCommand();
            var expectedSong = new Song();
            _mockMediator.Setup(m => m.Send(It.IsAny<UpdateSongCommand>(), CancellationToken.None))
                .ReturnsAsync(expectedSong);

            // Act
            var result = await _songController.UpdateSong(updateSongCommand);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<ActionResult<Song>>(result);
            Assert.AreEqual(expectedSong, result.Value);
        }

        [TestMethod]
        public async Task RemoveSong_WithNonExistentId_ReturnsNotFound()
        {
            // Arrange
            const int nonExistentId = 100;
            _mockMediator.Setup(m => m.Send(It.IsAny<DeleteSongCommand>(), CancellationToken.None))
                .ReturnsAsync(false);

            // Act
            var result = await _songController.RemoveSong(nonExistentId);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}