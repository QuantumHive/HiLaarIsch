namespace QuantumHive.Core
{
    public interface IApplicationDeployment
    {
        ApplicationPhase Phase { get; }
    }

    public enum ApplicationPhase : byte
    {
        //0 reserved
        Test = 2,
        Production = 4,
    }
}
