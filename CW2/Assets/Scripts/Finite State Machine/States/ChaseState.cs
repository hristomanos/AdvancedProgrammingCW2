using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is responsible for making the hunter chase the pray.
[CreateAssetMenu(fileName = "ChaseState",menuName = "Unity-FSM/States/Chase")]
public class ChaseState : AbstractFSMState
{
    Transform m_Target;
    float m_DistanceToTarget;

    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.CHASE;
    }

    public override bool EnterState()
    {
        EnteredState = false;

        if (base.EnterState())
        {
            Debug.Log("Entered Chase state");
            m_Target = p_NPC.PreyTranform;
            
            if (m_Target == null)
            {
                Debug.LogError("Target was not set in ChaseState");
            }
            else
            {
                p_NavMeshAgent.SetDestination(m_Target.position);
                EnteredState = true;
            }
        }

        return EnteredState;
    }

    public override void UpdateState()
    {
        if (EnteredState)
        {
               // m_DistanceToTarget = Vector3.Distance(p_NavMeshAgent.transform.position, m_Target.position);
               // Debug.Log("Distance to prey: " + m_DistanceToTarget);

                if (p_NPC.PreyWasCought)
                {
                    Debug.Log("PREDATOR: switched to idle");
                    p_FiniteStateMachine.EnterState(FSMStateType.IDLE);
                }
                else
                    p_NavMeshAgent.SetDestination(m_Target.position);
            
        }
    }


    public override bool ExitState()
    {
        base.ExitState();
        p_NPC.PreyWasCought = false;
        Debug.Log("Exiting Chase state");

        return true;
    }
}
