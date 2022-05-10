using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleeNode : Node
{
    NavMeshAgent m_NavMeshAgent;
    //Transform m_FleeingFromPosition;
    Prey m_prey;
    public FleeNode(NavMeshAgent navMeshAgent, Prey prey)
    {
        m_NavMeshAgent = navMeshAgent;
        m_prey = prey;
    }

    public override NodeState Execute()
    {
        if (m_NavMeshAgent != null)
        {
            Flee();
            return NodeState.SUCCESS;
        }
        else
        {
            Debug.LogError("Flee node error: nav mesh agent is missing");
            return NodeState.FAILURE;
        }
    }

    void Flee()
    {
        m_prey.CurrentBehaviour = "Fleeing";
        Vector3 directionToTarget = m_NavMeshAgent.transform.position - m_prey.Predator.position;
        m_NavMeshAgent.speed = 5f;
        m_NavMeshAgent.SetDestination(m_NavMeshAgent.transform.position + directionToTarget);
    }

}
