using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Pray : MonoBehaviour
{

    [SerializeField] Transform m_Hunter;
    [SerializeField] float m_Range = 2f;

    NavMeshAgent m_NavMeshAgent;

    SelectorNode m_TopNode;

    // Start is called before the first frame update
    void Start()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();

        if (m_NavMeshAgent != null)
        {
            ConstructBehaviorTree();
        }
        else
            Debug.LogError("Behaviour tree: Nav mesh agent is null");

    }

    // Update is called once per frame
    void Update()
    {
        m_TopNode.Execute();
        if (m_TopNode.NodeState == NodeState.FAILURE)
        {
            Debug.LogError("All nodes failed!");
        }
    }

    void ConstructBehaviorTree()
    {
        FleeNode fleeNode = new FleeNode(m_NavMeshAgent, m_Hunter);
        IsInRangeNode inRangeNode = new IsInRangeNode(transform,m_Hunter, m_Range);
        WanderNode wanderNode = new WanderNode(m_NavMeshAgent, transform);

        SequenceNode fleeSequence = new SequenceNode(new List<Node> {inRangeNode, fleeNode });
        m_TopNode = new SelectorNode(new List<Node> { fleeSequence, wanderNode });
    }
  
}
