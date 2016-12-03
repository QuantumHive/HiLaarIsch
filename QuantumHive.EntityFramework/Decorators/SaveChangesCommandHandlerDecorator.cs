using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
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


            try
            {
                this.databaseContext.SaveChanges();
            }
            catch (DbEntityValidationException entityValidationException)
            {
                var errorMessages = entityValidationException.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);

                //TODO: rethrow with validationerror as innerexception + full stacktrace
                throw;
            }
        }
    }
}
