namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface IServicesManager
    {
        public IAuthServices AuthServices { get; }
        public IEmailClient EmailClient { get; }

    }
}
