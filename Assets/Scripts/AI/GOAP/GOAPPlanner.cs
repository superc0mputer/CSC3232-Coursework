using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAPPlanner : MonoBehaviour
{
    private Goal_Base[] Goals;
    private Action_Base[] Actions;

    private Goal_Base ActiveGoal;
    private Action_Base ActiveAction;
    
    
    private void Awake()
    {
        Goals = GetComponents<Goal_Base>();
        Actions = GetComponents<Action_Base>();
    }

    private void Update()
    {
        //best goal + best associated action 
        Goal_Base bestGoal = null;
        Action_Base bestAction = null;
        
        //find highest priority goal that can be activated
        foreach (var goal in Goals)
        {
            //update all the goals (priority logic)
            goal.OnTickGoal();
            
            //can goal run?
            if (!goal.CanRun())
            {
                continue;
            }
            
            //is it a worse priority than current bestGoal?
            if (!(bestGoal == null || (goal.CalculatePriority() > bestGoal.CalculatePriority())))
            {
                continue;
            }

            //find the best cost action
            Action_Base candidateAction = null;
            foreach (var action in Actions)
            {
                if (!action.GetSupportedGoals().Contains(goal.GetType()))
                {
                    continue;
                }

                //found action with suitable cost
                if (action.CheckPrerequisites())
                {
                    if (candidateAction == null || action.GetCost() < candidateAction.GetCost())
                    {
                        candidateAction = action;
                    }
                }
            }
            //did we find an action?
            if (candidateAction != null)
            {
                bestGoal = goal;
                bestAction = candidateAction;
            }
        }
        
        //if no current goal
        if (ActiveAction == null)
        {
            ActiveGoal = bestGoal;
            ActiveAction = bestAction;
            
            if (ActiveGoal != null) ActiveGoal.OnGoalActivated(ActiveAction);
            if (ActiveAction != null) ActiveAction.OnActivated(ActiveGoal);
            
            
        } //no change in goal, but maybe change in action
        else if (ActiveGoal == bestGoal)
        {
            //action change?
            if (ActiveAction != bestAction)
            {
                ActiveAction.OnDeactivated();
                ActiveAction = bestAction;
                if (ActiveAction != null) ActiveAction.OnActivated(ActiveGoal);
            }

        } //new goal or no valid goal
        else if (ActiveGoal != bestGoal)
        {
            ActiveGoal.OnGoalDeactivate();
            ActiveAction.OnDeactivated();

            ActiveGoal = bestGoal;
            ActiveAction = bestAction;

            if (ActiveGoal != null) ActiveGoal.OnGoalActivated(ActiveAction);
            if (ActiveAction != null) ActiveAction.OnActivated(ActiveGoal);
            
        }
        
        //tick the action
        if (ActiveAction != null) ActiveAction.OnTick();
        
        //Debug.Log("Current Goal: " + ActiveGoal);
        //Debug.Log("Current action: " + ActiveAction);
        
    }
}
