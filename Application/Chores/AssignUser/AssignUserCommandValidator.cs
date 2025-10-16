using FluentValidation;

namespace Application.Chores.AssignUser;

public class AssignUserCommandValidator : AbstractValidator<AssignUserCommand>
{
    public AssignUserCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.ChoreId).NotEmpty();
    }
}