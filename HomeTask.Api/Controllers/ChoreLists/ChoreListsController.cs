using Application.Abstractions.Messaging;
using Application.ChoreLists;
using Application.ChoreLists.CreateChoreList;
using Application.ChoreLists.DeleteChoreList;
using Application.ChoreLists.EditChoreList;
using Application.ChoreLists.GetChoreList;
using Application.ChoreLists.GetChoreLists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace HomeTask.Api.Controllers.ChoreLists;

[Authorize]
[ApiController]
[Route("api/choreLists")]
public class ChoreListsController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ChoreListDto>>> GetChoreLists(
        IQueryHandler<GetChoreListsQuery, List<ChoreListDto>> queryHandler,
        CancellationToken cancellationToken)
    {
        var query = new GetChoreListsQuery();

        Result<List<ChoreListDto>> result = await queryHandler.HandleAsync(query, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("{choreListId:guid}")]
    public async Task<ActionResult<List<ChoreListDto>>> GetChoreList(
        [FromRoute] Guid choreListId,
        [FromServices] IQueryHandler<GetChoreListQuery, ChoreListDto> queryHandler,
        CancellationToken cancellationToken)
    {
        var query = new GetChoreListQuery(choreListId);

        Result<ChoreListDto> result = await queryHandler.HandleAsync(query, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromBody] CreateChoreListRequest request,
        [FromServices] ICommandHandler<CreateChoreListCommand, Guid> commandHandler,
        CancellationToken cancellationToken)
    {
        var command = new CreateChoreListCommand(request.Name, request.Description);

        Result<Guid> result = await commandHandler.HandleAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(GetChoreList), new { choreListId = result.Value }, result.Value);
    }

    [HttpPut("{choreListId:guid}")]
    public async Task<IActionResult> Edit(
        [FromRoute] Guid choreListId,
        [FromBody] EditChoreListRequest request,
        [FromServices] ICommandHandler<EditChoreListCommand> commandHandler,
        CancellationToken cancellationToken)
    {
        var command = new EditChoreListCommand(choreListId, request.Name, request.Description);

        Result result = await commandHandler.HandleAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }

    [HttpDelete("{choreListId:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid choreListId,
        [FromServices] ICommandHandler<DeleteChoreListCommand> commandHandler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteChoreListCommand(choreListId);

        Result result = await commandHandler.HandleAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }
}