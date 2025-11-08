using LibraryManagmentSystem.Domain.Contracts;
using StackExchange.Redis;

namespace LibraryManagmentSystem.Infrastructure.Data.Repositories
{
    public class CasheRepository(IConnectionMultiplexer connection) : ICasheRepository
    {
        private readonly IDatabase database = connection.GetDatabase();



        public async Task<string?> GetAsync(string CasheKey)
        {
            var CasheValue = await database.StringGetAsync(CasheKey);
            return CasheValue.IsNullOrEmpty ? null : CasheValue.ToString();
        }

        public async Task<bool> SetAsync(string CasheKey, string CasheValue, TimeSpan timeSpan)
        {
            return await database.StringSetAsync(CasheKey, CasheValue, timeSpan);
        }
        public async Task<bool> DeleteAsync(string id)
        {
            return await database.KeyDeleteAsync(id);
        }
    }
}
