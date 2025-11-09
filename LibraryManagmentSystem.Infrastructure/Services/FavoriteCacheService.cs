using LibraryManagmentSystem.Application.Interfaces;
using StackExchange.Redis;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class FavoriteCacheService(IConnectionMultiplexer connection) : IFavoriteCacheService
    {
        private readonly IDatabase database = connection.GetDatabase();
        private string GetKey(string userId) => $"favorites:{userId}";

        public async Task AddToFavoriteAsync(string userId, string bookId)
        {
            await database.SetAddAsync(GetKey(userId), bookId);
        }

        public async Task<List<string>> GetFavoritesAsync(string userId)
        {
            var data = await database.SetMembersAsync(GetKey(userId));
            return data.Select(x => x.ToString()).ToList();
        }

        public async Task RemoveFromFavoriteAsync(string userId, string bookId)
        {
            await database.SetRemoveAsync(GetKey(userId), bookId);
        }
    }
}
