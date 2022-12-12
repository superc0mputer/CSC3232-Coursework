using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Base : MonoBehaviour
{
    protected CharacterAgent characterAgent;
    protected Action_Base LinkedAction;
    public GameObject CurrentTarget;
    protected int CurrentPriority = -1;

    protected virtual void Awake()
    {
        characterAgent = GetComponent<CharacterAgent>();
    }

    private void Update()
    {
        OnTickGoal();
    }

    //Priority is number between 1 and 100
    public virtual int CalculatePriority()
    {
        return CurrentPriority;
    }

    public virtual bool CanRun()
    {
        return false;
    }

    public virtual void OnTickGoal()
    {
        
    }

    public virtual void OnGoalActivated(Action_Base linkedAction)
    {
        LinkedAction = linkedAction;
    }

    public virtual void OnGoalDeactivate()
    {
        LinkedAction = null;
    }
}
