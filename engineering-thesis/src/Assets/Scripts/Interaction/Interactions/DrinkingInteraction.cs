using UnityEngine;

namespace Interaction
{
    public class DrinkingInteraction : Interaction
    {
        [SerializeField] private float thirstChangeFactor;
        private Needs _needs;

        private float _drankAmount;
    
        protected override void Start()
        {
            base.Start();
            _needs = SimulationObject.GetComponent<Needs>();
            RegisterThirstChangeAfterInteraction();
        }
        
        protected virtual void RegisterThirstChangeAfterInteraction()
        {
            AfterSuccessfulInteraction += () => _needs["Thirst"] -= _drankAmount;
            AfterInterruptedInteraction += () => _needs["Thirst"] -= _drankAmount; 
        }

        protected override void AtInteractionStart()
        {
            _drankAmount = 0;
        }

        protected override void AtInteractionIncrement()
        {
            if (_needs["Thirst"] - _drankAmount < 0)
            {
                Interrupt();
            }
            else
            {
                _drankAmount += thirstChangeFactor;
            }
        }
    }
}