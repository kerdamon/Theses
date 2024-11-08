using Interaction;
using Interaction.RabbitInteractions;
using Unity.MLAgents;
using UnityEngine;

namespace Interaction.InteractionManagers
{
    public class MaleRabbitInteractionManager : RabbitInteractionManager 
    {

        protected override void Start()
        {
            var rabbit_mating_reward = Academy.Instance.EnvironmentParameters.GetWithDefault("rabbit_mating_reward", 0.0f);
            
            MatingInteraction = GetComponent<MatingInteraction>();
            AddRewardAfterInteraction(MatingInteraction, rabbit_mating_reward);
            RegisterUpdatingCurrentInteractionAfterEndOf(MatingInteraction); 
            
            base.Start();
        }
    }
}