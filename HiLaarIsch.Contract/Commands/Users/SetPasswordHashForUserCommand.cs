using System;

namespace HiLaarIsch.Contract.Commands
{
    public class SetPasswordHashForUserCommand
    {
        public SetPasswordHashForUserCommand(Guid userId, string passwordHash)
        {
            this.UserId = userId;
            this.PasswordHash = passwordHash;
        }

        public Guid UserId { get; }
        public string PasswordHash { get; }

    }
}
