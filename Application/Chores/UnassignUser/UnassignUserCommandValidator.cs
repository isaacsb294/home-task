using FluentValidation;

namespace Application.Chores.UnassignUser;

public class UnassignUserCommandValidator : AbstractValidator<UnassignUserCommand>
{
    public UnassignUserCommandValidator()
    {
        RuleFor(c => c.ChoreId).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
    }
}