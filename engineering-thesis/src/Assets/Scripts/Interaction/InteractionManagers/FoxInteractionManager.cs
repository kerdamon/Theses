using Interaction.FoxInteractions;
using Unity.MLAgents;
using UnityEngine;

namespace Interaction.InteractionManagers
{
    public class FoxInteractionManager : InteractionManager
    {
        private EatingRabbitInteraction _eatingRabbitInteraction;
        
        protected override void Start()
        {
            _eatingRabbitInteraction = GetComponent<EatingRabbitInteraction>();
            var fox_eating_rabbit_reward = Academy.Instance.EnvironmentParameters.GetWithDefault("fox_eating_rabbit_reward", 0.0f);

            AddRewardAfterInteraction(_eatingRabbitInteraction, fox_eating_rabbit_reward);
            RegisterUpdatingCurrentInteractionAfterEndOf(_eatingRabbitInteraction);
            
            base.Start();
        }
    }
}