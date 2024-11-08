using System.Collections.Generic;
using System.Linq;
using DecisionMaking.States;
using Unity.MLAgents;
using UnityEngine;

namespace DecisionMaking
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private State defaultState;
    
        public State CurrentState { get; private set; }
        private List<State> _statesList;
        
        private void Start()
        {
            _statesList = GetComponentsInChildren<State>().ToList();
            SetDefaultState();
            CurrentState.OnEnterState();
        }

        protected virtual void SetDefaultState()
        {
           SetState(defaultState);
        }
        
        protected virtual void Update(){
            InferState();
        }

        private void InferState()
        {
            var newStateCandidate = _statesList.Aggregate((state1, state2) => state1.CurrentRank > state2.CurrentRank ? state1 : state2);
            if (newStateCandidate.CurrentRank > CurrentState.CurrentRank)
            {
                ChangeStateTo(newStateCandidate);
            }
        }

        private void ChangeStateTo(State newState)
        {
            CurrentState.OnLeaveState();
            SetState(newState);
            CurrentState.OnEnterState();
        }

        private void SetState(State newState)
        {
            CurrentState = newState;
        }
    }
}

