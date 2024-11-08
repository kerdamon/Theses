using System;
using Interaction.InteractionManagers;
using UnityEngine;

namespace DecisionMaking.States
{
    public abstract class MainState : State
    {
        [SerializeField] protected AnimationCurve scoreCurve;
        
        protected InteractionManager InteractionManager;

        protected override void Awake()
        {
            var parent = transform.parent.parent;
            
            InteractionManager = parent.GetComponentInChildren<InteractionManager>();
            base.Awake();
            enabled = false;
        }

        public override void OnEnterState()
        {
            enabled = true;
            base.OnEnterState();
        }
        
        public override void OnLeaveState()
        {
            enabled = false;
            base.OnLeaveState();
        }
    }
}