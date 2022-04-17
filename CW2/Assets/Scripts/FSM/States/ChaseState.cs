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

    public delegate void KillPray();
    public static event KillPray onKillPray;
    

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
            m_Target = p_NPC.PrayTranform;

            if (m_Target == null)
            {
                Debug.LogError("Target was not set! in ChaseState");
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
            m_DistanceToTarget = Vector3.Distance(p_NavMeshAgent.transform.position, m_Target.position);
            Debug.Log("Distance to pray: " + m_DistanceToTarget);
            if (m_DistanceToTarget <= 1.2f)
            {
                Debug.Log("Pray was caught!");
                //Destroy pray game object

                if (onKillPray != null)
                {
                    onKillPray();
                }
                else
                {
                    Debug.Log("onkillPray is null");
                }

                p_FiniteStateMachine.EnterState(FSMStateType.IDLE);
            }
            else
                p_NavMeshAgent.SetDestination(m_Target.position);
        }
    }


    public override bool ExitState()
    {
        base.ExitState();

        Debug.Log("Exiting Chase state");

        return true;
    }
}
