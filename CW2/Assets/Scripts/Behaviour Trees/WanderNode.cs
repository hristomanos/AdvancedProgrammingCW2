using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderNode : Node
{
    NavMeshAgent m_NavMeshAgent;
    Transform m_OriginPosition;

    float m_WalkRadius = 7f;
    float m_Speed = 1f;


    public WanderNode(NavMeshAgent navMeshAgent, Transform originPositiion)
    {
        m_NavMeshAgent = navMeshAgent;
        m_OriginPosition = originPositiion;

        if (m_NavMeshAgent != null)
        {
            m_NavMeshAgent.speed = m_Speed;
            //m_NavMeshAgent.SetDestination(RandomNavMeshLocation());
        }

    }

    public override NodeState Execute()
    {
        if (m_NavMeshAgent != null)
        {
            if (m_NavMeshAgent.remainingDistance <= m_NavMeshAgent.stoppingDistance)
            {
                m_NavMeshAgent.SetDestination(RandomNavMeshLocation());
                return NodeState.SUCCESS;
            }
            else
            {
                return NodeState.RUNNING;
            }
        }
        else
        {
            Debug.LogError("Wander node error: Nav mesh agent is missing");
            return p_NodeState = NodeState.FAILURE;
        }
    }

    Vector3 RandomNavMeshLocation()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * m_WalkRadius;
        randomPosition += m_OriginPosition.position;
        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, m_WalkRadius, 1))
        {
            finalPosition = hit.position;
        }

        return finalPosition;
    }
}
