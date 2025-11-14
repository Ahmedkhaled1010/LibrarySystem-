using LibraryManagmentSystem.Domain.Enum.BorrowRecord;
using LibraryManagmentSystem.Domain.Enum.Reservations;
using LibraryManagmentSystem.Infrastructure.Data.Context;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SharedEventsServices.Events;

namespace BackgroundServices
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly LibraryDbContext dbContext;
        private readonly IBus bus;
        private readonly IServiceScopeFactory _scopeFactory;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory, IBus bus)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            this.bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();


                    await NotifyUpcomingReturns(dbContext);
                    await NotifyAvailableBooks(dbContext);
                    await UpdateNotReturnStatus(dbContext);
                    _logger.LogInformation("Worker running at: {time}",
                        DateTimeOffset.Now);
                }

                //  await Task.Delay(1000, stoppingToken);
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);

            }
        }
        private async Task NotifyUpcomingReturns(LibraryDbContext dbContext)
        {
            var upcomingReturns = await dbContext.borrowRecords
                .Include(b => b.User)
                .Include(b => b.Book)
                .Where(b => b.ActualReturnDate.HasValue &&
                            b.ActualReturnDate.Value.Date == DateTime.UtcNow.AddDays(1).Date)
                .ToListAsync();

            foreach (var record in upcomingReturns)
            {
                var notify = new NotifyUpcomingReturnsEvent
                {
                    UserId = record.UserId,
                    BookTitle = record.Book.Title,
                    ActualReturnDate = record.ActualReturnDate,
                    UserEmail = record.User.Email

                };
                await bus.Publish(notify);
            }
        }
        private async Task NotifyAvailableBooks(LibraryDbContext dbContext)
        {
            var ReserveBook = await dbContext.reservations
                .Include(r => r.User)
                .Where(r => r.Status == ReservationStatus.Active).ToListAsync();
            foreach (var record in ReserveBook)
            {
                var book = await dbContext.books.FirstOrDefaultAsync(b => b.Id == record.BookId);
                var notify = new NotifyAvailableEvent
                {
                    UserId = record.UserId,
                    BookTitle = book.Title,
                    UserEmail = record.User.Email

                };
                await bus.Publish(notify);
            }
        }
        private async Task UpdateNotReturnStatus(LibraryDbContext dbContext)
        {
            var upcomingReturns = await dbContext.borrowRecords
                .Include(b => b.User)
                .Include(b => b.Book)
                .Where(b => b.ActualReturnDate.HasValue &&
                            b.ActualReturnDate.Value.Date <= DateTime.UtcNow.Date && b.ReturnDate == null && b.Status == BorrowRecordStatus.active.ToString())
                .ToListAsync();

            foreach (var record in upcomingReturns)
            {
                record.Status = BorrowRecordStatus.overdue.ToString();
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
