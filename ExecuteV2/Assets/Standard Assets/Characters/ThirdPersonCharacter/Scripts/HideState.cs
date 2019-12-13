using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideState : State
{
    Transform destination;
    float startTime = 999999;
    public HideState(StateController stateController) : base(stateController) { }

    public override void CheckTransitions()
    {
        if (stateController.CheckIfInRange("Player") && 30f < Time.time - startTime)
        {
            stateController.health = 100;
            stateController.SetState(new ChaseState(stateController));
        }

        if (!stateController.CheckIfInRange("Player") && 30f < Time.time - startTime)
        {
            stateController.health = 100;
            stateController.SetState(new PatrolState(stateController));
        }

        if (stateController.shotAt > 5)
        {
            stateController.SetState(new CoverState(stateController));
        }


    }
    public override void Act()
    {
       
        if (stateController.ai.DestinationReached() && startTime == 999999)
        {
            startTime = Time.time;
        }
    }
    public override void OnStateEnter()
    {
        
        
        destination = stateController.GetHidePoint();
        if (stateController.ai.agent != null)
        {
            stateController.ai.agent.speed = 1f;
        }
        stateController.ai.SetTarget(destination);
        stateController.ChangeColor(Color.green);
    }

}
