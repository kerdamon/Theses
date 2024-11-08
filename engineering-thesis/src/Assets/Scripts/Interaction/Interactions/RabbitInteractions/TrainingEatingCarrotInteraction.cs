using UnityEngine;

namespace Interaction.RabbitInteractions
{
    public class TrainingEatingCarrotInteraction : EatingCarrotInteraction
    {
        protected override void AtInteractionEnd()
        {
            Destroy(SecondSimulationObject);
        }
    }
}