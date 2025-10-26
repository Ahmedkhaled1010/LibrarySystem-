using LibraryManagmentSystem.Application.Interfaces;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class ServicesManager(Func<IAuthServices> IAuth,
        Func<ICategoryServices> ICategory,
        Func<IBookServices> IBook,
        Func<IEmailClient> IEmail

       ) : IServicesManager
    {
        public IAuthServices AuthServices => IAuth.Invoke();
        public IEmailClient EmailClient => IEmail.Invoke();

        public ICategoryServices CategoryServices => ICategory.Invoke();

        public IBookServices BookServices => IBook.Invoke();
    }
}
