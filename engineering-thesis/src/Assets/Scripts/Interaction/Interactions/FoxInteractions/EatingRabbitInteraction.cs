using Interaction.InteractionManagers;

namespace Interaction.FoxInteractions
{
    public class EatingRabbitInteraction : Interaction
    {
        private Needs _needs;
    
        protected override void Start()
        {
            base.Start();
            _needs = SimulationObject.GetComponent<Needs>();
        }
        protected override void AtInteractionEnd()
        {
            var rabbit = SecondSimulationObject.GetComponentInChildren<MovementAgent>();
            rabbit.KillAgent(DeathCause.Eaten);
            _needs["Hunger"] = 0;
        }
    }
}