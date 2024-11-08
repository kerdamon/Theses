using Interaction;
using Interaction.RabbitInteractions;
using Unity.MLAgents;
using UnityEngine;

namespace Interaction.InteractionManagers
{
    public class MaleFoxInteractionManager : FoxInteractionManager 
    {

        protected override void Start()
        {
            var fox_mating_reward = Academy.Instance.EnvironmentParameters.GetWithDefault("fox_mating_reward", 0.0f);
            
            MatingInteraction = GetComponent<MatingInteraction>();
            AddRewardAfterInteraction(MatingInteraction, fox_mating_reward);
            RegisterUpdatingCurrentInteractionAfterEndOf(MatingInteraction); 
            
            base.Start();
        }
    }
}