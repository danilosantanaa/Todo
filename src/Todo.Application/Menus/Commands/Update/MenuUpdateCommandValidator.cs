using FluentValidation;

namespace Todo.Application.Menus.Commands.Update;

public sealed class MenuUpdateCommandValidator : AbstractValidator<MenuUpdateCommand>
{
    public MenuUpdateCommandValidator()
    {
        RuleFor(command => command.Nome)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(command => command.IconUrl)
            .MaximumLength(200);
    }
}