using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_ChaseAttack : Action_Base
{
    //No PreConditions for this Action
    //Completed when agents arrives at target 
    protected override void Awake()
    {
        base.Awake();
        //SupportedGoals = new List<System.Type>
            //{typeof(Goal_DestroyBase), typeof(Goal_KillPlayer), typeof(Goal_KillEnemyAI)};
        SupportedGoals = new List<System.Type>  {typeof(Goal_KillPlayer), typeof(Goal_KillEnemyAI)};
    }
    

    public override void OnActivated(Goal_Base linkedGoal)
    {
        base.OnActivated(linkedGoal);
        characterAgent.MoveToTarget(LinkedGoal.CurrentTarget);
    }
    
    public override void OnDeactivated()
    {
        characterAgent.StopFollowingTarget();
    }

    public override void OnTick()
    {
        if (characterAgent.InAttackRange(LinkedGoal.CurrentTarget))
        {
            characterAgent.DealDamage(LinkedGoal.CurrentTarget);
        }
    }
    
}
