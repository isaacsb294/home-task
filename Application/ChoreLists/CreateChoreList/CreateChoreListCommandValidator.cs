using FluentValidation;

namespace Application.ChoreLists.CreateChoreList;

public class CreateChoreListCommandValidator : AbstractValidator<CreateChoreListCommand>
{
    public CreateChoreListCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
    }
}