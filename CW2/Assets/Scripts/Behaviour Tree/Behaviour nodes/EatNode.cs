using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EatNode : Node
{
    NavMeshAgent m_NavMeshAgent;

    float m_Hunger;
    Prey m_Prey;

    public EatNode(NavMeshAgent navMeshAgent, Prey prey)
    {
        m_Prey = prey;
        m_Hunger = prey.Hunger;
        m_NavMeshAgent = navMeshAgent;
    }

    public override NodeState Execute()
    {
        if (m_NavMeshAgent != null)
        {
            if (!m_NavMeshAgent.pathPending)
            {
                if (m_NavMeshAgent.remainingDistance <= m_NavMeshAgent.stoppingDistance)
                {
                    if (!m_NavMeshAgent.hasPath || m_NavMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        Debug.Log(m_NavMeshAgent.remainingDistance + " <= " + m_NavMeshAgent.stoppingDistance);
                        Debug.Log("Eat");
                        Eat();
                        return NodeState.SUCCESS;
                    }
                    else
                    {
                        return NodeState.RUNNING;
                    }
                }
                else
                {
                    return NodeState.RUNNING;
                }
            }
            else
            {
                Debug.Log("Not path pending!");
                return NodeState.FAILURE;
            }
        }
        else
        {
            Debug.LogError("Eat node error: Nav mesh agent is missing");
            return p_NodeState = NodeState.FAILURE;
        }
    }


    void Eat()
    {
        Debug.Log("Before: " + m_Prey.Hunger);
        m_Prey.Hunger -= 100;
        m_Prey.IsHungry = false;

        if (m_Prey.Hunger <= 0)
        {
            m_Prey.Hunger = 0;
        }
       
        Debug.Log("After: " + m_Prey.Hunger);
    }

}
