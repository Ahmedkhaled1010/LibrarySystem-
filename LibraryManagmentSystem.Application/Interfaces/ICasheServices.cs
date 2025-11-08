namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface ICasheServices
    {
        Task<string?> GetAsync(string casheKey);
        Task SetAsync<T>(string cachekey, T cachevalue, TimeSpan timeSpan);
    }
}
