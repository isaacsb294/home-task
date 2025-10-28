using FluentValidation;

namespace Application.Chores.EditChore;

public class EditChoreCommandValidator : AbstractValidator<EditChoreCommand>
{
    public EditChoreCommandValidator()
    {
        RuleFor(x => x.ChoreId).NotEmpty();
        
        RuleFor(x => x.Priority).IsInEnum().When(p => p is not null);
        RuleFor(x => x.Frequency).IsInEnum().When(f => f is not null);
        RuleFor(x => x.Category).IsInEnum().When(c => c is not null);
        RuleFor(x => x.DayOfWeek).IsInEnum().When(dw => dw is not null);
    }
}