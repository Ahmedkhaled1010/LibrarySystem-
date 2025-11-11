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
    }
}
