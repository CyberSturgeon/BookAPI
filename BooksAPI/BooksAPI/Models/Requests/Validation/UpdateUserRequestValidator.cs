using FluentValidation;
using BooksAPI.Models.Requests;

namespace BooksAPI.Models.Requests.Validation;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(model => model.Name).NotEmpty().WithMessage("The Name property must not be empty.").Length(8, 15).WithMessage("The Name property must be between 8 and 15 characters long.");
        RuleFor(model => model.Email).NotEmpty().WithMessage("The Email property must not be empty.").EmailAddress().WithMessage("The Email property must be Email.");
        RuleFor(model => model.Password).NotEmpty().WithMessage("The Password property must not be empty.").Length(8, 15).WithMessage("The Password property must be between 8 and 15 characters long.");
    }
}
