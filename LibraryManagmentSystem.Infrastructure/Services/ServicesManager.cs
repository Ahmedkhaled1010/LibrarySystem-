using LibraryManagmentSystem.Application.Interfaces;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class ServicesManager(Func<IAuthServices> IAuth,
        Func<IEmailClient> IEmail

       ) : IServicesManager
    {
        public IAuthServices AuthServices => IAuth.Invoke();
        public IEmailClient EmailClient => IEmail.Invoke();



    }
}
