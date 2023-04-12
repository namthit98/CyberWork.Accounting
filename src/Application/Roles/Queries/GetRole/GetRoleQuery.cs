using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Application.Roles.DTOs;
using MediatR;

namespace CyberWork.Accounting.Application.Roles.Queries.GetRole;

public record GetRoleQuery(Guid Id) : IRequest<Result<RoleDto>>;