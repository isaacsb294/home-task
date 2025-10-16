using FluentValidation;

namespace Application.Chores.EditChore;

public class EditChoreCommandValidator : AbstractValidator<EditChoreCommand>
{
    public EditChoreCommandValidator()
    {
        RuleFor(x => x.ChoreId).NotEmpty();
        
        RuleFor(x => x.Priority).IsInEnum();
        RuleFor(x => x.Frequency).IsInEnum();
        RuleFor(x => x.Category).IsInEnum();
        RuleFor(x => x.DayOfWeek).IsInEnum();
    }
}