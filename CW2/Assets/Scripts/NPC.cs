using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//This script is responsible for moving the npc by calling the nav mesh agent depending on the finite state machine

//Embed a rule that says whenever I create an instance of this, there has to be a nav mesh agent and a FSM attached to the same game object.
//If that is not the case, it will add them as components.
[RequireComponent(typeof(NavMeshAgent),typeof(FiniteStateMachine))]
public class NPC : MonoBehaviour
{
    [SerializeField] ConnectedWaypoint[] m_PatrolPoints;
    public ConnectedWaypoint[] PatrolPoints { get { return m_PatrolPoints; } }

    NavMeshAgent m_NavMeshAgent;
    FiniteStateMachine m_FiniteStateMachine;

    //Target
    Transform m_PreyTransform;
    public Transform PreyTranform { get { return m_PreyTransform; } }

    //Target was caught
    bool m_PreyWasCought;
    public bool PreyWasCought { get { return m_PreyWasCought; } set { m_PreyWasCought = value; } }

    // Start is called before the first frame update
    void Awake()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_FiniteStateMachine = GetComponent<FiniteStateMachine>();
        m_PreyWasCought = false;

        if (m_PatrolPoints != null)
        {
            FillPatrolPoints();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Prey"))
        {
            m_PreyWasCought = true;
        }
    }

    void FillPatrolPoints()
    {
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        
        m_PatrolPoints = new ConnectedWaypoint[waypoints.Length];

        for (int i = 0; i < m_PatrolPoints.Length; i++)
        {
            m_PatrolPoints[i] = waypoints[i].GetComponent<ConnectedWaypoint>();
        }
    }

    public void SetTarget(Transform target)
    {
        m_PreyTransform = target.transform;
    }

}
