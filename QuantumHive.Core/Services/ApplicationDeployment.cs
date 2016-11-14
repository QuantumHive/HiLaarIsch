namespace QuantumHive.Core.Services
{
    public class ApplicationDeployment : IApplicationDeployment
    {
        public ApplicationDeployment(ApplicationPhase phase)
        {
            this.Phase = phase;
        }

        public ApplicationPhase Phase { get; }
    }
}
