using FluentValidation;

using Todo.Domain.Todos.Enums;

namespace Todo.Application.Todos.Commands.Update;

public sealed class TodoUpdateCommandValidator : AbstractValidator<TodoUpdateCommand>
{
    public TodoUpdateCommandValidator()
    {
        RuleFor(command => command.Descricao)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(command => command.Tipo)
            .NotEmpty();

        RuleFor(command => command.RepeticaoTipo)
            .NotEmpty();

        RuleFor(command => command.TodoEtapas)
            .NotNull()
            .When(x => x.Tipo.Equals(TodoTipo.Etapa));
    }
}

public sealed class TodoEtapaCommandValidator : AbstractValidator<TodoEtapaUpdateCommand>
{
    public TodoEtapaCommandValidator()
    {
        RuleFor(command => command.Descricao)
            .NotEmpty()
            .MaximumLength(200);
    }
}