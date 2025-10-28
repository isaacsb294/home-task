using Application.Abstractions.Messaging;
using Application.Chores;
using Application.Chores.AddProduct;
using Application.Chores.AssignUser;
using Application.Chores.CreateChore;
using Application.Chores.EditChore;
using Application.Chores.GetChore;
using Application.Chores.RemoveProduct;
using Application.Chores.SearchChores;
using Application.Chores.UnassignUser;
using Domain.Chores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace HomeTask.Api.Controllers.Chores;

[Authorize]
[ApiController]
[Route("api/chores")]
public class ChoresController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginationResult<Chore>>> Search(
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

    [HttpGet("{choreId:guid}")]
    public async Task<ActionResult<ChoreDto>> GetById(
        [FromRoute]
        Guid choreId,
        [FromServices]
        IQueryHandler<GetChoreQuery, ChoreDto> queryHandler,
        CancellationToken cancellationToken) 
    {
        var query = new GetChoreQuery(choreId);
        
        Result<ChoreDto> result = await queryHandler.HandleAsync(query, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromBody] CreateChoreRequest request,
        [FromServices] ICommandHandler<CreateChoreCommand, Guid> commandHandler,
        CancellationToken cancellationToken)
    {
        var command = new CreateChoreCommand(
            request.ChoreListId,
            request.Name,
            request.Description,
            request.DayOfWeek,
            request.Priority,
            request.Frequency,
            request.Category);

        Result<Guid> result = await commandHandler.HandleAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        return CreatedAtAction(nameof(GetById), new { choreId = result.Value }, result.Value);
    }

    [HttpPut("{choreId:guid}")]
    public async Task<IActionResult> Edit(
        [FromRoute] Guid choreId,
        [FromBody] EditChoreRequest request,
        [FromServices] ICommandHandler<EditChoreCommand> commandHandler,
        CancellationToken cancellationToken)
    {
        var command = new EditChoreCommand(
            choreId,
            request.Name,
            request.Description,
            request.DayOfWeek,
            request.Priority,
            request.Frequency,
            request.Category);

        Result result = await commandHandler.HandleAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }

    [HttpPost("{choreId:guid}/assignees")]
    public async Task<IActionResult> AssignUser(
        [FromRoute] Guid choreId,
        [FromBody] AssignUserRequest request,
        [FromServices] ICommandHandler<AssignUserCommand> commandHandler,
        CancellationToken cancellationToken)
    {
        var command = new AssignUserCommand(choreId, request.UserId);

        Result result = await commandHandler.HandleAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }

    [HttpDelete("{choreId:guid}/assignees/{userId:guid}")]
    public async Task<IActionResult> UnassignUser(
        [FromRoute] Guid choreId,
        [FromRoute] Guid userId,
        [FromServices] ICommandHandler<UnassignUserCommand> commandHandler,
        CancellationToken cancellationToken)
    {
        var command = new UnassignUserCommand(choreId, userId);

        Result result = await commandHandler.HandleAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }

    [HttpPost("{choreId:guid}/products")]
    public async Task<ActionResult<Guid>> AddProduct(
        [FromRoute] Guid choreId,
        [FromBody] AddProductRequest request,
        [FromServices] ICommandHandler<AddProductCommand, Guid> commandHandler,
        CancellationToken cancellationToken)
    {
        var command = new AddProductCommand(
            choreId,
            request.Name,
            request.Link,
            request.PriceAmount,
            request.PriceCurrencyCode);

        Result<Guid> result = await commandHandler.HandleAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(GetById), new { choreId }, result.Value);
    }

    [HttpDelete("{choreId:guid}/products/{productId:guid}")]
    public async Task<IActionResult> RemoveProduct(
        [FromRoute] Guid choreId,
        [FromRoute] Guid productId,
        [FromServices] ICommandHandler<RemoveProductCommand> commandHandler,
        CancellationToken cancellationToken)
    {
        var command = new RemoveProductCommand(choreId, productId);

        Result result = await commandHandler.HandleAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }
}