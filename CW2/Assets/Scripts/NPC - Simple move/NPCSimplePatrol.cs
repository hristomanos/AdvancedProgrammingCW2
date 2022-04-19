using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//This script is responsible for iterating through a list of waypoints that the agent is going to randomly pick one to go to.
public class NPCSimplePatrol : MonoBehaviour
{
    //Dictates whether the agent wait on each node.
    [SerializeField] bool m_PatrolWaiting;

    //The probability of switching direction.
    [SerializeField] float m_SwitchProbability = 0.2f;

    //The total time we wait at each node.
    [SerializeField] float m_TotalWaitTime = 3.0f;

    //List of patrol points
    [SerializeField] List<WayPoint> m_PatrolPoints;

    //Private variables for base behaviour.
    NavMeshAgent m_MeshAgent;
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
            //Otherwise, Check if there are at least two way points in the list for the patrolling behaviour to work
            if (m_PatrolPoints != null && m_PatrolPoints.Count >= 2)
            {       
                //Initialise patrol index
                m_CurrentPatrolIndex = 0;
                SetDestination();
            }
            else
                Debug.LogError("Insufficient amount of patrol points for basic patrolling behaviour");
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
                ChangePatrolPoint();
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
                ChangePatrolPoint();
                SetDestination();
            }
        }

    }

    private void SetDestination()
    {
        //If there is a destination.
        if (m_PatrolPoints != null)
        {
            Vector3 targetVector = m_PatrolPoints[m_CurrentPatrolIndex].transform.position;
            m_MeshAgent.SetDestination(targetVector);
            m_Travelling = true;
        }
    }

    /// <summary>
    /// Selects a new patrol point from the list, but with a small propability enables us to move forward or backward
    /// </summary>
    void ChangePatrolPoint()
    {
        if (Random.Range(0f,1f) <= m_SwitchProbability)
        {
            m_PatrolForward = !m_PatrolForward;
        }

        if (m_PatrolForward)
        {
            m_CurrentPatrolIndex = (m_CurrentPatrolIndex + 1) % m_PatrolPoints.Count;

            //Same as
            //m_CurrentPatrolIndex++;

            //if (m_CurrentPatrolIndex >= m_PatrolPoints.Count)
            //{
            //    m_CurrentPatrolIndex = 0;
            //}

        }
        else
        {
            if (-- m_CurrentPatrolIndex < 0)
            {
                m_CurrentPatrolIndex = m_PatrolPoints.Count - 1;
            }

            //Same as:
            //m_CurrentPatrolIndex--;

            //if (m_CurrentPatrolIndex < 0)
            //{
            //    m_CurrentPatrolIndex = m_PatrolPoints.Count - 1;
            //}
        }

    }


}
