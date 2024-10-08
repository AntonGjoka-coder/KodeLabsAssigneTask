using FluentValidation;

namespace Application.DataSources.Create;

public class CreateDataSourceCommandValidator : AbstractValidator<CreateDataSourceCommand>
{
    public CreateDataSourceCommandValidator()
    {
        RuleFor(x=> x.Name).NotEmpty().WithMessage("Name cannot be empty")
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters");
        RuleFor(x=> x.Description).NotEmpty().WithMessage("Name cannot be empty")
            .MaximumLength(200).WithMessage("Name cannot exceed 50 characters");
    }
}