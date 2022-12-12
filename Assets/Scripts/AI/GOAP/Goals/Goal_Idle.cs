using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Idle : Goal_Base
{
    [SerializeField] private int IdlePriority = 10;
    
    public override int CalculatePriority()
    {
        return IdlePriority;
    }

    public override bool CanRun()
    {
        return true;
    }
}
