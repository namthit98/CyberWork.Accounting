using CyberWork.Accounting.Application.Common.Models;
using MediatR;

namespace CyberWork.Accounting.Application.Organizations.Commands.CreateOrganization;

public class CreateOrganizationCommand : IRequest<Result<Guid>>
{
    public string Code { get; init; }
    public string Name { get; init; }
    public string ShortName { get; init; } = string.Empty;
    public Guid UnderOrganizationId { get; init; }
    public string OrganizationLevel { get; init; }
    public string Address { get; init; } = string.Empty;
}