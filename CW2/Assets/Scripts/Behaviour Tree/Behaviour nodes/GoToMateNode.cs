using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//This script is responsible for making the prey move to position of interest.
//Such as food or water.

public class GoToMateNode : Node
{
    NavMeshAgent m_NavMeshAgent;

    Prey m_Prey;

    //Constructor
    public GoToMateNode(NavMeshAgent navMeshAgent, Prey prey)
    {
        m_NavMeshAgent = navMeshAgent;
        m_Prey = prey;
    }


    //Executes each frame
    public override NodeState Execute()
    {
        //Do we have a nav mesh agent?
        if (m_NavMeshAgent != null)
        {
            if (m_Prey.Mate == null)
            {
                return NodeState.FAILURE;
            }
            else
            {

                GoToMate();
                return NodeState.SUCCESS;
            }
        }
        else
        {
            //Nav mesh agent was not initialised
            Debug.LogError("Go to mate node error: nav mesh agent is missing");
            return NodeState.FAILURE;
        }
    }

    void GoToMate()
    {
        m_Prey.CurrentBehaviour = "Going to mate";
        m_NavMeshAgent.speed = 3f;
       // Debug.Log("Found mate at: " + m_Prey.Mate.position);
        m_NavMeshAgent.SetDestination(m_Prey.Mate.position);
    }

}
