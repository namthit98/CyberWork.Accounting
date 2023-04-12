using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Mappings;

namespace CyberWork.Accounting.Application.Roles.DTOs;

public class RoleDto : IMapFrom<IAppRole>
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
}