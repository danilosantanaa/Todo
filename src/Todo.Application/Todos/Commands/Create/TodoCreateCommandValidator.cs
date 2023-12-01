using FluentValidation;

using Todo.Domain.Todos.Enums;

namespace Todo.Application.Todos.Commands.Create;

public sealed class TodoCreateCommandValidator : AbstractValidator<TodoCreateCommand>
{
    public TodoCreateCommandValidator()
    {
        RuleFor(command => command.Descricao)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(command => command.Tipo)
            .NotEmpty();

        RuleFor(command => command.RepeticaoTipo)
            .NotEmpty();

        RuleFor(command => command.MenuId)
            .NotEmpty();

        RuleFor(command => command.TodoEtapas)
            .NotNull()
            .When(x => x.Tipo.Equals(TodoTipo.Etapa));
    }
}

public sealed class TodoEtapaCommandValidator : AbstractValidator<TodoEtapaCreateCommand>
{
    public TodoEtapaCommandValidator()
    {
        RuleFor(command => command.Descricao)
            .NotEmpty()
            .MaximumLength(200);
    }
}