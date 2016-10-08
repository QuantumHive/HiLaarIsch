namespace QuantumHive.Core
{
    public interface ICommandHandler<TCommand>
    {
        void Handle(TCommand command);
    }
}
