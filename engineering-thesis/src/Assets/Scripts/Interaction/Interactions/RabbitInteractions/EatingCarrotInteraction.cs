using Interaction.InteractionManagers;
using UnityEngine;

namespace Interaction.RabbitInteractions
{
    public class EatingCarrotInteraction : Interaction
    {
        [SerializeField] private float energyReceivedPerBite;
        [SerializeField] private float biteSize;

        private Needs _needs;
        private float _eatenAmount;
        private PlantGrower _plantGrower;
    
        protected override void Start()
        {
            base.Start();
            _needs = SimulationObject.GetComponent<Needs>();
            RegisterHungerChangeAfterInteraction();
        }

        protected virtual void RegisterHungerChangeAfterInteraction()
        {
            AfterSuccessfulInteraction += () => _needs["Hunger"] -= _eatenAmount;
            AfterInterruptedInteraction += () => _needs["Hunger"] -= _eatenAmount;
        }

        protected override void AtInteractionStart()
        {
            _eatenAmount = 0;
            _plantGrower = SecondSimulationObject.GetComponent<PlantGrower>();
        }

        protected override void AtInteractionIncrement()
        {
            if ((_needs["Hunger"] - _eatenAmount < 0) || (_plantGrower == null))
            {
                Interrupt();
            }
            else
            {
                _eatenAmount += energyReceivedPerBite;
                if (_plantGrower.OnEaten(biteSize) < 0.5)
                {
                    Interrupt();
                }
            }
        }
    }
}