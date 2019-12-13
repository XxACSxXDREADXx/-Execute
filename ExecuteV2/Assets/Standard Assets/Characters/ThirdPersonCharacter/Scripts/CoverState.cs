using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverState : State
{

    float moveTimer = 0f;
    float maxMoveTimer = 10.0f;
    Transform destination;

    public CoverState(StateController stateController) : base(stateController) { }

    public override void CheckTransitions()
    {
        
        if (stateController.CheckIfInRange("Player") && stateController.shotAt < 1)
        {
            stateController.SetState(new ChaseState(stateController));
        }

        if (!stateController.CheckIfInRange("Player") && stateController.shotAt < 1)
        {
            stateController.SetState(new PatrolState(stateController));
        }



    }
    public override void Act()
    {
        if (stateController.PlayerCanSee())
        {
            destination = stateController.GetClosestCover();
            stateController.ai.SetTarget(destination);
        }
       

        if(stateController.CheckTime())
        {
            stateController.shotAt = 0;
            stateController.ChangeColor(Color.cyan);
        }
    }
    public override void OnStateEnter()
    {
        destination = stateController.GetClosestCover();
        if (stateController.ai.agent != null)
        {
            stateController.ai.agent.speed = 1f;
        }
        stateController.ai.SetTarget(destination);
        stateController.ChangeColor(Color.yellow);
        stateController.StartTimer(maxMoveTimer);
    }
}
