using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using QuantumHive.Core;

namespace HiLaarIsch.Services
{
    public class PromptableMvcCommandHandler<TCommand> : IPromptableMvcCommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> commandHandler;

        public PromptableMvcCommandHandler(
            ICommandHandler<TCommand> commandHandler)
        {
            this.commandHandler = commandHandler;
        }

        public ActionResult Handle(TCommand command, Func<ActionResult> successAction, Func<Exception, ActionResult> errorAction)
        {
            try
            {
                this.commandHandler.Handle(command);
                return successAction.Invoke();
            }
            catch (ValidationException validationException)
            {
                return errorAction.Invoke(validationException);
            }
            catch (Exception exception)
            {
                return errorAction.Invoke(exception);
            }
        }
    }
}