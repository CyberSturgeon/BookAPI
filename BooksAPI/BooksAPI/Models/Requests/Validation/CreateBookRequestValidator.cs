using FluentValidation;
using SampleBackend.Models.Requests;

namespace BooksAPI.Models.Requests.Validation;

public class CreateBookRequestValidator : AbstractValidator<CreateBookRequest>
{
    public CreateBookRequestValidator()
    {
        RuleFor(model => model.Name).NotEmpty().WithMessage("The Name property must not be empty.").Length(6, 100).WithMessage("The Name property must be between 1 and 100 characters long.");
        RuleFor(model => model.Author).Length(6, 100).WithMessage("The Description property must be between 1 and 100 characters long.");
        RuleFor(model => model.Genre).NotEmpty().WithMessage("The Genre property must not be empty.").Length(6, 100).WithMessage("The Genre property must be between 1 and 100 characters long.");
        RuleFor(model => model.UserId).NotEmpty().WithMessage("The UserId property must not be empty.");
    }
}
