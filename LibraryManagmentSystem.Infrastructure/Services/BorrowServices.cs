using AutoMapper;
using LibraryManagmentSystem.Application.Feature.Borrow.Command.BorrowBook;
using LibraryManagmentSystem.Application.Feature.Borrow.Command.ReturnBook;
using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Contracts;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.Model;
using LibraryManagmentSystem.Shared.Response;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class BorrowServices(IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        IMapper mapper, IServicesManager servicesManager) : IBorrowServices
    {
        private readonly IBorrowRepository borrowRepository = unitOfWork.borrowRepository;

        public async Task<ApiResponse<string>> BorrowBook(BorrowBookCommand command)
        {
            using (var transaction = await unitOfWork.BeginTransactionAsync())
            {
                try
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
                        //Implement a waitlist feature here in the future   
                        return ApiResponse<string>.Fail($"Book {book.Title} is not available");
                    }
                    var borrowRecord = await servicesManager.borrowRecordService.GetActiveBorrowAsync(command.BookId, command.UserId);
                    if (borrowRecord is not null)
                    {

                        return ApiResponse<string>.Fail($"You have already borrowed the book {book.Title} and not returned it yet");

                    }
                    await servicesManager.borrowRecordService.CreateBorrowRecordAsync(book, command.UserId);
                    servicesManager.BookServices.UpdateAvailabilityAsync(book, -1);
                    await servicesManager.UserService.UpdateBorrowLimitAsync(user, -1);
                    servicesManager.BookServices.UpdateTotalBorrow(book);
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
                    Book? book = await servicesManager.BookServices.GetBookAsync(bookCommand.BookId);
                    servicesManager.BookServices.UpdateAvailabilityAsync(book, 1);
                    await servicesManager.UserService.UpdateBorrowLimitAsync(userId, 1);
                    await unitOfWork.SaveChangesAsync();

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
    }
}
