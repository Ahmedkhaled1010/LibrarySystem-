using FluentValidation;

namespace LibraryManagmentSystem.Application.Feature.Documents.Queries.GetDocument
{
    public class GetDocumentQueryValidator : AbstractValidator<GetDocumentQuery>
    {
        public GetDocumentQueryValidator()
        {
            RuleFor(x => x.DocumentId)
                .NotEmpty().WithMessage("DocumentId is required.");
        }
    }
}
