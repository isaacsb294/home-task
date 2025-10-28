using FluentValidation;

namespace Application.ChoreLists.DeleteChoreList;

public class DeleteChoreListCommandValidator : AbstractValidator<DeleteChoreListCommand>
{
    public DeleteChoreListCommandValidator()
    {
        RuleFor(x => x.ChoreListId).NotEmpty();
    }
}