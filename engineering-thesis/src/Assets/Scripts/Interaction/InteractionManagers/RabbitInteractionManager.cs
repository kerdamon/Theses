using DecisionMaking;
using DecisionMaking.States;
using DecisionMaking.States.EventStates;
using Interaction;
using Interaction.RabbitInteractions;
using Unity.MLAgents;
using UnityEngine;

namespace Interaction.InteractionManagers
{
    public class RabbitInteractionManager : InteractionManager
    {
        private EatingCarrotInteraction _eatingCarrotInteraction;
        private StateMachine _stateMachine;
        private EscapingPredatorState _escapingPredatorState;

        protected override void Start()
        {
            var rabbit_eating_carrot_reward =
                Academy.Instance.EnvironmentParameters.GetWithDefault("rabbit_eating_carrot_reward", 0.0f);
            _eatingCarrotInteraction = GetComponent<EatingCarrotInteraction>();
            AddRewardAfterInteraction(_eatingCarrotInteraction, rabbit_eating_carrot_reward);
            RegisterUpdatingCurrentInteractionAfterEndOf(_eatingCarrotInteraction);

            var parent = transform.parent;
            _stateMachine = parent.GetComponentInChildren<StateMachine>();
            _escapingPredatorState = parent.GetComponentInChildren<EscapingPredatorState>();

            base.Start();
        }

        protected override void StopInteractionWhenAgentDontWantTo()
        {
            if (IsInteracting && _stateMachine.CurrentState == _escapingPredatorState)
                CurrentInteraction.Interrupt();
            base.StopInteractionWhenAgentDontWantTo();
        }
    }
}