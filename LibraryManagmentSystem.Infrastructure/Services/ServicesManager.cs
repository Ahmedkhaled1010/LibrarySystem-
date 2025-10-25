using LibraryManagmentSystem.Application.Interfaces;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class ServicesManager(Func<IAuthServices> IAuth

       ) : IServicesManager
    {
        public IAuthServices AuthServices => IAuth.Invoke();



    }
}
