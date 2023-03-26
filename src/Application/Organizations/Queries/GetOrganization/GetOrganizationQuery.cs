
using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Application.Organizations.DTOs;
using MediatR;

namespace CyberWork.Accounting.Application.Organizations.Queries.GetOrganization;

public record GetOrganizationQuery(Guid id) : IRequest<Result<OrganizationDto>>;