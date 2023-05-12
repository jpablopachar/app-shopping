using GenFu;
using book_service.Models;
using book_service.Application;
using Moq;
using Microsoft.EntityFrameworkCore;
using book_service.Persistence;
using AutoMapper;

namespace test_book_service
{
    public class BookServiceTest
    {
        private IEnumerable<LibraryMaterial> GetDataTest()
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

            dbSet.As<IAsyncEnumerable<LibraryMaterial>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken())).Returns(new AsyncEnumerator<LibraryMaterial>(data.GetEnumerator()));

            dbSet.As<IQueryable<LibraryMaterial>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<LibraryMaterial>(data.Provider));

            var context = new Mock<LibraryContext>();

            context.Setup(x => x.LibraryMaterial).Returns(dbSet.Object);

            return context;
        }

        [Fact]
        public async void GetBookId()
        {
            var mockContext = CreateContext();
            var mapConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingTest()));
            var mapper = mapConfig.CreateMapper();
            var request = new QueryFilter.UniqueBook();

            request.LibraryMaterialId = Guid.Empty;

            var handler = new QueryFilter.Handler(mockContext.Object, mapper);
            var book = await handler.Handle(request, new System.Threading.CancellationToken());

            Assert.NotNull(book);
            Assert.True(book.LibraryMaterialId == Guid.Empty);
        }

        [Fact]
        public async void GetBooks()
        {
            var mockContext = CreateContext();
            var mapConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingTest()));
            var mapper = mapConfig.CreateMapper();

            Query.Handler handler = new Query.Handler(mockContext.Object, mapper);

            Query.Main request = new Query.Main();

            var list = await handler.Handle(request, new System.Threading.CancellationToken());

            Assert.True(list.Any());
        }

        [Fact]
        public async void CreateBook()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>().UseInMemoryDatabase(databaseName: "Library").Options;
            var context = new LibraryContext(options);
            var request = new New.Main();

            request.Title = "Libro de microservices";
            request.AuthorBook = Guid.Empty;
            request.PublicationDate = DateTime.Now;

            var handler = new New.Handler(context);
            var book = await handler.Handle(request, new System.Threading.CancellationToken());

            Assert.True(book != null);
        }
    }
}