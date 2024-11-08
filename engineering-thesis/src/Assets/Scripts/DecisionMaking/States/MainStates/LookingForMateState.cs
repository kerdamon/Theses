using DecisionMaking.States.SpecialStates;
using Interaction;
using Interaction.InteractionManagers;
using UnityEngine;

namespace DecisionMaking.States
{
    public class LookingForMateState : MainState
    {
        [SerializeField] private float increaseInPartnersUrgeOnMatingAttempt;
        [SerializeField] private string femaleTag;
        public override float CurrentRank => scoreCurve.Evaluate(Needs["ReproductionUrge"]);

        private MatingState _thisMatingState;
        private MatingState _partnerMatingState;
        private Needs _mateNeeds;
        
        protected override void Awake()
        {
            base.Awake();
            _thisMatingState = transform.parent.GetComponentInChildren<MatingState>();
            MatingInteraction.BeforeInteraction += () =>
            {
                _thisMatingState.ActivateThis();
                _partnerMatingState.ActivateThis();
            };
            MatingInteraction.AfterInterruptedInteraction += () =>
            {
                _thisMatingState.DeactivateThis();
                _partnerMatingState.DeactivateThis();
            };
            MatingInteraction.AfterSuccessfulInteraction += () =>
            {
                _thisMatingState.DeactivateThis();
                _partnerMatingState.DeactivateThis();
            };
        }

        private void OnTriggerStay(Collider other)
        {
            if (!enabled || !other.gameObject.CompareTag(femaleTag)) return;
            Mate = other.transform.parent.gameObject;
            _mateNeeds = Mate.GetComponent<Needs>();
            _partnerMatingState = Mate.GetComponentInChildren<MatingState>();
            if (_mateNeeds.IsMaxOrGreater("ReproductionUrge"))
            {
                InteractionManager.InteractIfAbleWith(MatingInteraction, Mate);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!enabled || !other.gameObject.CompareTag(femaleTag)) return;
            if (!InteractionManager.AbleToInteract()) return;
            Mate = other.transform.parent.gameObject;
            _mateNeeds = Mate.GetComponent<Needs>();
            _mateNeeds["ReproductionUrge"] += increaseInPartnersUrgeOnMatingAttempt;
        }
    }
}