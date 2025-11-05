using AutoMapper;
using LibraryManagmentSystem.Application.Feature.Borrow.Command.BorrowBook;
using LibraryManagmentSystem.Application.Feature.Borrow.Command.ResponsBorrowBook;
using LibraryManagmentSystem.Application.Feature.Borrow.Command.ReturnBook;
using LibraryManagmentSystem.Application.Feature.Borrow.Queries.GetAllBorrowByUser;
using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Contracts;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.DataTransferModel.Borrow;
using LibraryManagmentSystem.Shared.Model;
using LibraryManagmentSystem.Shared.Response;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using SharedEventsServices.Events;

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
            var user = await userManager.FindByIdAsync(command.UserId);
            var checkUser = await servicesManager.UserService.ValidateUserForBorrowing(command.UserId);
            if (checkUser is not null)
            {
                return checkUser;
            }
            Book? book = await servicesManager.BookServices.GetBookAsync(command.BookId);
            var isAvailable = servicesManager.BookServices.IsAvailable(book);
            if (!isAvailable)
            {
                return ApiResponse<string>.Fail($"Book {book.Title} is not available");
            }
            var borrowEvent = new BookBorrowRequestedEvent
            {
                BookId = command.BookId,
                UserId = command.UserId,
                BookTitle = book.Title,
                RequestedAt = DateTime.UtcNow,
                UserName = user.UserName,
                returnDate = DateTime.UtcNow.AddDays(book.BorrowDurationDays)

            };

            await bus.Publish<BookBorrowRequestedEvent>(borrowEvent);
            return ApiResponse<string>.Ok($"Book {book.Title} .", $"Book {book.Title} borrow request submitted successfully.");




        }
        public async Task<ApiResponse<string>> ApproveBorrowRequest(ResponsBorrowBookCommand command)
        {
            var response = new BookBorrowStatusChangedEvent
            {
                Status = command.IsApproved ? "Approved" : "Rejected",
                BookTitle = command.BookTitle,
                UserId = command.UserId,
                RequsetId = command.RequestId,

            };
            bus.Publish<BookBorrowStatusChangedEvent>(response);
            if (command.IsApproved)
            {
                var result = await BorrowBook(command.UserId, Guid.Parse(command.BookId));
                return result;
            }
            return ApiResponse<string>.Ok($"Request {command.RequestId} Rejected", $"Your request to borrow the book {command.BookTitle} has been rejected.");
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
                    var isAvailable = servicesManager.BookServices.IsAvailable(book);
                    if (!isAvailable)
                    {
                        //Implement a waitlist feature here in the future   
                        return ApiResponse<string>.Fail($"Book {book.Title} is not available");
                    }
                    var borrowRecord = await servicesManager.borrowRecordService.GetActiveBorrowAsync(BookId, UserId);
                    if (borrowRecord is not null)
                    {

                        return ApiResponse<string>.Fail($"You have already borrowed the book {book.Title} and not returned it yet");

                    }
                    await servicesManager.borrowRecordService.CreateBorrowRecordAsync(book, UserId);
                    servicesManager.BookServices.UpdateAvailabilityAsync(book, -1);
                    await servicesManager.UserService.UpdateBorrowLimitAsync(user, -1);
                    servicesManager.BookServices.UpdateTotalBorrow(book);
                    await servicesManager.UserService.UpdateTotalBorrowAsync(user);
                    await unitOfWork.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return new ApiResponse<string>
                    {
                        Message = $"Book {book.Title} borrowed successfully.",
                        Data = $"Book {book.Title} .",
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
                        return ApiResponse<string>.Fail("No active borrow record found for this book and user.");

                    }
                    var overdueDays = 0;
                    if (Borrow.ActualReturnDate < DateTime.UtcNow)
                    {
                        overdueDays = (DateTime.UtcNow - Borrow.ActualReturnDate.Value).Days;

                    }
                    servicesManager.borrowRecordService.SetReturnDate(Borrow);
                    servicesManager.borrowRecordService.UpdateStatus(Borrow);

                    Book? book = await servicesManager.BookServices.GetBookAsync(bookCommand.BookId);
                    servicesManager.BookServices.UpdateAvailabilityAsync(book, 1);
                    await servicesManager.UserService.UpdateBorrowLimitAsync(userId, 1);
                    await unitOfWork.SaveChangesAsync();
                    var returnEvent = new ReturnBookEvent
                    {

                        BookTitle = book.Title,
                        ReturnDate = DateTime.UtcNow,
                        UserName = userId.UserName
                    };
                    await bus.Publish<ReturnBookEvent>(returnEvent);
                    if (overdueDays > 0)
                    {
                        Fine fine = new Fine
                        {
                            UserId = bookCommand.UserId,
                            Amount = overdueDays * 2,
                            Reason = $"Late return of book {book.Title} by {overdueDays} days.",
                            DateIssued = DateTime.UtcNow
                        };
                        await servicesManager.FineClient.AddFineAsync(fine);
                    }
                    await transaction.CommitAsync();
                    var returnMessage = overdueDays > 0 ? $"Book {book.Title} returned successfully.And You Have A fine , Because You are late {overdueDays} Days for your return."
                        : $"Book {book.Title} returned successfully.";
                    return new ApiResponse<string>
                    {
                        Message = $"Book {book.Title} returned successfully.",
                        Data = returnMessage
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
                return ApiResponse<IEnumerable<BorrowRecordDto>>.Fail("No borrow records found for this user.");

            }
            var mappedBorrowRecords = mapper.Map<IEnumerable<BorrowRecordDto>>(borrowRecords);
            return ApiResponse<IEnumerable<BorrowRecordDto>>.Ok(mappedBorrowRecords, "Borrow records retrieved successfully.");


        }
    }
}
