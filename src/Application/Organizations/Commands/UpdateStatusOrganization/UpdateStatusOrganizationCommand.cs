using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Domain.Enums;
using MediatR;

namespace CyberWork.Accounting.Application.Organizations.Commands.UpdateStatusOrganization;

public record UpdateStatusOrganizationCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; private set; }

    public void SetId(Guid id)
    {
        Id = id;
    }
    public Status Status { get; set; }
}