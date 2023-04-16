using CyberWork.Accounting.Application.Common.Models;
using MediatR;

namespace CyberWork.Accounting.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id) : IRequest<Result<Guid>>;
