using AutoMapper;
using LibraryManagmentSystem.Application.Feature.Books.Command.CreateBook;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.DataTransferModel.Books;

namespace LibraryManagmentSystem.Application.Mapping
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>()
                .ForMember(dist => dist.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
                .ForMember(dist => dist.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ReverseMap();
            CreateMap<CreateBookCommand, Book>().ReverseMap();

        }
    }
}
