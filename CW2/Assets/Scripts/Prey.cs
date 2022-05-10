using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum Gender
{
    MALE,
    FEMALE,
}


[RequireComponent(typeof(NavMeshAgent))]
public class Prey : MonoBehaviour
{
    //Attributes
    [Header("Attributes")]
    [SerializeField] int m_Age;
    [SerializeField] int m_Speed;

    [SerializeField] Gender m_Gender;

    [Header("Circle field of view")]
    [SerializeField] float m_Range = 2f;
    
    Transform m_Predator;
    Transform m_Mate;

    string m_CurrentBehaviour;

    //Navigation
    NavMeshAgent m_NavMeshAgent;

    //Behaviour tree
    SelectorNode m_TopNode;

    //Physical needs

    //HUNGER
    float m_Hunger;
    bool m_IsHungry;

    public float Hunger { get { return m_Hunger; } set { m_Hunger = value; } }
    public bool IsHungry { get { return m_IsHungry; } set { m_IsHungry = value; } }


    //THIRST
    float m_Thirst;
    bool m_IsThirsty;

    public float Thirst { get { return m_Thirst; } set { m_Thirst = value; } }
    public bool IsThirsty { get { return m_IsThirsty; } set { m_IsThirsty = value; } }

    //URGE TO REPRODUCE
    float m_UrgeToReproduce;
    bool m_HasUrgeToReproduce;

    public float UrgeToReproduce { get { return m_UrgeToReproduce; } set { m_UrgeToReproduce = value; } }
    public bool HasUrgeToReproduce { get { return m_HasUrgeToReproduce; } set { m_HasUrgeToReproduce = value; } }

    public Transform Mate { get => m_Mate; set => m_Mate = value; }
    public Gender Gender { get => m_Gender; set => m_Gender = value; }
    public Transform Predator { get => m_Predator; set => m_Predator = value; }
    public string CurrentBehaviour { get => m_CurrentBehaviour; set => m_CurrentBehaviour = value; }

    public void SetPredator(Transform predator)
    {
        m_Predator = predator.transform;
    }

    public void SetMate(Transform mate)
    {
        m_Mate = mate.transform;
    }

    void Start()
    {
        m_Age = 1;
        m_Speed = 1;

        m_Hunger = 0;
        m_IsHungry = false;

        m_Thirst = 0;
        m_IsThirsty = false;

        m_UrgeToReproduce = 0;
        m_HasUrgeToReproduce = false;

        m_CurrentBehaviour = "";

        m_NavMeshAgent = GetComponent<NavMeshAgent>();

        if (m_NavMeshAgent != null)
        {
            ConstructBehaviorTree();
        }
        else
            Debug.LogError("Behaviour tree: Nav mesh agent is null");
    }

    
    void Update()
    {
        //Execute all nodes
        m_TopNode.Execute();

        //If all nodes failed
        if (m_TopNode.NodeState == NodeState.FAILURE)
        {
            Debug.LogError("All nodes failed!");
        }
    }

    void ConstructBehaviorTree()
    {
        IsInRangeNode   inRangeNode         = new IsInRangeNode(this, m_Range);
        FleeNode        fleeNode            = new FleeNode(m_NavMeshAgent, this);

        IsHungryNode    isHungryNode        = new IsHungryNode(this);
        GoToFoodNode    goToFoodNode        = new GoToFoodNode(m_NavMeshAgent, "Food");

        IsThirstyNode   isThirsty           = new IsThirstyNode(this);
        GoToFoodNode    goToWaterNode       = new GoToFoodNode(m_NavMeshAgent, "Water");

        UrgeToReproduce urgeToReproduce     = new UrgeToReproduce(this);
        GoToMateNode    goToMateNode        = new GoToMateNode(m_NavMeshAgent, this);
        MateNode        mateNode            = new MateNode(m_NavMeshAgent, this);

        WanderNode      wanderNode          = new WanderNode(m_NavMeshAgent, transform);
        
        SelectorNode    searchForMateSelector = new SelectorNode(new List<Node> { mateNode, wanderNode });

        SequenceNode    fleeSequence        = new SequenceNode(new List<Node> {inRangeNode     ,   fleeNode                    });
        SequenceNode    thirstSequence      = new SequenceNode(new List<Node> {isThirsty       ,   goToWaterNode               });
        SequenceNode    hungerSequence      = new SequenceNode(new List<Node> {isHungryNode    ,   goToFoodNode                });
        SequenceNode    matingSequence      = new SequenceNode(new List<Node> {urgeToReproduce ,   goToMateNode     ,  mateNode});

        m_TopNode = new SelectorNode(new List<Node> { fleeSequence ,thirstSequence, hungerSequence , matingSequence, wanderNode  });
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Predator"))
        {
            Destroy(gameObject);
        }
    }    
}
