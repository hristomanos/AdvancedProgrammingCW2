using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//This script is responsible for moving the npc by calling the nav mesh agent depending on the fsm

//Emmbedd a rule that says whenever I create an instance of this, there has to be a nav mesh agent and a FSM attached to the same game object.
//If that is not the case, it will add them as components.
[RequireComponent(typeof(NavMeshAgent),typeof(FiniteStateMachine))]
public class NPC : MonoBehaviour
{
    [SerializeField] ConnectedWaypoint[] m_PatrolPoints;
    public ConnectedWaypoint[] PatrolPoints { get { return m_PatrolPoints; } }

    NavMeshAgent m_NavMeshAgent;
    FiniteStateMachine m_FiniteStateMachine;
    [SerializeField] Transform m_PrayTransform;
    public Transform PrayTranform { get { return m_PrayTransform; } }

    // Start is called before the first frame update
    void Awake()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_FiniteStateMachine = GetComponent<FiniteStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
