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
                    _logger.LogInformation("Worker running at: {time}",
                        DateTimeOffset.Now);
                }

                await Task.Delay(1000, stoppingToken);
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
    }
}
