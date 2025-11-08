namespace LibraryManagmentSystem.Domain.Contracts
{
    public interface ICasheRepository
    {
        Task<string?> GetAsync(string CasheKey);

        Task<bool> SetAsync(string CasheKey, string CasheValue, TimeSpan timeSpan);
        Task<bool> DeleteAsync(string id);

    }
}
