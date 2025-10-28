using FluentValidation;

namespace Application.ChoreLists.EditChoreList;

public class EditChoreListCommandValidator : AbstractValidator<EditChoreListCommand>
{
    public EditChoreListCommandValidator()
    {
        RuleFor(cl => cl.ChoreListId).NotEmpty();
    }
}