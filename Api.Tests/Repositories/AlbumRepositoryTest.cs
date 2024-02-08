using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Entities;
using App.Infrastructure.Contexts;
using App.Infrastructure.Repositories;
using App.Logic.Commands.AddAlbum;
using App.Logic.Commands.UpdateAlbum;
using App.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Api.Tests.Repositories
{
    [TestClass]
    public class AlbumRepositoryTests
    {
        [TestMethod]
        public async Task GetAllAlbumsAsync_ReturnsListOfAlbums()
        {
            // Arrange
            var albums = new List<Album> { new Album(), new Album() };
            var mockRepository = new Mock<IAlbumRepository>();
            mockRepository.Setup(repo => repo.GetAllAlbumsAsync())
                .ReturnsAsync(albums);
            var repository = mockRepository.Object;
    
            // Act
            var result = await repository.GetAllAlbumsAsync();

            // Assert
            Assert.AreEqual(albums.Count, result.Count);
        }

        [TestMethod]
        public async Task RemoveAlbumAsync_RemovesAlbumById()
        {
            // Arrange
            var albumId = 1;
            var albumToRemove = new Album { Id = albumId };
            var mockRepository = new Mock<IAlbumRepository>();
            mockRepository.Setup(repo => repo.RemoveAlbumAsync(albumId))
                .ReturnsAsync(true);
            var repository = mockRepository.Object;

            // Act
            var result = await repository.RemoveAlbumAsync(albumId);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task UpdateAlbumAsync_UpdatesAlbumById()
        {
            // Arrange
            var albumId = 1;
            var updateCommand = new UpdateAlbumCommand { Id = 1, Title = "Updated Title", ReleaseYear = 2023 };
            var albumToUpdate = new Album { Id = albumId };
            var mockRepository = new Mock<IAlbumRepository>();
            mockRepository.Setup(repo => repo.UpdateAlbumAsync(updateCommand, albumId)).ReturnsAsync(albumToUpdate);
            var repository = mockRepository.Object;

            // Act
            var result = await repository.UpdateAlbumAsync(updateCommand, albumId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updateCommand.Title, result.Title);
            Assert.AreEqual(updateCommand.ReleaseYear, result.ReleaseYear);
        }
        
        [TestMethod]
        public async Task GetAlbumByIdAsync_ReturnsAlbumById()
        {
            // Arrange
            var expectedAlbum = new Album { Id = 1 };
            var mockRepository = new Mock<IAlbumRepository>();
            mockRepository.Setup(repo => repo.GetAlbumByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedAlbum);
            var repository = mockRepository.Object;

            // Act
            var result = await repository.GetAlbumByIdAsync(expectedAlbum.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedAlbum.Id, result.Id);
        }

        [TestMethod]
        public async Task CreateAlbumAsync_CreatesNewAlbum()
        {
            // Arrange
            var command = new AddAlbumCommand { Title = "Test Album", ReleaseYear = 2022 };
            var mockRepository = new Mock<IAlbumRepository>();
            mockRepository.Setup(repo => repo.CreateAlbumAsync(It.IsAny<AddAlbumCommand>()))
                .ReturnsAsync(new Album { Title = command.Title, ReleaseYear = command.ReleaseYear });
            var repository = mockRepository.Object;

            // Act
            var result = await repository.CreateAlbumAsync(command);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(command.Title, result.Title);
            Assert.AreEqual(command.ReleaseYear, result.ReleaseYear);
        }
    }
}
