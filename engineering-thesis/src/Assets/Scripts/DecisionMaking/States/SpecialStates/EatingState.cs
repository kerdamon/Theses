using Interaction;
using UnityEngine;

namespace DecisionMaking.States.EventStates
{
    public class EatingState : SpecialState
    {
        protected override void Awake()
        {
            base.Awake();
            EatingCarrotInteraction.BeforeInteraction += ActivateThis;
            EatingCarrotInteraction.AfterInterruptedInteraction += DeactivateThis;
            EatingCarrotInteraction.AfterSuccessfulInteraction += DeactivateThis;
        }
    }
}