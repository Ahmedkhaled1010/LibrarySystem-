using LibraryManagmentSystem.Application.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class PublishEventServices(IBus bus) : IPublishEventServices
    {
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
