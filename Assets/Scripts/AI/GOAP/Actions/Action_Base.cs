using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Base : MonoBehaviour
{
    protected Goal_Base LinkedGoal = null;
    protected CharacterAgent characterAgent;
    protected List<System.Type> SupportedGoals = null;
    protected int cost = 0;
    
    

    protected virtual void Awake()
    {
        characterAgent = GetComponent<CharacterAgent>();
    }

    public virtual List<System.Type> GetSupportedGoals()
    {
        return SupportedGoals;
    }

    public virtual int GetCost()
    {
        return cost;
    }
    

    public virtual void OnActivated(Goal_Base linkedGoal)
    {
        LinkedGoal = linkedGoal;
    }

    public virtual void OnDeactivated()
    {
        
    }

    public virtual void OnTick()
    {
        
    }

    public virtual bool CheckPrerequisites()
    {
        return true;
    }


}
