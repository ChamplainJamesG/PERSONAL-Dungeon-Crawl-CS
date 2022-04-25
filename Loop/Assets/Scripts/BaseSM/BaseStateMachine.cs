using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStateMachine : MonoBehaviour
{
    public BaseState[] _states;

    protected BaseState _currentState;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        foreach (BaseState bs in _states)
            bs.ExitState();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public void TransitionState(int newState)
    {
        _currentState.ExitState();
        _currentState = _states[newState];
        _currentState.EnterState();
    }
}
