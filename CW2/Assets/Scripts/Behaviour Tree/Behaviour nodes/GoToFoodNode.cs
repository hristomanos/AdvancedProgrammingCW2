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
    

    //Constructor
    public GoToFoodNode(NavMeshAgent navMeshAgent)
    {
        m_NavMeshAgent = navMeshAgent;    
    }


    //Executes each frame
    public override NodeState Execute()
    {
        //Do we have a nav mesh agent?
        if (m_NavMeshAgent != null)
        {
            //Yes, then seek food to eat
            GoToFood();
            return NodeState.SUCCESS;
        }
        else
        {
            //Nav mesh agent was not initialised
            Debug.LogError("Go to food node error: nav mesh agent is missing");
            return NodeState.FAILURE;
        }
    }
    
    //Pick closest piece of food.
    Transform FindClosestDistance(Collider[] targetsInViewRadius)
    {
        if (targetsInViewRadius.Length <= 0)
        {
            Debug.LogError("No food");
        }


        float minDistance = Vector3.Distance(m_NavMeshAgent.transform.position, targetsInViewRadius[0].transform.position);
        int   minIndex = 0;
        float distToTarget = 0.0f;

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            if (targetsInViewRadius[i].CompareTag("Food"))
            {

                distToTarget = Vector3.Distance(m_NavMeshAgent.transform.position, targetsInViewRadius[i].transform.position);

                if (distToTarget <= minDistance)
                {
                    minDistance = distToTarget;
                    minIndex = i;
                }

            }
        }

        return targetsInViewRadius[minIndex].transform;
    }

    //Look for food
    void FindFood()
    {
        //Check if you can see anything
        Collider[] targetsInViewRadius = Physics.OverlapSphere(m_NavMeshAgent.transform.position, 10);

        //Find closest one
        m_Target = FindClosestDistance(targetsInViewRadius);

        //Tell me which food you targeted
        Debug.Log(m_Target.transform.position);
        Debug.Log("Food detected!");
    }

    void GoToFood()
    {
        FindFood();
        m_NavMeshAgent.speed = 3f;
        m_NavMeshAgent.SetDestination(m_Target.position);
    }

}
