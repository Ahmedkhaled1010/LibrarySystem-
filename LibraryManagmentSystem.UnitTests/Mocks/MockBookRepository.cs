using LibraryManagmentSystem.Domain.Contracts;
using LibraryManagmentSystem.Domain.Entity;
using Moq;

namespace LibraryManagmentSystem.UnitTests.Mocks
{
    public class MockBookRepository
    {
        public static Mock<IGenericRepository<Book, Guid>> GetMockBookRepository()
        {
            var books = new List<Book>
        {
            new Book
            {
                Id = Guid.NewGuid(),
                Title = "C# in Depth",
                PublishedYear = 2020,
                AuthorId = "author1",
                BorrowDurationDays = 14,
                Price = 250,
                CategoryId = Guid.NewGuid(),
                Description = "Deep dive into C# language features",
                Language = "English",
                Pages = 900,
                TotalBorrow = 120,
                TotalSell = 50,
                IsAvailable = true,
                IsAvailableForSale = true,
                             Status="Available",

                PdfUrl = "https://example.com/csharp.pdf",
                CoverImageUrl = "https://example.com/csharp-cover.jpg"
            },
            new Book
            {
                Id = Guid.NewGuid(),
                Title = "ASP.NET Core Guide",
                PublishedYear = 2022,
                AuthorId = "author2",
                BorrowDurationDays = 10,
                Price = 200,
                CategoryId = Guid.NewGuid(),
                Description = "Building modern web apps with ASP.NET Core",
                Language = "English",
                Pages = 750,
                TotalBorrow = 90,
                TotalSell = 40,
                IsAvailable = true,
                IsAvailableForSale = true,
                             Status="Available",

                PdfUrl = "https://example.com/aspnetcore.pdf",
                CoverImageUrl = "https://example.com/aspnetcore-cover.jpg"
            },
            new Book
            {
                Id = Guid.NewGuid(),
                Title = "Learn Entity Framework Core",
                PublishedYear = 2021,
                AuthorId = "author3",
                BorrowDurationDays = 12,
                Price = 180,
                CategoryId = Guid.NewGuid(),
                Description = "Complete guide for EF Core",
                Language = "English",
                Pages = 500,
                TotalBorrow = 75,
                TotalSell = 30,
                IsAvailable = true,
                IsAvailableForSale = true,
                              Status="Available",

                PdfUrl = "https://example.com/efcore.pdf",
                CoverImageUrl = "https://example.com/efcore-cover.jpg"
            },
            new Book
            {
                Id = Guid.NewGuid(),
                Title = "JavaScript Essentials",
                PublishedYear = 2019,
                AuthorId = "author4",
                BorrowDurationDays = 7,
                Price = 150,
                CategoryId = Guid.NewGuid(),
                Description = "Master JS fundamentals",
                Language = "English",
                Pages = 400,
                TotalBorrow = 100,
                TotalSell = 45,
                IsAvailable = true,
                IsAvailableForSale = true,
                              Status="Available",

                PdfUrl = "https://example.com/js.pdf",
                CoverImageUrl = "https://example.com/js-cover.jpg"
            },
            new Book
            {
                Id = Guid.NewGuid(),
                Title = "Python for Data Science",
                PublishedYear = 2023,
                AuthorId = "author5",
                BorrowDurationDays = 14,
                Price = 300,
                CategoryId = Guid.NewGuid(),
                Description = "Data Science and Machine Learning with Python",
                Language = "English",
                Pages = 850,
                TotalBorrow = 60,
                TotalSell = 25,
                IsAvailable = true,
                IsAvailableForSale = true,
              Status="Available",
                PdfUrl = "https://example.com/python.pdf",
                CoverImageUrl = "https://example.com/python-cover.jpg"
            }
        };

            var mockRepo = new Mock<IGenericRepository<Book, Guid>>();
            mockRepo.Setup(b => b.GetAllAsync(It.IsAny<ISpecifications<Book, Guid>>())).ReturnsAsync(books);
            mockRepo.Setup(b => b.AddAsync(It.IsAny<Book>())).Returns((Book book) =>
            {
                books.Add(book);
                return Task.CompletedTask;


            }

                );
            mockRepo.Setup(b => b.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) =>
            {
                return books.FirstOrDefault(x => x.Id == id);
            });

            return mockRepo;
        }

    }
}
