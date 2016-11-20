namespace HiLaarIsch.Contract.Commands
{
    public class ConfirmEmailForUserCommand
    {
        public ConfirmEmailForUserCommand(int userId)
        {
            this.UserId = userId;
        }

        public int UserId { get; }
    }
}
