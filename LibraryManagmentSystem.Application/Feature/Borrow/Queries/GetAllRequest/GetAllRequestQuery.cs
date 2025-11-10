using LibraryManagmentSystem.Shared.DataTransferModel.RequestDto;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Borrow.Queries.GetAllRequest
{
    public record GetAllRequestQuery(string status) : IRequest<ApiResponse<IReadOnlyList<RequestDto>>>;

}
