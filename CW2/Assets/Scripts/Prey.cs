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
    bool m_IsHungry;

    public float Hunger { get { return m_Hunger; } set { m_Hunger = value; } }
    public bool IsHungry { get { return m_IsHungry; } set { m_IsHungry = value; } }

    float m_Thirst;
    bool m_IsThirsty;

    public float Thirst { get { return m_Thirst; } set { m_Thirst = value; } }
    public bool IsThirsty { get { return m_IsThirsty; } set { m_IsThirsty = value; } }



    void Start()
    {
        m_Hunger = 0;
        m_IsHungry = false;

        m_Thirst = 0;
        m_IsThirsty = false;

        m_NavMeshAgent = GetComponent<NavMeshAgent>();

        if (m_NavMeshAgent != null)
        {
            ConstructBehaviorTree();
        }
        else
            Debug.LogError("Behaviour tree: Nav mesh agent is null");

        ChaseState.onKillPrey += DestroyGameObject;

    }

    private void OnDestroy()
    {
        ChaseState.onKillPrey -= DestroyGameObject;
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    
    void Update()
    {
        //Debug.Log("IN PREY: " + m_Hunger);
        m_TopNode.Execute();
        if (m_TopNode.NodeState == NodeState.FAILURE)
        {
            Debug.LogError("All nodes failed!");
        }
    }

    void ConstructBehaviorTree()
    {
        FleeNode fleeNode           = new FleeNode(m_NavMeshAgent, m_Predator);
        IsInRangeNode inRangeNode   = new IsInRangeNode(transform,m_Predator, m_Range);
        WanderNode wanderNode       = new WanderNode(m_NavMeshAgent, transform);

        IsHungryNode isHungryNode   = new IsHungryNode(this);
        GoToFoodNode goToFoodNode   = new GoToFoodNode(m_NavMeshAgent, "Food");

        IsThirstyNode isThirsty     = new IsThirstyNode(this);
        GoToFoodNode goToWaterNode  = new GoToFoodNode(m_NavMeshAgent, "Water");

        SequenceNode fleeSequence   = new SequenceNode(new List<Node> {inRangeNode  ,   fleeNode      });
        SequenceNode hungerSequence = new SequenceNode(new List<Node> {isHungryNode ,   goToFoodNode  });
        SequenceNode thirstSequence = new SequenceNode(new List<Node> {isThirsty    ,   goToWaterNode });

        SelectorNode physicalNeedsNode = new SelectorNode(new List<Node> {thirstSequence, hungerSequence });

        m_TopNode = new SelectorNode(new List<Node> {fleeSequence, thirstSequence, hungerSequence, wanderNode });
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(transform.position, 20);
    //}

}
