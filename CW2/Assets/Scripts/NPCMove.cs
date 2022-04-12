using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//This script is responsible for instructing an npc agent to move using the nav mesh.
public class NPCMove : MonoBehaviour
{
    [SerializeField] Transform m_Destination;

    NavMeshAgent m_MeshAgent;


    // Start is called before the first frame update
    void Start()
    {
        //Assign the nav mesh component
        m_MeshAgent = GetComponent<NavMeshAgent>();

        //In case the nav mesh agent component is not attached to the gameobject.
        if (m_MeshAgent == null)
        {
            Debug.LogError("The nav mesh component is not attached to " + gameObject.name);
        }
        else
            SetDestination();

    }

    
    private void SetDestination()
    {
        //If there is a destination.
        if (m_Destination != null)
        {
            Vector3 targetVector = m_Destination.transform.position;
            m_MeshAgent.SetDestination(targetVector);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
