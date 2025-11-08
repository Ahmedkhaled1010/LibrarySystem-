using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Contracts;
using System.Text.Json;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class CasheServices(IUnitOfWork unitOfWork) : ICasheServices
    {

        public Task<string?> GetAsync(string casheKey) => unitOfWork.casheRepository.GetAsync(casheKey);


        public async Task SetAsync<T>(string cachekey, T cachevalue, TimeSpan timeSpan)
        {
            var value = JsonSerializer.Serialize(cachevalue);
            await unitOfWork.casheRepository.SetAsync(cachekey, value, timeSpan);
        }
    }
}
