using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is responsible for implementing the idle state.
//In the idle state the agent should stop moving and not do anything.

[CreateAssetMenu(fileName = "IdleState", menuName = "Unity-FSM/States/Idle")]
public class IdleState : AbstractFSMState
{
    [SerializeField] float m_Duration = 3f;

    float m_TotalDuration;

    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.IDLE;
    }


    public override bool EnterState()
    {
        EnteredState = base.EnterState();

        if (EnteredState)
        {
            Debug.Log("Entered Idle state");
            m_TotalDuration = 0;
        }

        
        return EnteredState;
    }

    public override void UpdateState()
    {
        if (EnteredState)
        {
            m_TotalDuration += Time.deltaTime;
           // Debug.Log("Updating Idle State " + m_TotalDuration + " seconds.");


            if (m_TotalDuration >= m_Duration)
            {
                p_FiniteStateMachine.EnterState(FSMStateType.PATROL);
            }

        }
    }

    public override bool ExitState()
    {
        base.ExitState();

        Debug.Log("Exiting Idle state");

        return true;
    }
}
