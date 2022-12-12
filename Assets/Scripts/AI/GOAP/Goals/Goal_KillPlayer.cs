using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_KillPlayer : Goal_Base
{
    [SerializeField] private int KillPlayerPriorityMax = 90;
    [SerializeField] private float MaxDistanceToChase = 15f;
    [SerializeField] private float DistanceToStopChase = 20f;

    protected override void Awake()
    {
        base.Awake();
        CurrentTarget = GameObject.FindWithTag("Player");
    }

    public override bool CanRun()
    {
        
        if (!characterAgent.hasPlayerInFieldOfView())
        {
            return false;
        }

        //check if player is close enough
        if (characterAgent.possibleTargetPlayer != null)
        {
            float distance = Vector3.Distance(gameObject.transform.position,
                characterAgent.possibleTargetPlayer.transform.position);
            if (distance <= MaxDistanceToChase)
            {
                return true;
            }
        }

        return false;
    }

    public override void OnTickGoal()
    {
        GameObject playerGameObject = characterAgent.possibleTargetPlayer;
        //no target (player not in range)
        if (!characterAgent.hasPlayerInFieldOfView())
            return;
        if (playerGameObject != null)
        {
            float distance =
                Vector3.Distance(gameObject.transform.position, playerGameObject.transform.position);

            if (distance > DistanceToStopChase)
            {
                CurrentPriority = 0;
                return;
            }
            
            //Calculate priority based on distance and health percentage of enemy
            int playerHealthPercentage = playerGameObject.GetComponent<HitHealth>().getHealthPercentage();

            //CurrentPriority =
                //Mathf.FloorToInt(KillPlayerPriorityMax - distance - (10 * playerHealthPercentage));
                CurrentPriority =
                    Mathf.FloorToInt(KillPlayerPriorityMax - distance);
            
        }
    }
}


