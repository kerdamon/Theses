using Interaction.InteractionManagers;
using Unity.MLAgents;
using UnityEngine;

namespace Interaction.FoxInteractions
{
    public class TrainingEatingRabbitInteraction : EatingRabbitInteraction
    {
        [SerializeField] private TrainingArea trainingArea;

        private float rabbit_on_eaten;
        
        protected override void Start()
        {
            base.Start();
            rabbit_on_eaten = Academy.Instance.EnvironmentParameters.GetWithDefault("rabbit_on_eaten", 0.0f);
        }

        protected override void AtInteractionEnd()
        {
            var rabbit = SecondSimulationObject.transform;
            var rabbitContainer = rabbit.parent;
            trainingArea.RandomizePositionAndRotationWithCollisionCheck(rabbit, rabbitContainer);

            if (Mathf.Abs(rabbit_on_eaten) < 0.00001f) return;
            rabbit.GetComponent<MovementAgent>().AddReward(rabbit_on_eaten);
            Debug.Log($"Added reward of value {rabbit_on_eaten} after rabbit was eaten", this);
        }
    }
}