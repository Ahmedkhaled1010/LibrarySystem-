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
using System.Net;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class BookServices(IUnitOfWork unitOfWork, IMapper mapper) : IBookServices
    {
        private readonly IGenericRepository<Book, Guid> bookRepository = unitOfWork.GetRepository<Book, Guid>();
        private readonly IGenericRepository<Category, Guid> categoryRepository = unitOfWork.GetRepository<Category, Guid>();

        public async Task<ApiResponse<BookDto>> CreateBookAsync(CreateBookCommand createBookCommand)
        {
            using var transaction = await unitOfWork.BeginTransactionAsync();

            try
            {
                var existingBook = await bookRepository.GetByIdAsync(new BookNameSpecification(createBookCommand.Title));
            if (existingBook != null)
            {
                return ApiResponse<BookDto>.Fail("Book with the same title already exists",(int)HttpStatusCode.Conflict);
            }
            var category = (await categoryRepository.GetAllAsync()).
                FirstOrDefault(c => c.Name.ToLower() == createBookCommand.CategoryName.ToLower());
            category = await CheckCategory(createBookCommand, category);

            var book = mapper.Map<Book>(createBookCommand);
            book.CategoryId = category.Id;

         
                await bookRepository.AddAsync(book);
                await unitOfWork.SaveChangesAsync();
                var bookDto = mapper.Map<BookDto>(book);
               await  transaction.CommitAsync();
                return ApiResponse<BookDto>.Ok(bookDto, $"Book {book.Title} Created Successfuly",(int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                return ApiResponse<BookDto>.Fail($"Error: {ex.InnerException?.Message ?? ex.Message}");

            }
           
        }

        public async Task<PagedApiResponse<BookDto>> GetAllBooksAsync(GetAllBookQuery bookQuery)
        {
            try
            {
                var specification = new BookSpecifications(bookQuery.BookQueryParams);
                var countSpecification = new BookCountSpecifications(bookQuery.BookQueryParams);

                var totalItems = await bookRepository.CountAsync(countSpecification);
                PaginationInfo pagination = new PaginationInfo
                {
                    PageNumber = bookQuery.BookQueryParams.pageNumber,
                    PageSize = bookQuery.BookQueryParams.PageSize,
                    TotalRecords = totalItems
                };
                var books = await bookRepository.GetAllAsync(specification);
                if (books is null)
                {
                    return PagedApiResponse<BookDto>.Ok([], pagination, "No books found", (int)HttpStatusCode.NoContent);

                }
                var bookDtos = mapper.Map<List<BookDto>>(books);

                return PagedApiResponse<BookDto>.Ok(bookDtos, pagination, "Books retrieved successfully");
            }
            catch (Exception ex) {
                return PagedApiResponse<BookDto>.Fail($"Error: {ex.InnerException?.Message ?? ex.Message}");

            }
        }

        public async Task<ApiResponse<BookDto>> GetBookAsync(GetBookByIdQuery createBookCommand)
        {
            try
            {
                var specification = new BookSpecifications(createBookCommand.Id);
                var book = await bookRepository.GetByIdAsync(specification);
                if (book == null)
                {
                    return ApiResponse<BookDto>.Fail("Book not found", (int)HttpStatusCode.NotFound);
                }
                var bookDto = mapper.Map<BookDto>(book);
                return ApiResponse<BookDto>.Ok(bookDto, "Book retrieved successfully");
            }
            catch (Exception ex) {
                return ApiResponse<BookDto>.Fail($"Error: {ex.InnerException?.Message ?? ex.Message}");

            }
        }

        public async Task<ApiResponse<BookDto>> UpdateBookAsync(UpdateBookCommand bookCommand)
        {
            var book = await bookRepository.GetByIdAsync(bookCommand.Id);
            if (book == null)
            {
                return ApiResponse<BookDto>.Fail("Book not found", (int)HttpStatusCode.NotFound);
            }

            await CheckUpdate(bookCommand, book);
            mapper.Map(bookCommand, book);
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
                return ApiResponse<string>.Fail("Book not found", (int)HttpStatusCode.NotFound);
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

        }
        public async Task<Book> GetBookAsync(Guid bookId)
        {
            var book = await bookRepository.GetByIdAsync(bookId);


            return book;
        }
      
       

        public void UpdateTotalBorrow(Book book)
        {
            book.TotalBorrow += 1;
            bookRepository.Update(book);

        }

        public async void UpdateAvailabilityAsync(Book book, bool avail)
        {
            book.IsAvailable = avail;
            bookRepository.Update(book);
        }
        public async void UpdateAvailabilityForSaleAsync(Book book, bool avail)
        {
            book.IsAvailableForSale = avail;
            bookRepository.Update(book);
        }

        public Task<BookDto> GetBookAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetTotalBookAsync()
        {
            return await bookRepository.CountAsync();
        }

        public async Task<int> GetTotalAvailable(bool isAvail)
        {
            var spec = new BookAvailableCountSpecifications(isAvail);
            var total = (await bookRepository.GetAllAsync(spec)).Count();
            return total;
        }
        private async Task<Category?> CheckCategory(CreateBookCommand createBookCommand, Category? category)
        {

            if (category == null)
            {
                category = new Category
                {
                    Name = createBookCommand.CategoryName
                };
                try
                {
                    await categoryRepository.AddAsync(category);
               
                    await unitOfWork.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException?.Message);
                    throw;
                }
            }

            return category;
        }

    }
}
