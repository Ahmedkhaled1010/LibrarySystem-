using LibraryManagmentSystem.Shared.DataTransferModel.Books;

namespace LibraryManagmentSystem.Shared.DataTransferModel.Category
{
    public class CategoryDto
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<BookDto> Books { get; set; }


    }
}
