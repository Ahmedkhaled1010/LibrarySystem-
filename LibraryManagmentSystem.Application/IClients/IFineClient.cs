using LibraryManagmentSystem.Shared.Model;

namespace LibraryManagmentSystem.Application.IClients
{
    public interface IFineClient
    {
        Task AddFineAsync(Fine fine);
    }
}
