using CyberWork.Accounting.Application.Roles.Commands.CreateRole;
using CyberWork.Accounting.Application.Roles.Commands.DeleteRole;
using CyberWork.Accounting.Application.Roles.Commands.UpdateRole;
using CyberWork.Accounting.Application.Roles.Queries.GetRole;
using CyberWork.Accounting.Application.Roles.Queries.GetRoles;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class RolesController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetRoles([FromQuery] GetRolesQuery query)
    {
        var result = await Mediator.Send(query);
        return HandlePagedResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRole(Guid id)
    {
        var query = new GetRoleQuery(id);

        var result = await Mediator.Send(query);

        return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult>
        CreateRole([FromBody] CreateRoleCommand role)
    {
        var result = await Mediator.Send(role);
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(Guid id)
    {
        var result = await Mediator.Send(new DeleteRoleCommand(id));
        return HandleResult(result);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRoleCommand role)
    {
        role.SetId(id);
        var result = await Mediator.Send(role);

        return HandleResult(result);
    }
}