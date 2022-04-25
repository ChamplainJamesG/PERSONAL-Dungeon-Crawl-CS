using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUIState : BaseState
{
    public GameObject Selector;

    public override void EnterState()
    {
        gameObject.SetActive(true);
    }

    public override void ExitState()
    {
        gameObject.SetActive(false);
    }

    protected override void InputState()
    {
        //throw new System.NotImplementedException();
    }

    protected override void RenderState()
    {
        //throw new System.NotImplementedException();
    }
}

