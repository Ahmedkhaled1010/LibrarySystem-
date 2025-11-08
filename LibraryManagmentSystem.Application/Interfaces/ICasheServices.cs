namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface ICasheServices
    {
        Task<string?> GetAsync(string casheKey);
        Task<bool> SetAsync<T>(string cachekey, T cachevalue, TimeSpan timeSpan);
        Task<bool> DeleteAsync(string id);
    }
}
