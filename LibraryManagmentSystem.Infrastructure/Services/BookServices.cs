using AutoMapper;
using LibraryManagmentSystem.Application.Feature.Books.Command.CreateBook;
using LibraryManagmentSystem.Application.Feature.Books.Command.DeleteBook;
using LibraryManagmentSystem.Application.Feature.Books.Command.UpdateBook;
using LibraryManagmentSystem.Application.Feature.Books.Queries.GetAllBook;
using LibraryManagmentSystem.Application.Feature.Books.Queries.GetBookById;
using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Contracts;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Infrastructure.Data.Specifications.BooksSpecifications;
using LibraryManagmentSystem.Shared.DataTransferModel.Books;
using LibraryManagmentSystem.Shared.Response;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class BookServices(IUnitOfWork unitOfWork, IMapper mapper) : IBookServices
    {
        private readonly IGenericRepository<Book, Guid> bookRepository = unitOfWork.GetRepository<Book, Guid>();
        private readonly IGenericRepository<Category, Guid> categoryRepository = unitOfWork.GetRepository<Category, Guid>();

        public async Task<ApiResponse<BookDto>> CreateBookAsync(CreateBookCommand createBookCommand)
        {
            //var existingBook = (await bookRepository.GetAllAsync()).
            //    FirstOrDefault(b => b.Title.ToLower() == createBookCommand.Title.ToLower());
            var existingBook = await bookRepository.GetByIdAsync(new BookNameSpecification(createBookCommand.Title));
            if (existingBook != null)
            {
                return ApiResponse<BookDto>.Fail("Book with the same title already exists");
            }
            var category = (await categoryRepository.GetAllAsync()).
                FirstOrDefault(c => c.Name.ToLower() == createBookCommand.CategoryName.ToLower());
            category = await CheckCategory(createBookCommand, category);
            var book = new Book
            {
                Title = createBookCommand.Title,
                CopiesAvailable = createBookCommand.CopiesAvailable,
                CategoryId = category.Id,
                PublishedYear = createBookCommand.PublishedYear,
                BorrowDurationDays = createBookCommand.BorrowDurationDays,
                Price = createBookCommand.Price,
                AuthorId = createBookCommand.AuthorId
            };
            try
            {
                await bookRepository.AddAsync(book);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return ApiResponse<BookDto>.Fail($"Error: {ex.InnerException?.Message ?? ex.Message}");

            }
            var bookDto = mapper.Map<BookDto>(book);
            return ApiResponse<BookDto>.Ok(bookDto, $"Book {book.Title} Created Successfuly");
        }

        public async Task<PagedApiResponse<BookDto>> GetAllBooksAsync(GetAllBookQuery bookQuery)
        {
            var specification = new BookSpecifications(bookQuery.BookQueryParams);
            var countSpecification = new BookCountSpecifications(bookQuery.BookQueryParams);
            var books = await bookRepository.GetAllAsync(specification);
            var totalItems = await bookRepository.CountAsync(countSpecification);
            var bookDtos = mapper.Map<List<BookDto>>(books);
            PaginationInfo pagination = new PaginationInfo
            {
                PageNumber = bookQuery.BookQueryParams.pageNumber,
                PageSize = bookQuery.BookQueryParams.PageSize,
                TotalRecords = totalItems
            };
            return PagedApiResponse<BookDto>.Ok(bookDtos, pagination, "Books retrieved successfully");
        }

        public async Task<ApiResponse<BookDto>> GetBookAsync(GetBookByIdQuery createBookCommand)
        {
            var specification = new BookSpecifications(createBookCommand.Id);
            var book = await bookRepository.GetByIdAsync(specification);
            if (book == null)
            {
                return ApiResponse<BookDto>.Fail("Book not found");
            }
            var bookDto = mapper.Map<BookDto>(book);
            return ApiResponse<BookDto>.Ok(bookDto, "Book retrieved successfully");
        }

        public async Task<ApiResponse<BookDto>> UpdateBookAsync(UpdateBookCommand bookCommand)
        {
            var book = await bookRepository.GetByIdAsync(bookCommand.Id);
            if (book == null)
            {
                return ApiResponse<BookDto>.Fail("Book not found");
            }

            CheckUpdate(bookCommand, book);
            bookRepository.Update(book);
            await unitOfWork.SaveChangesAsync();
            var bookDto = mapper.Map<BookDto>(book);
            return ApiResponse<BookDto>.Ok(bookDto, "Book updated successfully");



        }
        public async Task<ApiResponse<string>> DeleteBookAsync(DeleteBookCommand bookId)
        {
            var book = await bookRepository.GetByIdAsync(bookId.Id);
            if (book == null)
            {
                return ApiResponse<string>.Fail("Book not found");
            }
            bookRepository.Delete(book);
            await unitOfWork.SaveChangesAsync();
            return ApiResponse<string>.Ok($"Book {book.Title} deleted successfully");
        }
        private async Task CheckUpdate(UpdateBookCommand bookCommand, Book? book)
        {
            if (!string.IsNullOrEmpty(bookCommand.CategoryName))
            {
                var category = (await categoryRepository.GetAllAsync()).
                   FirstOrDefault(c => c.Name.ToLower() == bookCommand.CategoryName.ToLower());
                if (category == null)
                {
                    category = new Category
                    {
                        Name = bookCommand.CategoryName
                    };
                    await categoryRepository.AddAsync(category);
                    await unitOfWork.SaveChangesAsync();
                    book.Category = category;
                }
            }
            if (!string.IsNullOrEmpty(bookCommand.Title))
            {
                book.Title = bookCommand.Title;
            }
            if (bookCommand.PublishedYear.HasValue)
            {
                book.PublishedYear = bookCommand.PublishedYear.Value;
            }
            if (bookCommand.CopiesAvailable.HasValue)
            {
                book.CopiesAvailable = bookCommand.CopiesAvailable.Value;
            }
            if (bookCommand.BorrowDurationDays.HasValue)
            {
                book.BorrowDurationDays = bookCommand.BorrowDurationDays.Value;
            }
            if (bookCommand.Price.HasValue)
            {
                book.Price = bookCommand.Price.Value;
            }
        }

        private async Task<Category?> CheckCategory(CreateBookCommand createBookCommand, Category? category)
        {
            if (category == null)
            {
                category = new Category
                {
                    Name = createBookCommand.CategoryName
                };
                await categoryRepository.AddAsync(category);
                await unitOfWork.SaveChangesAsync();
            }

            return category;
        }

        public async Task<Book> GetBookAsync(Guid bookId)
        {
            var book = await bookRepository.GetByIdAsync(bookId);


            return book;
        }

        public bool IsAvailable(Book book)
        {
            return book.CopiesAvailable > 0;
        }

        public void UpdateAvailabilityAsync(Book book, int change)
        {
            book.CopiesAvailable += change;

            bookRepository.Update(book);
        }

        public void UpdateTotalBorrow(Book book)
        {
            book.TotalBorrow += 1;
            bookRepository.Update(book);

        }
    }
}
