using API.Extensions;
using CyberWork.Accounting.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender _mediator;

    protected ISender Mediator =>
        _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (result == null) return NotFound();

        if (result.IsSuccess && result.Value != null)
            return Ok(result.Value);

        if (result.IsSuccess && result.Value == null)
            return NotFound();

        return BadRequest(result.Error);
    }

    protected ActionResult HandlePagedResult<T>(Result<PaginatedList<T>> result)
    {
        if (result == null) return NotFound();
        if (result.IsSuccess && result.Value != null)
        {
            Response.AddPaginationHeader(result.Value.PageNumber, result.Value.PageSize,
                result.Value.TotalCount, result.Value.TotalPages);
            return Ok(result.Value);
        }

        if (result.IsSuccess && result.Value == null)
            return NotFound();
        return BadRequest(result.Error);
    }
}
