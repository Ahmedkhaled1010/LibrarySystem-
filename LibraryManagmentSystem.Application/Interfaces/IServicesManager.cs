namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface IServicesManager
    {
        public IAuthServices AuthServices { get; }
        public ICategoryServices CategoryServices { get; }
        public IBookServices BookServices { get; }
        public IEmailClient EmailClient { get; }

    }
}
