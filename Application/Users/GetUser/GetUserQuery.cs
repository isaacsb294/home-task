using Application.Abstractions.Messaging;

namespace Application.Users.GetUser;

public record GetUserQuery(Guid UserId) : IQuery;