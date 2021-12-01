namespace Pidp.Features
{
    public interface ICommandHandler<TCommand> : IRequestHandler where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }

    public interface ICommandHandler<TCommand, TResult> : IRequestHandler where TCommand : ICommand<TResult>
    {
        Task<TResult> HandleAsync(TCommand command);
    }
}
