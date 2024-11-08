using Interaction.RabbitInteractions;
using UnityEngine;

namespace DecisionMaking.States
{
    public class LookingForFoodState : MainState
    {
        public override float CurrentRank => scoreCurve.Evaluate(Needs["Hunger"]);
        
        private void OnTriggerStay(Collider other)
        {
            if(enabled && other.gameObject.CompareTag("Food") && Needs["Hunger"] > 0) //todo abstract this method to State
                InteractionManager.InteractIfAbleWith(EatingCarrotInteraction, other.gameObject);
        }
    }
}