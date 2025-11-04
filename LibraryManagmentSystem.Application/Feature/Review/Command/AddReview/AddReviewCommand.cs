using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Review.Command.AddReview
{
    public class AddReviewCommand : IRequest<ApiResponse<string>>
    {
        public string Comment { get; set; }
        public string BookTitle { get; set; }
        public string UserName { get; set; }
        public Guid BookId { get; set; }
        public int Rating { get; set; }
    }
}
