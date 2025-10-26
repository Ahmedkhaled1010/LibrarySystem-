using LibraryManagmentSystem.Domain.Entity;

namespace LibraryManagmentSystem.Shared.DataTransferModel.Category
{
    public class CategoryDto
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Book> Books { get; set; }


    }
}
