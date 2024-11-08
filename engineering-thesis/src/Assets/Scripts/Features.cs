using System;
using System.Linq;
using NaughtyAttributes;
using Unity.MLAgents;
using UnityEngine;

public class Features : DictionarySerializer<int>
{
    /// <summary>
    /// This is genetic maintenance cost. It has higher values if agent has better features.
    /// </summary>
    [ShowNativeProperty] public float CurrentGeneticCost => this.Sum(feature => feature.Value);
    [ShowNativeProperty] public float MaxGeneticCost => this.Count() * maxValue;
    
    public override bool IsMaxOrGreater(string value)
    {
        return this[value] >= maxValue;
    }

    //todo change this hardcoded
    private void Start()
    {
        var is_training = Academy.Instance.EnvironmentParameters.GetWithDefault("is_training", 0) > 0;
        if (is_training)
        {
            this["Speed"] = (int)Academy.Instance.EnvironmentParameters.GetWithDefault("agent_speed", 50);
            this["SensoryRange"] = (int)Academy.Instance.EnvironmentParameters.GetWithDefault("agent_sensory_range", 50);
        }
    }
}
