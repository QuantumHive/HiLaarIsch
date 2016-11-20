namespace HiLaarIsch.Contract.Commands
{
    public class SetPasswordHashForUserCommand
    {
        public SetPasswordHashForUserCommand(int userId, string passwordHash)
        {
            this.UserId = userId;
            this.PasswordHash = passwordHash;
        }

        public int UserId { get; }
        public string PasswordHash { get; }

    }
}
