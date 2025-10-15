using Shared;

namespace Domain.ChoreInstances;

public static class ChoreInstanceErrors
{
    public static readonly Error CommentNotExists = new(
        "ChoreInstance.CommentNotExists",
        "The comment does not exist.");

    public static readonly Error AlreadyComplete = new(
        "ChoreInstance.AlreadyComplete",
        "Chore instance has already been completed.");
    
    public static readonly Error AlreadyIncomplete = new(
        "ChoreInstance.AlreadyIncomplete",
        "Chore instance is already incomplete.");
}