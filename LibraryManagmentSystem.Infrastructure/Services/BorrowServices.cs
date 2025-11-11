using AutoMapper;
using LibraryManagmentSystem.Application.Feature.Borrow.Command.BorrowBook;
using LibraryManagmentSystem.Application.Feature.Borrow.Command.ResponsBorrowBook;
using LibraryManagmentSystem.Application.Feature.Borrow.Command.ReturnBook;
using LibraryManagmentSystem.Application.Feature.Borrow.Queries.GetAllBorrowByUser;
using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Contracts;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Infrastructure.Data.Specifications.BooksSpecifications;
using LibraryManagmentSystem.Shared.DataTransferModel.Books;
using LibraryManagmentSystem.Shared.DataTransferModel.Borrow;
using LibraryManagmentSystem.Shared.Response;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using SharedEventsServices.Events;
using System.Net;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class BorrowServices(IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        IMapper mapper, IServicesManager servicesManager,
        IBus bus) : IBorrowServices
    {
        private readonly IBorrowRepository borrowRepository = unitOfWork.borrowRepository;


        public async Task<ApiResponse<string>> RequestBorrowBook(BorrowBookCommand command)
        {
            try
            {
                var user = await userManager.FindByIdAsync(command.UserId);
                var checkUser = await servicesManager.UserService.ValidateUserForBorrowing(command.UserId);
                if (checkUser.Data != "User passed all checks")
                {
                    return checkUser;
                }
                Book? book = await servicesManager.BookServices.GetBookAsync(command.BookId);
                if (book is null)
                {
                    return ApiResponse<string>.Fail($"Book {book.Title} is not found", (int)HttpStatusCode.NotFound);

                }
                if (!book.IsAvailable)
                {
                    // await servicesManager.ReservationServices.CreateReservation(user.Id, book.Id);
                    return ApiResponse<string>.Fail($"Book {book.Title} is not available", (int)HttpStatusCode.Conflict);
                }
                await servicesManager.publishEventServices.BorrowBook(user, book);

                return ApiResponse<string>.Ok($"Book {book.Title} .", $"Book {book.Title} borrow request submitted successfully.");

            }
            catch (Exception ex) {

                return ApiResponse<string>.Fail($"Error: {ex.InnerException?.Message ?? ex.Message}");

            }



        }
        public async Task<ApiResponse<string>> ApproveBorrowRequest(ResponsBorrowBookCommand command)
        {
            try
            {
                await servicesManager.publishEventServices.BorrowStatusChangedEvent(command);
                if (command.IsApproved)
                {
                    var result = await BorrowBook(command.UserId, Guid.Parse(command.BookId));
                    return result;
                }
                return ApiResponse<string>.Ok($"Request {command.RequestId} Rejected", $"Your request to borrow the book {command.BookTitle} has been rejected.");
            }
            catch (Exception ex) {

                return ApiResponse<string>.Fail($"Error: {ex.InnerException?.Message ?? ex.Message}");

            }
        }

        public async Task<ApiResponse<string>> BorrowBook(string UserId, Guid BookId)
        {

            using (var transaction = await unitOfWork.BeginTransactionAsync())
            {
                try
                {


                    var user = await userManager.FindByIdAsync(UserId);
                    var checkUser = await servicesManager.UserService.ValidateUserForBorrowing(UserId);
                    if (checkUser is not null)
                    {
                        return checkUser;
                    }
                    Book? book = await servicesManager.BookServices.GetBookAsync(BookId);
                    var borrowRecord = await servicesManager.borrowRecordService.GetActiveBorrowAsync(BookId, UserId);
                    if (borrowRecord is not null)
                    {

                        return ApiResponse<string>.Fail($"You have already borrowed the book {book.Title} and not returned it yet", (int)HttpStatusCode.Conflict);

                    }
                    servicesManager.BookServices.UpdateAvailabilityAsync(book, false);
                 
                  
                    await servicesManager.borrowRecordService.CreateBorrowRecordAsync(book, UserId);
                    await servicesManager.UserService.UpdateBorrowLimitAsync(user, -1);
                    servicesManager.BookServices.UpdateTotalBorrow(book);
                    await servicesManager.UserService.UpdateTotalBorrowAsync(user);
                    await unitOfWork.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return new ApiResponse<string>
                    {
                        Message = $"Book {book.Title} borrowed successfully.",
                        Data = $"Book {book.Title} .",
                        StatusCode=(int)HttpStatusCode.OK,
                    };

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return ApiResponse<string>.Fail($"An error occurred while borrowing the book: {ex.Message}");
                }
            }
        }


        public async Task<ApiResponse<string>> ReturnBook(ReturnBookCommand bookCommand)
        {
            using (var transaction = await unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var userId = await userManager.FindByIdAsync(bookCommand.UserId);
                    var Borrow = await servicesManager.borrowRecordService.GetActiveBorrowAsync(bookCommand.BookId, bookCommand.UserId);
                    if (Borrow == null)
                    {
                        return ApiResponse<string>.Fail("No active borrow record found for this book and user.", (int)HttpStatusCode.Conflict);

                    }
                    var overdueDays = 0;
                    if (Borrow.ActualReturnDate < DateTime.UtcNow)
                    {
                        overdueDays = (DateTime.UtcNow - Borrow.ActualReturnDate.Value).Days;

                    }
                    servicesManager.borrowRecordService.SetReturnDate(Borrow);
                    servicesManager.borrowRecordService.UpdateStatus(Borrow);

                    Book? book = await servicesManager.BookServices.GetBookAsync(bookCommand.BookId);
                    servicesManager.BookServices.UpdateAvailabilityAsync(book, true);

                    await servicesManager.UserService.UpdateBorrowLimitAsync(userId, 1);
                    await unitOfWork.SaveChangesAsync();
                    await servicesManager.publishEventServices.ReturnBook(userId.UserName, book.Title);
                  
                    if (overdueDays > 0)
                    {
                        await servicesManager.publishEventServices.FineAdded(bookCommand.UserId, overdueDays*2, $"Late return of book {book.Title} by {overdueDays} days.", Borrow.Book.Title);
                       
                    }
                    await transaction.CommitAsync();
                    var returnMessage = overdueDays > 0 ? $"Book {book.Title} returned successfully.And You Have A fine , Because You are late {overdueDays} Days for your return."
                        : $"Book {book.Title} returned successfully.";
                    return new ApiResponse<string>
                    {
                        Message = $"Book {book.Title} returned successfully.",
                        Data = returnMessage,
                        StatusCode=(int)HttpStatusCode.OK,
                    };
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return ApiResponse<string>.Fail($"An error occurred while returning the book: {ex.Message}");
                }
            }

        }

        public async Task<ApiResponse<IEnumerable<BorrowRecordDto>>> BorrowingHistory(GetAllBorrowByUserQuery user)
        {
            var borrowRecords = await borrowRepository.GetBorrowRecordsByMemberAsync(user.UserId);
            if (borrowRecords is null)
            {
                return ApiResponse<IEnumerable<BorrowRecordDto>>.Fail("No borrow records found for this user.",(int)HttpStatusCode.NoContent);

            }
            var mappedBorrowRecords = mapper.Map<IEnumerable<BorrowRecordDto>>(borrowRecords);
            return ApiResponse<IEnumerable<BorrowRecordDto>>.Ok(mappedBorrowRecords, "Borrow records retrieved successfully.");


        }

        public async Task<int> GetTotalBookBorrowed()
        {
            return await borrowRepository.GetTotalBookBorrowed();
        }

        public async Task<int> GetTotalBookNotReturn()
        {
            var spec = new BorrowNotReturnedCountSpecifications();
            return (await borrowRepository.GetAllAsync(spec)).Count();
        }
    }
}
