using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    private MovementAgent _movementAgent;
    
    private float agent_bump_into_wall;
    private float agent_bump_into_water;
    private float agent_bump_into_food;

    private void Awake()
    {
        _movementAgent = GetComponentInParent<MovementAgent>();
    }

    private void Start()
    {
        agent_bump_into_wall = Academy.Instance.EnvironmentParameters.GetWithDefault("agent_bump_into_wall", 0.0f);
        agent_bump_into_water = Academy.Instance.EnvironmentParameters.GetWithDefault("agent_bump_into_water", 0.0f);
        agent_bump_into_food = Academy.Instance.EnvironmentParameters.GetWithDefault("agent_bump_into_food", 0.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            if(Mathf.Abs(agent_bump_into_wall) > 0.0001f){
                Debug.Log($"Added reward of value {agent_bump_into_wall} after bumping into wall", this);
                _movementAgent.AddReward(agent_bump_into_wall); 
            }
        }
        if (other.gameObject.CompareTag("Water"))
        {
            if(Mathf.Abs(agent_bump_into_water) > 0.0001f){
                Debug.Log($"Added reward of value {agent_bump_into_water} after bumping into water", this);
                _movementAgent.AddReward(agent_bump_into_water);
            }
        }
        if (other.gameObject.CompareTag("Food"))
        {
            if(Mathf.Abs(agent_bump_into_food) > 0.0001f){
                Debug.Log($"Added reward of value {agent_bump_into_food} after bumping into food", this);
                _movementAgent.AddReward(agent_bump_into_food);
            }
        }
    }
}
