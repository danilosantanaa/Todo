using FluentValidation;

namespace Todo.Application.Menus.Commands.Create;

public sealed class MenuCreateCommandValidator : AbstractValidator<MenuCreateCommand>
{
    public MenuCreateCommandValidator()
    {
        RuleFor(command => command.Nome)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(command => command.IconUrl)
            .MaximumLength(200);
    }
}