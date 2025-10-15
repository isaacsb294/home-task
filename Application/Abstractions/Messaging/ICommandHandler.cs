namespace Application.Abstractions.Messaging;

public interface ICommandHandler<in TCommand, TCommandResponse> 
    where TCommand : ICommand
{
    Task<TCommandResponse> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}