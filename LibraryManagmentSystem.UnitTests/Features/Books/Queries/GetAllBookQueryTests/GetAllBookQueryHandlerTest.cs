using LibraryManagmentSystem.Application.Feature.Books.Queries.GetAllBook;
using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Contracts;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.QueryParams;
using Moq;
using Shouldly;

namespace LibraryManagmentSystem.UnitTests.Features.Books.Queries.GetAllBookQueryTests
{
    public class GetAllBookQueryHandlerTests
    {
        //private readonly Mock<IBookServices> _bookServiceMock;
        //private readonly Mock<IServicesManager> _servicesManagerMock;
        //private readonly Mock<IMapper> _mapperMock;
        //private readonly GetAllBookQueryHandler _handler;
        //public GetAllBookQueryHandlerTests()
        //{
        //    _bookServiceMock = new Mock<IBookServices>();
        //    _servicesManagerMock = new Mock<IServicesManager>();
        //    _mapperMock = new Mock<IMapper>();

        //    _servicesManagerMock.Setup(s => s.BookServices).Returns(_bookServiceMock.Object);

        //    _handler = new GetAllBookQueryHandler(_servicesManagerMock.Object);
        //}
        //[Fact]
        //public async Task Handle_ReturnsPagedApiResponse_WithBooks()
        //{
        //    var fakeBooks = FakeBooks; // List<Book>
        //    var fakeBookDtos = new List<BookDto>
        //{
        //    new BookDto { Title = "C# in Depth", Price = 250 },
        //    new BookDto { Title = "ASP.NET Core Guide", Price = 200 }
        //};
        //    var queryParams = new BookQueryParams
        //    {
        //        pageNumber = 1,
        //        PageSize = 10
        //    };
        //    var query = new GetAllBookQuery(queryParams);
        //    _bookServiceMock
        //   .Setup(s => s.GetAllBooksAsync(It.IsAny<GetAllBookQuery>()))
        //   .ReturnsAsync(new PagedApiResponse<BookDto>
        //   {
        //       Data = fakeBookDtos,
        //       Message = "Books retrieved successfully",
        //       Pagination = new PaginationInfo { PageNumber = 1, PageSize = 10, TotalRecords = 2 }
        //   });
        //    var result = await _handler.Handle(query, CancellationToken.None);
        //    Assert.NotNull(result);
        //    Assert.Equal(2, result.Data.Count);
        //    Assert.Equal("Books retrieved successfully", result.Message);
        //    Assert.Equal(1, result.Pagination.PageNumber);
        //}
        //public static List<Book> FakeBooks => new()
        //{
        //    new Book
        //    {
        //        Id = Guid.NewGuid(),
        //        Title = "C# in Depth",
        //        AuthorId = "author1",
        //        PublishedYear = 2020,
        //        Price = 250,
        //        BorrowDurationDays = 14,
        //        IsAvailable = true,
        //        CategoryId = Guid.NewGuid(),
        //        Description="Test"
        //    },
        //    new Book
        //    {
        //        Id=Guid.NewGuid(),
        //        Title="ASP.NET Core Guide",
        //        AuthorId="author2",
        //        PublishedYear=2022,
        //        Price=200,
        //        BorrowDurationDays=10,
        //        IsAvailable=true,
        //        CategoryId=Guid.NewGuid(),
        //        Description="Test"
        //    }
        //};

        private readonly Mock<IServicesManager> _servicesManagerMock;
        private readonly Mock<IGenericRepository<Book, Guid>> mockRepo;
        private readonly GetAllBookQueryHandler _handler;
        private readonly Mock<IBookServices> _bookServiceMock;
        public GetAllBookQueryHandlerTests()
        {

            _servicesManagerMock = new Mock<IServicesManager>();

            _bookServiceMock = new Mock<IBookServices>();
            _servicesManagerMock.Setup(s => s.BookServices).Returns(_bookServiceMock.Object);
            _handler = new GetAllBookQueryHandler(_servicesManagerMock.Object);
        }
        [Fact]
        public async Task Handle_ReturnsPagedApiResponse_WithBooks()
        {

            var queryParams = new BookQueryParams
            {
                pageNumber = 1,
                PageSize = 10
            };
            var query = new GetAllBookQuery(queryParams);


            var result = await _handler.Handle(query, CancellationToken.None);
            result.ShouldBeOfType<List<Book>>();
            result.Data.Count.ShouldBe(12);
        }
        public static List<Book> FakeBooks => new()
        {
            new Book
            {
                Id = Guid.NewGuid(),
                Title = "C# in Depth",
                AuthorId = "author1",
                PublishedYear = 2020,
                Price = 250,
                BorrowDurationDays = 14,
                IsAvailable = true,
                CategoryId = Guid.NewGuid(),
                Description="Test"
            },
            new Book
            {
                Id=Guid.NewGuid(),
                Title="ASP.NET Core Guide",
                AuthorId="author2",
                PublishedYear=2022,
                Price=200,
                BorrowDurationDays=10,
                IsAvailable=true,
                CategoryId=Guid.NewGuid(),
                Description="Test"
            }
        };
    }
}
