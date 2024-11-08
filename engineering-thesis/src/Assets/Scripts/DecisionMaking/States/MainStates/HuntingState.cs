using Interaction.RabbitInteractions;
using UnityEngine;

namespace DecisionMaking.States
{
    public class HuntingState: MainState
    {
        public override float CurrentRank => scoreCurve.Evaluate(Needs["Hunger"]);
        
        private void OnTriggerStay(Collider other)
        {
            if(enabled && (other.gameObject.CompareTag("Rabbit-Male") || other.gameObject.CompareTag("Rabbit-Female")) && Needs["Hunger"] > 0)
                InteractionManager.InteractIfAbleWith(EatingRabbitInteraction, other.transform.parent.gameObject);
        }
    }
}