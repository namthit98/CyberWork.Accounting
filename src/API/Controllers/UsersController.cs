using CyberWork.Accounting.Application.Users.Commands.CreateUser;
using CyberWork.Accounting.Application.Users.Commands.DeleteUser;
using CyberWork.Accounting.Application.Users.Commands.UpdateUser;
using CyberWork.Accounting.Application.Users.Queries.GetUser;
using CyberWork.Accounting.Application.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersQuery query)
    {
        var result = await Mediator.Send(query);
        return HandlePagedResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var query = new GetUserQuery(id);

        var result = await Mediator.Send(query);

        return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand user)
    {
        var result = await Mediator.Send(user);
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var result = await Mediator.Send(new DeleteUserCommand(id));
        return HandleResult(result);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserCommand user)
    {
        user.SetId(id);
        var result = await Mediator.Send(user);

        return HandleResult(result);
    }
}