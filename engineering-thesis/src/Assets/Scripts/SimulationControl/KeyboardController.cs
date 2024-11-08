using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    [SerializeField] GameObject simulationMetricsUIStats;
    [SerializeField] GameObject simulationMetricsUIDeath;
    [SerializeField] GameObject simulationMetricsUITime;
    
    private bool issimulationMetricsUIStatsActive;
    private bool issimulationMetricsUIDeathActive;
    private bool issimulationMetricsUITimeActive;

    private float _defaultTimeScale;
    private float _defaultFixedDeltaTime;

    private void Start()
    {
        _defaultTimeScale = Time.timeScale;
        _defaultFixedDeltaTime = Time.fixedDeltaTime;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(issimulationMetricsUIDeathActive)
                simulationMetricsUIDeath.SetActive(false); 
            issimulationMetricsUIStatsActive = !issimulationMetricsUIStatsActive;
            simulationMetricsUIStats.SetActive(issimulationMetricsUIStatsActive);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(issimulationMetricsUIStatsActive)
                simulationMetricsUIStats.SetActive(false);  
            issimulationMetricsUIDeathActive = !issimulationMetricsUIDeathActive;
            simulationMetricsUIDeath.SetActive(issimulationMetricsUIDeathActive);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            issimulationMetricsUITimeActive = !issimulationMetricsUITimeActive;
            simulationMetricsUITime.SetActive(issimulationMetricsUITimeActive);
        } 

        if (Input.GetKeyDown(KeyCode.Equals))
        {
            Time.timeScale += 1;
            Time.fixedDeltaTime = _defaultFixedDeltaTime * Time.timeScale;
        }
        
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            Time.timeScale -= 1;
            Time.fixedDeltaTime = _defaultFixedDeltaTime * Time.timeScale;
        }
    }
}
