using GenFu;
using book_service.Models;
using book_service.Application;
using Moq;
using Microsoft.EntityFrameworkCore;
using book_service.Persistence;
using AutoMapper;
// using GenFu;

namespace test_book_service
{
    public class BookServiceTest
    {
        private IEnumerable<LibraryMaterialDto> GetDataTest()
        {
            A.Configure<LibraryMaterial>().Fill(x => x.Title).AsArticleTitle().Fill(x => x.LibraryMaterialId, () => { return Guid.NewGuid(); });

            var books = A.ListOf<LibraryMaterial>(30);

            books[0].LibraryMaterialId = Guid.Empty;

            return books;
        }

        private Mock<LibraryContext> CreateContext()
        {
            var data = GetDataTest().AsQueryable();

            var dbSet = new Mock<DbSet<LibraryMaterial>>();

            dbSet.As<IQueryable<LibraryMaterial>>().Setup(x => x.Provider).Returns(data.Provider);
            dbSet.As<IQueryable<LibraryMaterial>>().Setup(x => x.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<LibraryMaterial>>().Setup(x => x.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<LibraryMaterial>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());
        }

        [Fact]
        public void GetBooks()
        {
            var mockContext = new Mock<LibraryContext>();
            var mockMapper = new Mock<IMapper>();

            Query.Handler handler = new Query.Handler(mockContext.Object, mockMapper.Object);
        }
    }
}