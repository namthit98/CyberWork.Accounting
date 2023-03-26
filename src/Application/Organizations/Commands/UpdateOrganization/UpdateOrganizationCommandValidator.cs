using FluentValidation;

namespace CyberWork.Accounting.Application.Organizations.Commands.UpdateOrganization;

public class UpdateOrganizationCommandValidator
    : AbstractValidator<UpdateOrganizationCommand>
{
    public UpdateOrganizationCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(200);
    }
}