using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Contracts;
using System.Text.Json;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class CasheServices(IUnitOfWork unitOfWork) : ICasheServices
    {
        public Task<bool> DeleteAsync(string id) => unitOfWork.casheRepository.DeleteAsync(id);


        public Task<string?> GetAsync(string casheKey) => unitOfWork.casheRepository.GetAsync(casheKey);


        public async Task<bool> SetAsync<T>(string cachekey, T cachevalue, TimeSpan timeSpan)
        {
            var value = JsonSerializer.Serialize(cachevalue);
            return await unitOfWork.casheRepository.SetAsync(cachekey, value, timeSpan);
        }
    }
}
