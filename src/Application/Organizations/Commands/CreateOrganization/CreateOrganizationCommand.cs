using AutoMapper;
using CyberWork.Accounting.Application.Common.Mappings;
using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Domain.Entities;
using MediatR;

namespace CyberWork.Accounting.Application.Organizations.Commands.CreateOrganization;

public record CreateOrganizationCommand : IRequest<Result<Guid>>, IMapFrom<Organization>
{
    public string Code { get; init; }
    public string Name { get; init; }
    public string ShortName { get; init; } = string.Empty;
    public Guid UnderOrganizationId { get; init; }
    public string OrganizationLevel { get; init; }
    public string Address { get; init; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateOrganizationCommand, Organization>();
    }
}