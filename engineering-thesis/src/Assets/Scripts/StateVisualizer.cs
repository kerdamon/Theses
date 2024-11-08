using DecisionMaking;
using UnityEngine;

public class StateVisualizer : MonoBehaviour
{
    private TextMesh _tooltip;
    private StateMachine _rabbitState;

    private void Start()
    {
        _tooltip = GetComponentInChildren<TextMesh>();
        _rabbitState = GetComponent<StateMachine>();
    }

    void Update()
    {
        _tooltip.text = _rabbitState.CurrentState.ToString();
    }
}
