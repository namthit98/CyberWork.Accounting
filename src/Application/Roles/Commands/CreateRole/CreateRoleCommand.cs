using CyberWork.Accounting.Application.Common.Models;
using MediatR;

namespace CyberWork.Accounting.Application.Roles.Commands.CreateRole;

public record CreateRoleCommand : IRequest<Result<Guid>>
{
    public string Name { get; init; }
    public string Description { get; init; }
}