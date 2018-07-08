namespace Shift.Domain.Core.Interfaces
{
    public interface IHandler<T> where T : ICommandResult
    {
        ICommandResult Handle(T command);
    }
}
