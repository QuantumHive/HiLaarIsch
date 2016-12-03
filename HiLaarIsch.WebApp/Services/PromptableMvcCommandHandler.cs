using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
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
            //TODO: logging
            try
            {
                this.commandHandler.Handle(command);
                return successAction.Invoke();
            }
            catch (ValidationException validationException)
            {
                HttpContext.Current.Items.Add("validation-result", validationException);
                return errorAction.Invoke(validationException);
            }
            catch (Exception exception)
            {
                return errorAction.Invoke(exception);
            }
        }
    }
}