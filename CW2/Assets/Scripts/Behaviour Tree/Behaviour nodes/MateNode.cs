using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MateNode : Node
{
    NavMeshAgent m_NavMeshAgent;
    Prey         m_Prey;

    public MateNode(NavMeshAgent navMeshAgent, Prey prey)
    {
        m_NavMeshAgent = navMeshAgent;
        m_Prey = prey;
    }

    public override NodeState Execute()
    {
        //Do we have a nav mesh agent?
        if (m_NavMeshAgent != null)
        {
            if (!m_NavMeshAgent.pathPending)
            {
                if (m_NavMeshAgent.remainingDistance <= m_NavMeshAgent.stoppingDistance)
                {
                    if (!m_NavMeshAgent.hasPath || m_NavMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        m_Prey.UrgeToReproduce = 0;

                        //If female then spawn prey animals
                        if (m_Prey.Gender == Gender.MALE)
                        {
                            //Spawn prey animals
                            SpawnAnimal();
                        }

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
               // Debug.Log("Mating: path is pending!");
                return NodeState.FAILURE;
            }
        }
        else
        {
            //Nav mesh agent was not initialised
            Debug.LogError("Mate node error: nav mesh agent is missing");
            return NodeState.FAILURE;
        }
    }


    void SpawnAnimal()
    {
        //Instantiate random prefab next to the parents.
        AnimalSpawner.Instance.SpawnAnimal(m_Prey.transform.position + m_Prey.GetComponent<CapsuleCollider>().bounds.size);
    }
   
}
