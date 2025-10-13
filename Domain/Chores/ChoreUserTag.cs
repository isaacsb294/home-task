using Shared;

namespace Domain.Chores;

public class ChoreUserTag : Entity
{
    public Guid ChoreId { get; init; }
    public Guid UserId { get; init; }
}