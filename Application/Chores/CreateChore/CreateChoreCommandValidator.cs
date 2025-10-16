using FluentValidation;

namespace Application.Chores.CreateChore;

public class CreateChoreCommandValidator : AbstractValidator<CreateChoreCommand>
{
    public CreateChoreCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.ChoreListId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();

        RuleFor(x => x.Priority).IsInEnum();
        RuleFor(x => x.Frequency).IsInEnum();
        RuleFor(x => x.Category).IsInEnum();
        RuleFor(x => x.DayOfWeek).IsInEnum();
    }
}