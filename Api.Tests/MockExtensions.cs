using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Api.Tests
{
    public static class MockExtensions
    {
        public static Mock<DbSet<T>> ReturnsDbSet<T>(this Mock<DataBaseContext> mockContext, List<T> data) where T : class
        {
            var queryableData = data.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryableData.GetEnumerator());
            mockContext.Setup(c => c.Set<T>()).Returns(dbSetMock.Object);
            return dbSetMock;
        }
    }
}