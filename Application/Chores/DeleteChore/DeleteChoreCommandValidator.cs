using FluentValidation;

namespace Application.Chores.DeleteChore;

public class DeleteChoreCommandValidator : AbstractValidator<DeleteChoreCommand>
{
    public DeleteChoreCommandValidator()
    {
        RuleFor(x => x.ChoreId).NotEmpty();
    }
}