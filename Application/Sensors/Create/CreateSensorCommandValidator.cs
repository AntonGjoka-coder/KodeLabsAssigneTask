using FluentValidation;

namespace Application.Sensors.Create;

public class CreateSensorCommandValidator : AbstractValidator<CreateSensorCommand>
{
    public CreateSensorCommandValidator()
    {
        RuleFor(x=> x.Name).NotEmpty().WithMessage("Name cannot be empty");
    }
}