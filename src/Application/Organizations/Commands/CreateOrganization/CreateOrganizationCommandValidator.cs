using FluentValidation;

namespace CyberWork.Accounting.Application.Organizations.Commands.CreateOrganization;

public class CreateOrganizationCommandValidator
    : AbstractValidator<CreateOrganizationCommand>
{
    public CreateOrganizationCommandValidator()
    {
        RuleFor(v => v.Code)
            .MaximumLength(200)
            .NotEmpty()
            .NotNull();

        RuleFor(v => v.Name)
            .MaximumLength(200)
            .NotEmpty()
            .NotNull();

        RuleFor(v => v.UnderOrganizationId)
            .NotEmpty()
            .NotNull();

        RuleFor(v => v.OrganizationLevel)
            .NotEmpty()
            .NotNull();
    }
}