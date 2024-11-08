using System;
using DecisionMaking;
using DecisionMaking.States;
using Interaction;
using Unity.MLAgents;
using UnityEngine;

namespace Interaction.InteractionManagers
{
    public abstract class InteractionManager : MonoBehaviour
    {
        protected MovementAgent MovementAgent;
        private DrinkingInteraction _drinkingInteraction;
        protected MatingInteraction MatingInteraction;
        protected Needs Needs;
        
        public Interaction CurrentInteraction { get; protected set; }
        public bool IsInteracting => !(CurrentInteraction is null);


        private float agent_drink_reward;

        protected virtual void Start()
        {
            var parent = transform.parent;
            MovementAgent = parent.GetComponent<MovementAgent>();
            
            Needs = parent.GetComponent<Needs>();
            
            agent_drink_reward = Academy.Instance.EnvironmentParameters.GetWithDefault("agent_drink_reward", 0.0f);

            _drinkingInteraction = GetComponent<DrinkingInteraction>();
            AddRewardAfterInteraction(_drinkingInteraction, agent_drink_reward);
            RegisterUpdatingCurrentInteractionAfterEndOf(_drinkingInteraction);

            MatingInteraction = GetComponent<MatingInteraction>();

        }

        private void Update()
        {
            StopInteractionWhenAgentDontWantTo();
        }

        protected void AddRewardAfterInteraction(Interaction interaction, float rewardValue)
        {
            if (!(Mathf.Abs(rewardValue) > 0.0001f)) return;    //check for 0.0f with epsilon
            interaction.AfterSuccessfulInteraction += () =>
            {
                MovementAgent.AddReward(rewardValue);
                Debug.Log($"Added reward of value {rewardValue} after successful interaction {interaction.GetType()}", this);
            };
        }

        protected void RegisterUpdatingCurrentInteractionAfterEndOf(Interaction interaction)
        {
            void ClearCurrentInteraction() => CurrentInteraction = null;
            interaction.AfterSuccessfulInteraction += ClearCurrentInteraction;
            interaction.AfterInterruptedInteraction += ClearCurrentInteraction;
        }

        public void InteractIfAbleWith(Interaction interaction, GameObject target)
        {
            if (MovementAgent.WantInteraction && !IsInteracting)
            {
                LaunchNewInteraction(interaction, target);
            }
        }

        public bool AbleToInteract() => MovementAgent.WantInteraction && !IsInteracting; 
        
        protected void LaunchNewInteraction(Interaction interaction, GameObject target)
        {
            CurrentInteraction = interaction;
            CurrentInteraction.StartInteraction(target);
        }

        protected virtual void StopInteractionWhenAgentDontWantTo()
        {

        }
    }
}