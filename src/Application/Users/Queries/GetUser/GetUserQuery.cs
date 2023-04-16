using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Application.Users.DTOs;
using MediatR;

namespace CyberWork.Accounting.Application.Users.Queries.GetUser;

public record GetUserQuery(Guid Id) : IRequest<Result<UserDto>>;