using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Goal_KillEnemyAI : Goal_Base
{
    //Priority will be calculated based on the distance to the Enemy AI + the health of the Enemy AI
    [SerializeField] private int KillEnemyAIPriorityMax = 70; //If distance = 0, this will be the priority 
    [SerializeField] private float MaxDistanceToChase = 15f;
    [SerializeField] private float DistanceToStopChase = 20f;

    public override bool CanRun()
    {
        if (!characterAgent.hasTargetsInFieldOfView())
        {
            return false;
        }
        /*if (characterAgent.possibleTargetsAI.Count == 0)
        {
            return false;
        }*/

        //check if targets are close enough
        foreach (var targetCandidate in characterAgent.possibleTargetsAI)
        {
            if (targetCandidate != null)
            {
                float distance = Vector3.Distance(gameObject.transform.position, targetCandidate.transform.position);
                if (distance <= MaxDistanceToChase)
                {
                    return true;
                }
            }
        }

        return false;
    }
    
    //Calculates Priority & CurrentTargetAI
    public override void OnTickGoal()
    {
        //no target
        if (!characterAgent.hasTargetsInFieldOfView())
            return;

        if (CurrentTarget != null)
        {
            //check if the current target is still in distance range
            foreach (var targetCandidate in characterAgent.possibleTargetsAI)
            {
                if (targetCandidate == CurrentTarget)
                {
                    float distance =
                        Vector3.Distance(gameObject.transform.position, targetCandidate.transform.position);
                    
                    //If distance bigger than distance to stop chase -> priority = 0
                    if (distance > DistanceToStopChase)
                    {
                        CurrentPriority = 0;
                        return;
                    }
                    
                    //Calculate priority based on distance and health percentage of enemy
                    int targetCandidateHealthPercentage = targetCandidate.GetComponent<HitHealth>().getHealthPercentage();
                    CurrentPriority =
                        Mathf.FloorToInt(KillEnemyAIPriorityMax - distance - (10 * targetCandidateHealthPercentage));
                    return;
                }
            }
            
            //clear current target (because target is not in range)
            CurrentTarget = null;
        }
        
        //acquire a new target based on distance to agent + health
        int tempKillAIPriority = 0;
        foreach (var targetCandidate in characterAgent.possibleTargetsAI)
        {
            if (targetCandidate != null)
            {
                float distance =
                    Vector3.Distance(gameObject.transform.position, targetCandidate.transform.position);

                //if target is in range
                if (distance <= MaxDistanceToChase)
                {
                    //calculate priority based on distance and health percentage of enemy
                    int targetCandidateHealthPercentage =
                        targetCandidate.GetComponent<HitHealth>().getHealthPercentage();

                    int currentTempKillAIPriority =
                        Mathf.FloorToInt(KillEnemyAIPriorityMax - distance - (10 * targetCandidateHealthPercentage));

                    //select target with the hightest priority
                    if (currentTempKillAIPriority > tempKillAIPriority)
                    {
                        CurrentTarget = targetCandidate;
                        tempKillAIPriority = currentTempKillAIPriority;
                    }
                }
            }

            CurrentPriority = tempKillAIPriority;
        }
    }
    
    public override void OnGoalDeactivate()
    {
        base.OnGoalDeactivate();
        CurrentTarget = null;
    }
    
    
}
