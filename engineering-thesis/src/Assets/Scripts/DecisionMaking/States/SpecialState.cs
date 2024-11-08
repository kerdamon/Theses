using UnityEngine;

namespace DecisionMaking.States
{
    public abstract class SpecialState : State
    {
        [Range(0f, 2f)]
        [SerializeField] protected float rank;

        /// <summary>
        /// Indicates whether state is active and will be taken into account during state inference.
        /// If it is false then effective rank of this state is 0.
        /// </summary>
        [Tooltip("Indicates whether state is active and will be taken into account during state inference." +
                 " If it is false then effective rank of this state is 0.")]
        [SerializeField] protected bool active;

        public override float CurrentRank => active ? rank : 0.0f;
        public void DeactivateThis() => active = false;
        public void ActivateThis() => active = true;
    }
}