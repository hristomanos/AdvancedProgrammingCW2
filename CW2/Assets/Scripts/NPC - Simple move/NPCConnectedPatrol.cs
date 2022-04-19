using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//This script is responsible for iterating through a list of waypoints that the agent is going to randomly pick one to go to.
public class NPCConnectedPatrol : MonoBehaviour
{
    //Dictates whether the agent wait on each node.
    [SerializeField] bool m_PatrolWaiting;

    //The total time we wait at each node.
    [SerializeField] float m_TotalWaitTime = 3.0f;

    //Private variables for base behaviour.
    NavMeshAgent m_MeshAgent;
    ConnectedWaypoint m_PreviousWaypoint;
    ConnectedWaypoint m_CurrentWaypoint;

    int m_CurrentPatrolIndex;
    bool m_Travelling;
    bool m_Waiting;
    bool m_PatrolForward;
    float m_WaitTimer;

    private void Start()
    {
        //Assign nav mesh agent to the script.
        m_MeshAgent = GetComponent<NavMeshAgent>();

        //If you don't find the nav mesh agent, then log an error
        if (m_MeshAgent == null)
        {
            Debug.LogError("Nav mesh agent is not attached to " + gameObject.name);
        }
        else
        {
            if (m_CurrentWaypoint == null)
            {
                GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

                if (allWaypoints.Length > 0)
                {
                  
                    while (m_CurrentWaypoint == null)
                    {
                        int random = Random.Range(0, allWaypoints.Length);
                        ConnectedWaypoint startingWaypoint = allWaypoints[random].GetComponent<ConnectedWaypoint>();

                        if (startingWaypoint != null)
                        {
                            m_CurrentWaypoint = startingWaypoint;
                           
                        }

                    }
                }
                else
                {
                    Debug.LogError("Failed to find any waypoints for use in the scene");
                }
            }
             
            SetDestination();
        }
    }


    private void Update()
    {
        //If the agent is close to its destination
        if (m_Travelling && m_MeshAgent.remainingDistance <= 0.1f)
        {
            m_Travelling = false;

            //Check if the agent needs to wait
            if (m_PatrolWaiting)
            {
                m_Waiting = true;
                m_WaitTimer = 0;
            }
            else
            {
                //Otherwise set a new destination
               
                SetDestination();
            }
        }

        
        if (m_Waiting)
        {
            //Count while waiting
            m_WaitTimer += Time.deltaTime;
            if (m_WaitTimer >= m_TotalWaitTime)
            {
                m_Waiting = false;

                //Set new destination when done waiting
              
                SetDestination();
            }
        }

    }

    private void SetDestination()
    {
        ConnectedWaypoint nextWaypoint = m_CurrentWaypoint.NextWayPoint(m_PreviousWaypoint);
        m_PreviousWaypoint = m_CurrentWaypoint;
        m_CurrentWaypoint = nextWaypoint;

        Vector3 targetVector = m_CurrentWaypoint.transform.position;
        m_MeshAgent.SetDestination(targetVector);
        m_Travelling = true;
    }
}
