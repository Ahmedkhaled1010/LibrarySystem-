using AutoMapper;
using LibraryManagmentSystem.Application.Feature.Borrow.Command.BorrowBook;
using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Contracts;
using LibraryManagmentSystem.Domain.Entity;
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
            return new ApiResponse<string>
            {
                Message = $"Book {book.Title} borrowed successfully.",
                Data = $"Book {book.Title} .",
            };



        }







    }
}
