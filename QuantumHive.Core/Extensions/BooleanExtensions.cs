namespace QuantumHive.Core.Extensions
{
    public static class BooleanExtensions
    {
        public static string ToLowerString(this bool boolean)
        {
            return boolean.ToString().ToLower();
        }
    }
}
