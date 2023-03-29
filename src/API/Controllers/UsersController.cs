using CyberWork.Accounting.Application.Users.Commands.CreateUser;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult>
        CreateOrganization([FromBody] CreateUserCommand user)
    {
        var result = await Mediator.Send(user);
        return HandleResult(result);
    }
}