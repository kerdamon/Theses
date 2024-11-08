using System;
using System.Collections;
using UnityEngine;

namespace Interaction
{
    public abstract class Interaction : MonoBehaviour
    {
        private IEnumerator _interactCoroutine;
        protected float TimeElapsed;
        [SerializeField] protected float timeIncrement;
        [SerializeField] protected float interactionDuration;

        protected GameObject SimulationObject;
        public GameObject SecondSimulationObject { get; set; }

        public Action BeforeInteraction;
        public Action AfterSuccessfulInteraction;
        public Action AfterInterruptedInteraction;
        
        protected virtual void Start()
        {
            SimulationObject = transform.parent.gameObject;
            _interactCoroutine = InteractionCoroutine();
        }

        public void StartInteraction(GameObject secondActor)
        {
            _interactCoroutine = InteractionCoroutine();
            SecondSimulationObject = secondActor;
            StartCoroutine(_interactCoroutine);
        }

        public void Interrupt()
        {
            StopAllCoroutines();
            AfterInterruptedInteraction();
        }
        
        private IEnumerator InteractionCoroutine()
        {
            BeforeInteraction?.Invoke();
            TimeElapsed = 0.0f;
            AtInteractionStart();
            
            while (TimeElapsed < interactionDuration)
            {
                TimeElapsed += timeIncrement;
                AtInteractionIncrement();
                yield return new WaitForSeconds(timeIncrement);
            }
            
            AtInteractionEnd();
            AfterSuccessfulInteraction?.Invoke();
        }

        protected virtual void AtInteractionStart()
        {
        }

        protected virtual void AtInteractionEnd()
        {
        }

        protected virtual void AtInteractionIncrement()
        {
        }
    }
}