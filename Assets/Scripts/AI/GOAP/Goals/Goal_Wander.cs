using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Wander : Goal_Base
{
    [SerializeField] private int MaxPriority = 20;
    [SerializeField] private float PriorityBuildRate = 1f;
    [SerializeField] private float PriorityDecayRate = 0.5f;
    private float CurrentPriorityFloat;
    
    public override int CalculatePriority()
    {
        return Mathf.FloorToInt(CurrentPriorityFloat);
    }

    public override bool CanRun()
    {
        return true;
    }
    
    public override void OnTickGoal()
    {
        if (characterAgent.IsMoving())
        {
            CurrentPriorityFloat -= PriorityDecayRate * Time.deltaTime;
        }
        else
        {
            CurrentPriorityFloat += PriorityBuildRate * Time.deltaTime;
        }
    }

    public override void OnGoalActivated(Action_Base linkedAction)
    {
        base.OnGoalActivated(linkedAction);
        CurrentPriorityFloat = MaxPriority;
    }
}
