using LibraryManagmentSystem.Application.Feature.Borrow.Command.ResponsBorrowBook;
using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Entity;
using MassTransit;
using SharedEventsServices.Events;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class PublishEventServices(IBus bus) : IPublishEventServices
    {
        public async Task BorrowBook(User user, Book book)
        {
            var borrowEvent = new BookBorrowRequestedEvent
            {
                BookId = book.Id,
                UserId = user.Id,
                BookTitle = book.Title,
                RequestedAt = DateTime.UtcNow,
                UserName = user.UserName,
                returnDate = DateTime.UtcNow.AddDays(book.BorrowDurationDays)

            };
            await bus.Publish<BookBorrowRequestedEvent>(borrowEvent);

        }

        public async Task BorrowStatusChangedEvent(ResponsBorrowBookCommand command)
        {
            var response = new BookBorrowStatusChangedEvent
            {
                Status = command.IsApproved ? "Approved" : "Rejected",
                BookTitle = command.BookTitle,
                UserId = command.UserId,
                RequsetId = command.RequestId,

            };
            await bus.Publish<BookBorrowStatusChangedEvent>(response);
        }

        public async Task FineAdded(string userId, decimal amount, string reaseon, string bookTitle)
        {
            var fine = new FineAddedEvent
            {
                UserId = userId,
                Amount = amount,
                Reason = reaseon,
                BorrowId = bookTitle
            };
            await bus.Publish(fine);
        }

        public async Task ReturnBook(string userId, string bookTitle)
        {
            var returnEvent = new ReturnBookEvent
            {

                BookTitle = bookTitle,
                ReturnDate = DateTime.UtcNow,
                UserName = userId
            };
            await bus.Publish<ReturnBookEvent>(returnEvent);
        }

        public async Task SendEmail(string To, string Subject, string Body)
        {
            var mail = new SendEmailEvent()
            {
                To = To,
                Subject = Subject,
                Body = Body
            };
            await bus.Publish<SendEmailEvent>(mail);
        }
    }
}
