using AutoMapper;
using CyberWork.Accounting.Application.Common.Mappings;
using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Domain.Entities;
using MediatR;

namespace CyberWork.Accounting.Application.Organizations.Commands.UpdateOrganization;

public record UpdateOrganizationCommand : IRequest<Result<Guid>>, IMapFrom<Organization>
{
    public Guid Id { get; private set; }

    public void SetId(Guid id)
    {
        Id = id;
    }
    public string Name { get; set; }
    public string ShortName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateOrganizationCommand, Organization>()
            .IgnoreAllNonExisting();
    }
}