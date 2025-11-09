namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface IFavoriteCacheService
    {
        Task AddToFavoriteAsync(string userId, string bookId);
        Task RemoveFromFavoriteAsync(string userId, string bookId);
        Task<List<string>> GetFavoritesAsync(string userId);
    }
}
