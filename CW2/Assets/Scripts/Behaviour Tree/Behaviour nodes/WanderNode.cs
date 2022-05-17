using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//This script picks a random position in the navmesh that is within a radius and tells the navmesh agent to move to it.
public class WanderNode : Node
{
    NavMeshAgent    m_NavMeshAgent;
    Transform       m_OriginPosition;

    float           m_WalkRadius = 7f;
    float           m_Speed = 1f;


    public WanderNode(NavMeshAgent navMeshAgent, Transform originPositiion)
    {
        m_NavMeshAgent   = navMeshAgent;
        m_OriginPosition = originPositiion;

        if (m_NavMeshAgent != null)
        {
            m_NavMeshAgent.speed = m_Speed;
        }

    }

    public override NodeState Execute()
    {
        if (m_NavMeshAgent != null)
        {
            //Has the navmesh reached the destination?
            if (m_NavMeshAgent.remainingDistance <= m_NavMeshAgent.stoppingDistance)
            {
                //Change agent speed
                m_NavMeshAgent.speed = m_Speed;

                //Update UI
                m_NavMeshAgent.GetComponent<Prey>().CurrentBehaviour = "Wandering";

                //Set destination at random location
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

    //Pick random location in the navmesh
    Vector3 RandomNavMeshLocation()
    {
        Vector3 finalPosition = Vector3.zero;

        //Random position inside sphere 
        Vector3 randomPosition = Random.insideUnitSphere * m_WalkRadius;

        //Add agent's current position
        randomPosition += m_OriginPosition.position;

        //Sample position on baked navmesh
        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, m_WalkRadius, 1))
        {
            finalPosition = hit.position;
        }

        return finalPosition;
    }
}
