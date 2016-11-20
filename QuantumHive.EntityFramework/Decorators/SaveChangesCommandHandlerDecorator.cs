using System.Data.Entity;
using QuantumHive.Core;

namespace QuantumHive.EntityFramework.Decorators
{
    public class SaveChangesCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> decoratee;
        private readonly DbContext databaseContext;

        public SaveChangesCommandHandlerDecorator(
            ICommandHandler<TCommand> decoratee,
            DbContext databaseContext)
        {
            this.decoratee = decoratee;
            this.databaseContext = databaseContext;
        }

        public void Handle(TCommand command)
        {
            this.decoratee.Handle(command);

            //TODO: try/catch
            this.databaseContext.SaveChanges();
        }
    }
}
