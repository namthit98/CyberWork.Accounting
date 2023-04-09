using FluentValidation;

namespace CyberWork.Accounting.Application.Organizations.Commands.UpdateStatusOrganization;

public class UpdateStatusOrganizationCommandValidator
    : AbstractValidator<UpdateStatusOrganizationCommand>
{
    public UpdateStatusOrganizationCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty()
            .NotNull();

        RuleFor(v => v.Status)
            .IsInEnum();
    }
}