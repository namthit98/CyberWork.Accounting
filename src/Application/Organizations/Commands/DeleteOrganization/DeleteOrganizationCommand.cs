using CyberWork.Accounting.Application.Common.Models;
using MediatR;

namespace CyberWork.Accounting.Application.Organizations.Commands.DeleteOrganization;

public record DeleteOrganizationCommand(Guid Id) : IRequest<Result<Unit>>;