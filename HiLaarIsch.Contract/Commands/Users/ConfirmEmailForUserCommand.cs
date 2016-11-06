using System;

namespace HiLaarIsch.Contract.Commands
{
    public class ConfirmEmailForUserCommand
    {
        public ConfirmEmailForUserCommand(Guid userId)
        {
            this.UserId = userId;
        }

        public Guid UserId { get; }
    }
}
