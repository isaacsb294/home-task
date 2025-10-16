using Shared;

namespace Application.Abstractions.Messaging;

public interface ICommandHandler<in TCommand> 
    where TCommand : ICommand
{
    Task<Result> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}

public interface ICommandHandler<in TCommand, TCommandResponse> 
    where TCommand : ICommand
{
    Task<Result<TCommandResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}