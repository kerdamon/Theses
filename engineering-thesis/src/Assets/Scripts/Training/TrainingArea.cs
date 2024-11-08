using System;
using System.Collections;
using NaughtyAttributes;
using Unity.MLAgents;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrainingArea : MonoBehaviour, ITrainingArea
{
    [SerializeField] private Transform waterContainterTransform;
    [SerializeField] private Transform foodGeneratorContainterTransform;
    [SerializeField] private Transform rabbitContainterTransform;
    [SerializeField] private Transform foxGeneratorContainterTransform; 
    [SerializeField] private int maxRepositionsOnCollisions;
    
    [Range(10, 100)]
    [SerializeField] private int updateSizePeriod;

    public Transform geographicalObjectsContainer;
    [ShowNativeProperty] public float ContentSetupRange => geographicalObjectsContainer.localScale.x * 100;

    private float _lastGeographicalObjectsContainerScale = -1234.0f;
    
    public void ResetArea()
    {
        StopCoroutine(nameof(InnerReset));
        StartCoroutine(nameof(InnerReset));
    }

    protected virtual IEnumerator InnerReset()
    {
        RandomizeWater();
        yield return 0;
        RandomizeFoodGenerators();
        yield return 0;
        RandomizeRabbits();
        yield return 0;
        RandomizeFoxes();
        yield return 0;
    }

    private void RandomizeFoodGenerators()
    {
        foreach (Transform foodGenerator in foodGeneratorContainterTransform)
        {
            ClearFood(foodGenerator);
            RandomizePositionAndRotationWithCollisionCheck(foodGenerator, foodGeneratorContainterTransform);
        }
    }
    
    public void RandomizeRabbits()
    {
        foreach (Transform rabbit in rabbitContainterTransform)
        {
            RandomizePositionAndRotationWithCollisionCheck(rabbit, rabbitContainterTransform);
        }
    }
    
    private void RandomizeFoxes()
    {
        foreach (Transform fox in foxGeneratorContainterTransform)
        {
            RandomizePositionAndRotationWithCollisionCheck(fox, foxGeneratorContainterTransform);
        }
    }

    private void RandomizeWater()
    {
        foreach (Transform water in waterContainterTransform)
        {
            RandomizePositionAndRotation(water);
        }
    }

    private void Start()
    {
        var newScale = Academy.Instance.EnvironmentParameters.GetWithDefault("training_area_size", 1.0f);
        geographicalObjectsContainer.localScale = new Vector3(newScale, 1, newScale);
    }

    private void Update()
    {
        if (Time.frameCount % updateSizePeriod != 0) return;
        var newScale = Academy.Instance.EnvironmentParameters.GetWithDefault("training_area_size", 1.0f);
        if (Math.Abs(_lastGeographicalObjectsContainerScale - newScale) > 0.001f)
        {
            geographicalObjectsContainer.localScale = new Vector3(newScale, 1, newScale);
            StartCoroutine(nameof(InnerReset));
            _lastGeographicalObjectsContainerScale = newScale;
        }
    }

    public void RandomizePositionAndRotationWithCollisionCheck(Transform obj, Transform containterTransform)
    {
        var iterator = 0;
        var newPosition = obj.position;
        var newRotation = obj.rotation;
        while (iterator < maxRepositionsOnCollisions)
        {
            var range = ContentSetupRange * 0.8f;
            newPosition = containterTransform.TransformPoint(new Vector3(Random.Range(-range, range), obj.localPosition.y, Random.Range(-range, range)));
            newRotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));
            var isCollision = Physics.CheckBox(newPosition, obj.localScale / 2);
            if(!isCollision)
            {
                break;
            }
            iterator++;
        }
        obj.position = newPosition;
        obj.rotation = newRotation;
    }

    protected void RandomizePositionAndRotation(Transform gameObject)
    {
        var range = ContentSetupRange * 0.8f;
        gameObject.localPosition = new Vector3(Random.Range(-range, range), gameObject.localPosition.y, Random.Range(-range, range));
        gameObject.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));
    }

    private static void ClearFood(Transform foodGeneratorTransform)
    {
        foreach(Transform foodObject in foodGeneratorTransform)
        {
            Destroy(foodObject.gameObject);
        }
    }
}
