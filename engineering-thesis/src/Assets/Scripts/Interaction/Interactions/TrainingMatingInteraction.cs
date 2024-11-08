using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Interaction
{
    public class TrainingMatingInteraction : MatingInteraction
    {
        [SerializeField] private TrainingArea trainingArea;

        protected override void AtInteractionEnd()
        {
            var mate = SecondSimulationObject.transform;
            var mateContainer = mate.parent;
            trainingArea.RandomizePositionAndRotationWithCollisionCheck(mate, mateContainer);
        }
    }
}
