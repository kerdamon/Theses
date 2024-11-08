using System;
using System.Linq;
using TMPro;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace DecisionMaking.States.EventStates
{
    public class EscapingPredatorState : SpecialState
    {
        private SphereCollider _sphereCollider;

        protected override void Awake()
        {
            base.Awake();
            _sphereCollider = GetComponent<SphereCollider>();
        }

        private void Update()
        {
            var colliders = Physics.OverlapSphere(transform.position, _sphereCollider.radius);
            if (colliders.Any(collider1 => (collider1.gameObject.CompareTag("Fox-Male") || collider1.gameObject.CompareTag("Fox-Female")) && !collider1.isTrigger))
            {
                ActivateThis();
                return;
            }
            DeactivateThis();
        }
    }
}