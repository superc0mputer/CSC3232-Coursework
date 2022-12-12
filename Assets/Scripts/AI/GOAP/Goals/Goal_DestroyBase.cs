using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_DestroyBase : Goal_Base
{
    //Notes: If there is an obstacle in the way, AI will move to that obstacle!
    //Priority will be calculated based on how many EnemyAI are still left in the scene

    [SerializeField] private int DestroyBasePriorityMax = 100;

    protected override void Awake()
    {
        base.Awake();
        String enemyBase = characterAgent.getAdversayTeam() + "Base";
        CurrentTarget = GameObject.Find(enemyBase);
        
    }

    public override bool CanRun()
    {
        return true;
    }

    public override void OnTickGoal()
    {
        GameObject[] allEnemiesAI = GameObject.FindGameObjectsWithTag(characterAgent.getAdversayTeam());

        //Calculate priority based on how many EnemyAI are still left in the scene
        CurrentPriority = DestroyBasePriorityMax - (allEnemiesAI.Length * 5);
    }


}
