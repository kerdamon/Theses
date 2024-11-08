using System.Collections.Generic;
using System.Linq;
using Unity.MLAgents;
using UnityEngine;

public class TrainingSettings : MonoBehaviour
{
    private List<ITrainingArea> TrainingAreas;
    private Academy _academyInstance;
    void Awake()
    {
        _academyInstance = Academy.Instance;
        _academyInstance.OnEnvironmentReset += EnvironmentReset;
        TrainingAreas = new List<ITrainingArea>();
    }

    private void EnvironmentReset()
    {
        TrainingAreas = FindObjectsOfType<MonoBehaviour>().OfType<ITrainingArea>().ToList();
        foreach(var trainingArea in TrainingAreas)
        {
            trainingArea.ResetArea();
        }
    }
}
