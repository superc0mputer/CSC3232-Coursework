using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Wander : Action_Base
{
    [SerializeField] private float SearchRadius = 15f;
    
    protected override void Awake ()
    {
        base.Awake();
        SupportedGoals = new List<System.Type>
            {typeof(Goal_Wander)};
    }
    
    public override void OnActivated(Goal_Base linkedGoal)
    {
        base.OnActivated(linkedGoal);
        Vector3 location = characterAgent.PickRandomLocationInRange(SearchRadius);
        
        characterAgent.MoveToLocation(location);
    }

    public override void OnTick()
    {
        //arrived at destination
        if (characterAgent.AtDestination())
        {
            OnActivated(LinkedGoal);
        }
    }
}
