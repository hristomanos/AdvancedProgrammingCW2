using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//Class taken from tutorial by a Youtube channel called table flip games
//Link: https://www.youtube.com/watch?v=21yDDUKCQOI&t=2s&ab_channel=TableFlipGames

public class FiniteStateMachine : MonoBehaviour
{
    
    AbstractFSMState m_CurrentState;

    [SerializeField] List<AbstractFSMState> m_ValidStates;


    Dictionary<FSMStateType, AbstractFSMState> m_FSMStates;

    // Start is called before the first frame update
    void Awake()
    {
        m_CurrentState = null;

        m_FSMStates = new Dictionary<FSMStateType, AbstractFSMState>();

        NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
        NPC npc = GetComponent<NPC>();

        foreach (AbstractFSMState state in m_ValidStates)
        {
            state.SetExecutingFSM(this);
            state.SetExecutingNPC(npc);
            state.SetNavMeshAgent(navMeshAgent);
            m_FSMStates.Add(state.StateType, state);
        }
    }

    private void Start()
    {
        EnterState(FSMStateType.IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CurrentState != null)
        {
            m_CurrentState.UpdateState();
        }
    }

    //Increases readability by collapsing whatever is within this region
    #region STATE MANAGEMENT

    public void EnterState(AbstractFSMState nextState)
    {
        if (nextState == null)
        {
            return;
        }

        if (m_CurrentState != null)
        {
            m_CurrentState.ExitState();
        }
        m_CurrentState = nextState;
        m_CurrentState.EnterState();
    }

    public void EnterState(FSMStateType stateType)
    {
        if (m_FSMStates.ContainsKey(stateType))
        {
            AbstractFSMState nextState = m_FSMStates[stateType];

            EnterState(nextState);
        }
    }

    #endregion

}
