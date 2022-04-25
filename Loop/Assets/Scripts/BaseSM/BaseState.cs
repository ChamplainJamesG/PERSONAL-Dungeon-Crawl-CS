using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected BaseStateMachine _myMachine;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        UpdateState();
    }

    public abstract void EnterState();

    public virtual void UpdateState()
    {
        InputState();
        RenderState();
    }

    protected abstract void InputState();
    protected abstract void RenderState();

    public abstract void ExitState();

    public virtual void SetMachine(BaseStateMachine m)
    {
        _myMachine = m;
    }
}