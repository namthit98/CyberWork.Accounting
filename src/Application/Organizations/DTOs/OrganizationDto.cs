using CyberWork.Accounting.Application.Common.Mappings;
using CyberWork.Accounting.Domain.Entities;

namespace CyberWork.Accounting.Application.Organizations.DTOs;

public class OrganizationDto : IMapFrom<Organization>
{
    public Guid Id { get; init; }
    public string Code { get; init; }
    public string Name { get; init; }
    public string ShortName { get; init; } = string.Empty;
    public Guid UnderOrganizationId { get; set; }
    public string OrganizationLevel { get; init; }
    public string Address { get; init; }
    public int Status { get; init; }
}