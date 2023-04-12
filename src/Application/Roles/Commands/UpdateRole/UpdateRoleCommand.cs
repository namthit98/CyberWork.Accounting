using CyberWork.Accounting.Application.Common.Models;
using MediatR;

namespace CyberWork.Accounting.Application.Roles.Commands.UpdateRole;

public record UpdateRoleCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; private set; }
    public void SetId(Guid id)
    {
        Id = id;
    }
    public string Name { get; init; }
    public string Description { get; init; }
}