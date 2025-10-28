using FluentValidation;

namespace Application.Chores.CreateChore;

public class CreateChoreCommandValidator : AbstractValidator<CreateChoreCommand>
{
    public CreateChoreCommandValidator()
    {
        RuleFor(x => x.ChoreListId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.DayOfWeek).NotNull().IsInEnum();

        RuleFor(x => x.Priority).IsInEnum().When(p => p is not null);
        RuleFor(x => x.Frequency).IsInEnum().When(f => f is not null);
        RuleFor(x => x.Category).IsInEnum().When(c => c is not null);
    }
}