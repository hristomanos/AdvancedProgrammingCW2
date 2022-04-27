using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




[RequireComponent(typeof(NavMeshAgent))]
public class Prey : MonoBehaviour
{

    [SerializeField] Transform m_Predator;
    [SerializeField] float m_Range = 2f;
    

    NavMeshAgent m_NavMeshAgent;

    SelectorNode m_TopNode;

    float m_Hunger;    
    
    void Start()
    {
        m_Hunger = 0;
        m_NavMeshAgent = GetComponent<NavMeshAgent>();

        if (m_NavMeshAgent != null)
        {
            ConstructBehaviorTree();
        }
        else
            Debug.LogError("Behaviour tree: Nav mesh agent is null");

        ChaseState.onKillPrey += KillYourself;

    }

    private void OnDestroy()
    {
        ChaseState.onKillPrey -= KillYourself;
    }

    void KillYourself()
    {
        Destroy(gameObject);
    }

    
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
        FleeNode fleeNode = new FleeNode(m_NavMeshAgent, m_Predator);
        IsInRangeNode inRangeNode = new IsInRangeNode(transform,m_Predator, m_Range);
        WanderNode wanderNode = new WanderNode(m_NavMeshAgent, transform);

        IsHungryNode isHungryNode = new IsHungryNode();
        GoToFoodNode goToFoodNode = new GoToFoodNode(m_NavMeshAgent);

        SequenceNode fleeSequence = new SequenceNode(new List<Node> {inRangeNode, fleeNode });
        SequenceNode hungerSequence = new SequenceNode(new List<Node> {isHungryNode,  goToFoodNode});
        m_TopNode = new SelectorNode(new List<Node> { hungerSequence });
    }
  

    void IncreaseHunger()
    {
        m_Hunger += Time.deltaTime;


    }
}
