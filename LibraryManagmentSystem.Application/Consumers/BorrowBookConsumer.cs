using LibraryManagmentSystem.Application.Events;
using MassTransit;

namespace LibraryManagmentSystem.Application.Consumers
{
    public class BorrowBookConsumer : IConsumer<BookBorrowRequestedEvent>
    {
        public async Task Consume(ConsumeContext<BookBorrowRequestedEvent> context)
        {
            var message = context.Message;
            Console.WriteLine($"Received borrow request for BookId: {message.BookId}, UserId: {message.UserId}");

            await Task.CompletedTask;
        }
    }
}
