using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Interaction
{
    public class MatingInteraction : Interaction
    {
        [SerializeField] private int averageOffspringNumberPerLitter;
        [SerializeField] private int maxDeviationFromFertility;
        [SerializeField] private int maxRandomDeviation;
        [SerializeField] private float mutationProbability;

        [SerializeField] private GameObject maleChild;
        [SerializeField] private GameObject femaleChild;

        [SerializeField] private float maxSensorySphereColliderRadius;
        [SerializeField] private float minSensorySphereColliderRadius;
        
        private Needs _needs;
        private Features _features;

        private int _maxFeatureValue;
        private int _minFeatureValue = 0;   //todo change, magic number

        protected override void Start()
        {
            base.Start();
            _needs = GetComponentInParent<Needs>();
            _features = GetComponentInParent<Features>();
            _maxFeatureValue = (int)_features.maxValue;
        }

        protected override void AtInteractionEnd()
        {
            var mateNeeds = SecondSimulationObject.GetComponent<Needs>();
            _needs["ReproductionUrge"] = 0;
            mateNeeds["ReproductionUrge"] = 0;
            SpawnOffspring(SecondSimulationObject);
        }

        private void SpawnOffspring(GameObject mate)
        {
            var actorFeatures = SimulationObject.GetComponent<Features>();
            var mateFeatures = mate.GetComponent<Features>();

            var numberOfChildren = CalculateNumberOfOffspring(mateFeatures["Fertility"]);
            for (var i = 0; i < numberOfChildren; i++)
            {
                GameObject originalGameObject;
                bool isMale = false;
                if (Random.value > 0.5f)
                {
                    originalGameObject = maleChild;
                    isMale = true;
                }
                else
                {
                    originalGameObject = femaleChild;
                }

                var offspring = Instantiate(originalGameObject, transform.parent.parent);
                offspring.transform.position = mate.transform.position;
                offspring.transform.Translate(Random.value * 2, 0, Random.value * 2);
                
                var offspringFeatures = offspring.GetComponent<Features>();
                Crossover(actorFeatures, offspringFeatures, mateFeatures);
                Mutation(actorFeatures, offspringFeatures);
                UpdateSensorColliderSize(offspringFeatures, offspring);

                if (isMale)
                    offspring.GetComponentInChildren<MatingInteraction>().maleChild = GetComponent<MatingInteraction>().maleChild;
                offspring.GetComponent<MovementAgent>().simulationController =
                    transform.parent.GetComponent<MovementAgent>().simulationController;
            }
        }

        private void UpdateSensorColliderSize(Features offspringFeatures, GameObject offspring)
        {
            var collider = offspring.GetComponentInChildren<SphereCollider>(); //todo change if there will be other sphere colliders
            collider.radius = CalculateSensorySphereColliderRadius(offspringFeatures);
        }

        private float CalculateSensorySphereColliderRadius(Features features)
        {
            return (maxSensorySphereColliderRadius - minSensorySphereColliderRadius) / 100.0f *
                features["SensoryRange"] + minSensorySphereColliderRadius;
        }

        private void Mutation(Features actorFeatures, Features offspringFeatures)
        {
            foreach (var f in actorFeatures)
            {
                var feature = f.Key;
                offspringFeatures[feature] = Random.value < mutationProbability
                    ? Random.Range(0, 101)
                    : offspringFeatures[feature];
            }
        }

        private static void Crossover(Features actorFeatures, Features offspringFeatures, Features mateFeatures)
        {
            foreach (var f in actorFeatures)
            {
                offspringFeatures[f.Key] = Random.value > 0.5f ? f.Value : mateFeatures[f.Key];
            }
        }

        private int CalculateNumberOfOffspring(int fertility)
        {
            return averageOffspringNumberPerLitter + CalculateNumberOfOffspringFromFertility(fertility) + CalculateRandomChildrenComponent();
        }
        
        private int CalculateNumberOfOffspringFromFertility(int fertility)
        {
            var wholeLength = _maxFeatureValue - _minFeatureValue;
            var numberOfRanges = maxDeviationFromFertility * 2 + 1;
            var rangeLength = wholeLength * 1.0f / numberOfRanges;
            var returned = (int)(fertility / rangeLength) - maxDeviationFromFertility;

            return returned;
        }

        private int CalculateRandomChildrenComponent()
        {
            var returned = Random.Range(-maxRandomDeviation, maxRandomDeviation+1);
            return returned;
        }
    }
}
