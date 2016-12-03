using System;
using System.Web.Mvc;

namespace HiLaarIsch
{
    public interface IPromptableMvcCommandHandler<TCommand>
    {
        ActionResult Handle(TCommand command, Func<ActionResult> successAction, Func<Exception, ActionResult> errorAction);
    }
}
