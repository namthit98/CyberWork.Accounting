using CyberWork.Accounting.Application.Organizations.Commands.CreateOrganization;
using CyberWork.Accounting.Application.Organizations.Commands.DeleteOrganization;
using CyberWork.Accounting.Application.Organizations.Commands.UpdateOrganization;
using CyberWork.Accounting.Application.Organizations.Commands.UpdateStatusOrganization;
using CyberWork.Accounting.Application.Organizations.Queries.GetAllOrganizations;
using CyberWork.Accounting.Application.Organizations.Queries.GetOrganization;
using CyberWork.Accounting.Application.Organizations.Queries.GetOrganizations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class OrganizationsController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult>
        GetOrganizations([FromQuery] GetOrganizationsQuery query)
    {
        var result = await Mediator.Send(query);
        return HandlePagedResult(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult>
        GetAllOrganizations([FromQuery] GetAllOrganizationsQuery query)
    {
        var result = await Mediator.Send(query);
        return HandleResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult>
        GetOrganization(Guid id)
    {
        var query = new GetOrganizationQuery(id);

        var result = await Mediator.Send(query);

        return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult>
        CreateOrganization([FromBody] CreateOrganizationCommand organization)
    {
        var result = await Mediator.Send(organization);
        return HandleResult(result);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult>
        Update(Guid id, [FromBody] UpdateOrganizationCommand organization)
    {
        organization.SetId(id);
        var result = await Mediator.Send(organization);

        return HandleResult(result);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult>
        UpdateStatus(Guid id, [FromBody] UpdateStatusOrganizationCommand organization)
    {
        organization.SetId(id);
        var result = await Mediator.Send(organization);

        return HandleResult(result);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrganization(Guid id)
    {
        var result = await Mediator.Send(new DeleteOrganizationCommand(id));
        return HandleResult(result);
    }
}