using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBrain
{
    // Monster AI shit here.
    public virtual void HandleBrainActions(Monster creature)
    {

    }
}

public class BatBrain : MonsterBrain
{
    public override void HandleBrainActions(Monster creature)
    {
        //base.HandleBrainActions();
    }
}

public class Monster : Entity
{
    public MonsterBrain Brain;
}
