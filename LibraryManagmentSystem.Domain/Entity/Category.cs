namespace LibraryManagmentSystem.Domain.Entity
{
    public class Category : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; } = "No description provided";
        public ICollection<Book> Books { get; set; }

    }
}
