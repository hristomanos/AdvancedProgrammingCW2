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
    
    Transform    m_Target;

    String       m_Tag;

    public GoToFoodNode(NavMeshAgent navMeshAgent, String tag)
    {
        m_NavMeshAgent = navMeshAgent;
        m_Tag = tag;
    }


    //Executes each frame
    public override NodeState Execute()
    {
        //Do we have a nav mesh agent?
        if (m_NavMeshAgent != null)
        {
            //Yes, then seek food to eat
            GoToTarget();
            return NodeState.SUCCESS;
        }
        else
        {
            //Nav mesh agent was not initialised
            Debug.LogError("Go to food node error: nav mesh agent is missing");
            return NodeState.FAILURE;
        }
    }
    
    //Find minumum distance to item.
    Transform FindClosestDistance(Collider[] targetsInViewRadius)
    {

        float minDistance = float.MaxValue;
        int   minIndex = 0;
        float distToTarget = 0.0f;

        //Find the closest distance to the target
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            if (targetsInViewRadius[i].CompareTag(m_Tag))
            {
                distToTarget = Vector3.Distance(m_NavMeshAgent.transform.position, targetsInViewRadius[i].transform.position);

                if (distToTarget <= minDistance)
                {
                    minDistance = distToTarget;
                    minIndex = i;
                   
                }
            }
        }

        //Check if the object found is of desired type
        if (targetsInViewRadius[minIndex].CompareTag(m_Tag))
        {
            return targetsInViewRadius[minIndex].transform;
        }
        else
        {
           //Else return the first object you found
            return targetsInViewRadius[minIndex].transform;
        }

    }

    void FindTarget()
    {
        //Check if you can see anything
        Collider[] targetsInViewRadius = Physics.OverlapSphere(m_NavMeshAgent.transform.position, 50);
       
        //Find closest target
        m_Target = FindClosestDistance(targetsInViewRadius);
    }

  

    void GoToTarget()
    {
        //Search for item
        FindTarget();

        //Update UI
        m_NavMeshAgent.GetComponent<Prey>().CurrentBehaviour = "Searching for " + m_Tag;

        //Set agent destination
        m_NavMeshAgent.speed = 3f;
        m_NavMeshAgent.SetDestination(m_Target.position);
    }

}
