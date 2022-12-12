using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Destroy : Action_Base
{
    private GameObject CurrentBaseTarget;
    private GameObject CurrentObstacleTarget;
    protected override void Awake()
    {
        base.Awake();
        SupportedGoals = new List<System.Type>
            {typeof(Goal_DestroyBase)};
    }
    
    public override void OnActivated(Goal_Base linkedGoal)
    {
        base.OnActivated(linkedGoal);
        CurrentBaseTarget = LinkedGoal.CurrentTarget;
        characterAgent.MoveToTarget(CurrentBaseTarget);
    }
    
    public override void OnDeactivated()
    {
        characterAgent.StopFollowingTarget();
    }
    
    public override void OnTick()
    {
        if (!characterAgent.TargetInFieldOfView(CurrentBaseTarget.transform))
        {
            if (CurrentObstacleTarget == null || !characterAgent.InAttackRange(CurrentObstacleTarget))
            {
                CurrentObstacleTarget = null;
                foreach (var obstacle in characterAgent.possibleTargetsObstacles)
                {
                    if (characterAgent.InAttackRange(obstacle))
                    {
                        CurrentObstacleTarget = obstacle;
                        Debug.Log(CurrentObstacleTarget);
                        break;
                    }
                }
            }
            
            if (CurrentObstacleTarget != null)
            {
                if (characterAgent.InAttackRange(CurrentObstacleTarget))
                {
                    characterAgent.DealDamage(CurrentObstacleTarget);
                }
            }
        }
        else
        { 
            if (characterAgent.InAttackRange(CurrentBaseTarget))
            {
                characterAgent.DealDamage(CurrentBaseTarget);
            }
        }

    }
}
