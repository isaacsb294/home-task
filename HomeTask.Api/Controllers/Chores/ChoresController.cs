using Application.Abstractions.Messaging;
using Application.Chores;
using Application.Chores.SearchChores;
using Domain.Chores;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace HomeTask.Api.Controllers.Chores;

[ApiController]
[Route("api/chores")]
public class ChoresController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginationResult<Chore>>> GetAsync(
        [FromQuery]
        SearchChoresRequest request,
        [FromServices]
        IQueryHandler<SearchChoresQuery, PaginationResult<ChoreDto>> queryHandler,
        CancellationToken cancellationToken)
    {
        var query = new SearchChoresQuery(
            request.Search,
            request.Priority,
            request.Frequency,
            request.Category,
            request.DayOfWeek,
            request.PaginationParams);

        Result<PaginationResult<ChoreDto>> result = await queryHandler.HandleAsync(query, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}