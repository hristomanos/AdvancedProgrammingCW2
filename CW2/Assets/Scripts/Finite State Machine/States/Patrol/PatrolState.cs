using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PatrolState", menuName = "Unity-FSM/States/Patrol", order = 2)]
public class PatrolState : AbstractFSMState
{

    ConnectedWaypoint[] m_PatrolPoints;
    int m_PatrolPointIndex; //Always knows which point we are at that array

    

    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.PATROL;
        m_PatrolPointIndex = -1;
    }

    public override bool EnterState()
    {
        EnteredState = false;
        if (base.EnterState())
        {
            //Grab and store the patrol points.
            m_PatrolPoints = p_NPC.PatrolPoints;

            if (m_PatrolPoints == null || m_PatrolPoints.Length == 0)
            {
                Debug.LogError("PatrolState: Failed to grab patrol points from the NPC.");
                
            }
            else
            {
                if (m_PatrolPointIndex < 0)
                {
                    m_PatrolPointIndex = Random.Range(0, m_PatrolPoints.Length);
                }
                else
                {
                    m_PatrolPointIndex = (m_PatrolPointIndex + 1) % m_PatrolPoints.Length; // It will not exceed patrolpoints.length instead, it goes back to zero.
                }

                SetDestination(m_PatrolPoints[m_PatrolPointIndex]);
                EnteredState = true;
            }
        }


        return EnteredState;
    }

    public override void UpdateState()
    {
        if (EnteredState)
        {
            if (Vector3.Distance(p_NavMeshAgent.transform.position, m_PatrolPoints[m_PatrolPointIndex].transform.position) <= 1f)
            {
                p_FiniteStateMachine.EnterState(FSMStateType.IDLE);
            }
            else if (p_NPC.PreyTranform != null)
            {
                //If close to pray, start chasing it.
                //if (Vector3.Distance(p_NavMeshAgent.transform.position, p_NPC.PrayTranform.position) <= 4f)
                //{
                //    Debug.Log("Pray detected");
                //    p_FiniteStateMachine.EnterState(FSMStateType.CHASE);
                //}

                if (p_NPC.PreyTranform != null)
                {
                    p_FiniteStateMachine.EnterState(FSMStateType.CHASE);
                }

            } 
        }
    }
    

    void SetDestination(ConnectedWaypoint destination)
    {
        if (p_NavMeshAgent != null && destination != null)
        {
            p_NavMeshAgent.SetDestination(destination.transform.position);
        }
    }

}
