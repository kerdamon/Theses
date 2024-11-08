using Interaction;
using Interaction.FoxInteractions;
using Interaction.InteractionManagers;
using Interaction.RabbitInteractions;
using Unity.Barracuda;
using Unity.MLAgents;
using Unity.MLAgents.Policies;
using UnityEngine;

namespace DecisionMaking.States
{
    /// <summary>
    /// State only indicates in which state agent is. State objects does not contain any logic other than determining its score and preparing movement model.
    /// </summary>
    public abstract class State : MonoBehaviour
    {
        [SerializeField] private NNModel nnModel;
        private Agent _agent;
        private string _behaviourName;
        protected Needs Needs;

        protected DrinkingInteraction DrinkingInteraction;
        protected EatingCarrotInteraction EatingCarrotInteraction;
        protected EatingRabbitInteraction EatingRabbitInteraction;
        protected MatingInteraction MatingInteraction;
        protected GameObject Mate;  //todo change - this shouldn't be here. It is for setting mating state in mate
        
        protected virtual void Awake()
        {
            _agent = GetComponentInParent<MovementAgent>();
            var parent = transform.parent.parent;
            Needs = parent.GetComponent<Needs>();
            _behaviourName = GetComponentInParent<BehaviorParameters>().BehaviorName;    //todo change if there are multiple behaviours on one agent
            var grandParent = transform.parent.parent;

            DrinkingInteraction = grandParent.GetComponentInChildren<DrinkingInteraction>();
            EatingCarrotInteraction = grandParent.GetComponentInChildren<EatingCarrotInteraction>();
            MatingInteraction = grandParent.GetComponentInChildren<MatingInteraction>();
            EatingRabbitInteraction = grandParent.GetComponentInChildren<EatingRabbitInteraction>();
        }

        private void PrepareModel()
        {
            _agent.SetModel(_behaviourName, nnModel);
        }
        public abstract float CurrentRank { get; }

        public virtual void OnEnterState()
        {
            PrepareModel();
        }
        
        public virtual void OnLeaveState()
        {
        }
    }
}