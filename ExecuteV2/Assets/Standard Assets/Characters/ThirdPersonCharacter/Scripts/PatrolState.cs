using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State {

    Transform destination;

    public PatrolState(StateController stateController) : base(stateController) { }

    public override void CheckTransitions()
    {
        if (stateController.CheckIfInRange("Player"))
        {
            stateController.SetState(new ChaseState(stateController));
        }

        if (stateController.health < 70)
        {
            stateController.SetState(new HideState(stateController));
        }

        if (stateController.shotAt > 1)
        {
            stateController.SetState(new CoverState(stateController));
        }
    }
    public override void Act()
    {
        if(destination == null || stateController.ai.DestinationReached())
        {
            destination = stateController.GetNextNavPoint();
            stateController.ai.SetTarget(destination);
        }
    }
    public override void OnStateEnter()
    {
        destination = stateController.GetNextNavPoint();
        if (stateController.ai.agent != null)
        {
            stateController.ai.agent.speed = 0.75f;
        }
        stateController.ai.SetTarget(destination);
        stateController.ChangeColor(Color.blue);
    }

}
