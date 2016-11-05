namespace QuantumHive.Core
{
    public interface IAuthenticationManager<TUser>
        where TUser : class
    {
        void SignIn(TUser user);
        void SignOut();
    }
}
