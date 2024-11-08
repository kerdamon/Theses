using UnityEngine;

namespace Interaction
{
    public class TrainingDrinkingInteraction : DrinkingInteraction
    {
        [SerializeField] private TrainingArea trainingArea;
        
        protected override void AtInteractionEnd()
        {
            var agent = SimulationObject.transform;
            var agentContainer = agent.parent;
            trainingArea.RandomizePositionAndRotationWithCollisionCheck(agent, agentContainer);
        }
    }
}