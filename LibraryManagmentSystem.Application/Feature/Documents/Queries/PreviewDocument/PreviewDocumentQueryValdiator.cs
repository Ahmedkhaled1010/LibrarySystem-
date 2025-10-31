using FluentValidation;

namespace LibraryManagmentSystem.Application.Feature.Documents.Queries.PreviewDocument
{
    public class PreviewDocumentQueryValdiator : AbstractValidator<PreviewDocumentQuery>
    {
        public PreviewDocumentQueryValdiator()
        {
            RuleFor(x => x.DocumentId)
              .NotEmpty().WithMessage("DocumentId is required.");
            RuleFor(x => x.Type).NotEmpty().WithMessage("Type is required.");
        }
    }
}
