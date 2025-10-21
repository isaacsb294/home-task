using Application.Abstractions.Messaging;
using Application.Users;
using Application.Users.GetSelf;
using Application.Users.LoginUser;
using Application.Users.RefreshUserToken;
using Application.Users.RegisterUser;
using HomeTask.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace HomeTask.Api.Controllers.Users;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    [Authorize(Roles = Roles.Registered)]
    [HttpGet("self")]
    public async Task<ActionResult<UserDto>> GetSelf(
        [FromServices] IQueryHandler<GetSelfQuery, UserDto> queryHandler,
        CancellationToken cancellationToken)
    {
        var query = new GetSelfQuery();

        Result<UserDto> result = await queryHandler.HandleAsync(query, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<Guid>> Register(
        [FromBody] RegisterUserRequest request,
        [FromServices] ICommandHandler<RegisterUserCommand, Guid> commandHandler,
        CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(
            request.Email,
            request.FirstName,
            request.LastName,
            request.Password);

        Result<Guid> result = await commandHandler.HandleAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        return Ok(result.Value);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AccessTokenDto>> Login(
        [FromBody] LoginRequest request,
        [FromServices] ICommandHandler<LoginUserCommand, AccessTokenResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        var command = new LoginUserCommand(request.Email, request.Password);

        Result<AccessTokenResponse> result = await commandHandler.HandleAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        HttpContext.Response.Cookies.SetRefreshToken(
            result.Value.RefreshToken, 
            result.Value.RefreshTokenExpiresIn);

        return Ok(new AccessTokenDto(result.Value.AccessToken));
    }
    
    [HttpPost("refresh")]
    public async Task<ActionResult<AccessTokenDto>> Refresh(
        ICommandHandler<RefreshUserTokenCommand, AccessTokenResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        string? cookiesRefreshToken = HttpContext.Request.Cookies["refreshToken"];

        if (cookiesRefreshToken is null)
        {
            return Unauthorized();
        }

        var command = new RefreshUserTokenCommand(cookiesRefreshToken);

        Result<AccessTokenResponse> result = await commandHandler.HandleAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            return Unauthorized(result.Error);
        }
        
        HttpContext.Response.Cookies.SetRefreshToken(
            result.Value.RefreshToken,
            result.Value.RefreshTokenExpiresIn);

        return Ok(new AccessTokenDto(result.Value.AccessToken));
    }
}