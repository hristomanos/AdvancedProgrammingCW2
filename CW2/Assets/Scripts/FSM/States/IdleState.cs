using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is responsible for implementing the idle state.
//In the idle state the agent should stop moving and not do anything.

[CreateAssetMenu(fileName = "IdleState", menuName = "Unity-FSM/States/Idle")]
public class IdleState : AbstractFSMState
{
    public override bool EnterState()
    {
        base.EnterState();
        Debug.Log("Entered Idle state");

        return true;
    }

    public override void UpdateState()
    {
        Debug.Log("Updating Idle State");
    }

    public override bool ExitState()
    {
        base.ExitState();

        Debug.Log("Exiting Idle state");

        return true;
    }
}
