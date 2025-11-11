using LibraryManagmentSystem.Application.Feature.Borrow.Command.ResponsBorrowBook;
using LibraryManagmentSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface IPublishEventServices
    {
        Task SendEmail(string To,string Subject,string Body);
        Task BorrowBook(User user, Book book);
        Task BorrowStatusChangedEvent(ResponsBorrowBookCommand respons);
        Task ReturnBook(string userId, string bookTitle);
        Task FineAdded(string userId,decimal amount,string reaseon,string bookTitle);
    }
}
