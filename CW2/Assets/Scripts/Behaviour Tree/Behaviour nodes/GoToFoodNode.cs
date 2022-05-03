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

    String m_Tag;

   

    //Constructor
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


        float minDistance = float.MaxValue;
        int   minIndex = 0;
        float distToTarget = 0.0f;

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            if (targetsInViewRadius[i].CompareTag(m_Tag))
            {
                distToTarget = Vector3.Distance(m_NavMeshAgent.transform.position, targetsInViewRadius[i].transform.position);

                if (distToTarget <= minDistance)
                {
                    minDistance = distToTarget;
                    minIndex = i;
                    Debug.Log(m_Tag + " Found!");
                }
            }
        }

        if (targetsInViewRadius[minIndex].CompareTag(m_Tag))
        {
            return targetsInViewRadius[minIndex].transform;
        }
        else
        {
            Debug.LogError(m_Tag + " not found!");
            return targetsInViewRadius[minIndex].transform;
        }

    }

    //Look for food
    void FindFood()
    {
        //Check if you can see anything
        Collider[] targetsInViewRadius = Physics.OverlapSphere(m_NavMeshAgent.transform.position, 50);
       

        //Find closest one
        m_Target = FindClosestDistance(targetsInViewRadius);

        //Tell me which food you targeted
        Debug.Log(m_Target.transform.position);
        Debug.Log(m_Tag + " detected at " + m_Target.transform.position);
    }

  

    void GoToFood()
    {
        FindFood();
        m_NavMeshAgent.speed = 3f;
        m_NavMeshAgent.SetDestination(m_Target.position);
    }

}
