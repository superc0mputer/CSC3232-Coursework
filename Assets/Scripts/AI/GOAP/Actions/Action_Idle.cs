using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Idle : Action_Base
{
    protected override void Awake()
    {
        base.Awake();
        SupportedGoals = new List<System.Type>
            {typeof(Goal_Idle)};
    }
    
    
}
