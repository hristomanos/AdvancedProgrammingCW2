using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//This script is responsible for making the prey move to position of interest.
//Such as food or water.

public class GoToFoodNode : Node
{
    NavMeshAgent m_NavMeshAgent;
    
    Transform m_Target;
    


    public GoToFoodNode(NavMeshAgent navMeshAgent)
    {
        m_NavMeshAgent = navMeshAgent;
        
    }

    public override NodeState Execute()
    {
        if (m_NavMeshAgent != null)
        {
            GoToFood();
            return NodeState.SUCCESS;
        }
        else
        {
            Debug.LogError("Go to food node error: nav mesh agent is missing");
            return NodeState.FAILURE;
        }
    }
       

    void GoToFood()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(m_NavMeshAgent.transform.position, 7);

           m_Target = targetsInViewRadius[0].transform;

            if (m_Target.CompareTag("Food"))
            {
                //Switch to chase state
                Debug.Log("Food detected!");
                m_NavMeshAgent.speed = 3f;
                m_NavMeshAgent.SetDestination(m_Target.position);
            }
        

    }
}
