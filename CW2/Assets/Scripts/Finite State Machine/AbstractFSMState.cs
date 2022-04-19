using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//This script is responsible for defining an abstract finite state machine class
//It declares the core functionality of a state
//We need to enter, update and exit a state
//Derived classes can use the default functions for entering and exiting a state but they need to add functionality to what happens at each state
//The class is a scriptable object so that we do not need to.
//The abstract class can dictate whether or not the state itself is in activated mode or not


/// <summary>
///Allows to monitor if the state machine is working as intended 
/// </summary>
public enum ExecutionState
{
    NONE, //??
    ACTIVATE,
    COMPLETED,
    TERMINATED,
};

public enum FSMStateType
{
    IDLE,
    PATROL,
    CHASE,
};


public abstract class AbstractFSMState : ScriptableObject
{
    public ExecutionState ExecutionState { get; protected set; }

    public FSMStateType StateType { get; protected set; }

    public bool EnteredState { get; protected set; }

    protected NavMeshAgent p_NavMeshAgent;
    protected NPC p_NPC;
    protected FiniteStateMachine p_FiniteStateMachine;

    public virtual void OnEnable()
    {
        ExecutionState = ExecutionState.NONE;
    }


    //Check if the state has been entered succesfully
    public virtual bool EnterState()
    {
        bool successMeshAgent = true;
        bool successNPC = true;

        //Does the nav mesh agent exist?
        successMeshAgent = (p_NavMeshAgent != null); //Success is true as long as navmesh is not null

        //Does the NPC exist?
        successNPC = (p_NPC != null);

        ExecutionState = ExecutionState.ACTIVATE;
        return successMeshAgent && successNPC;
    }


    //Abstract method for stating what each state does
    //It is not using Unity's update method to restrain if it is going to work for a particular tick
    public abstract void UpdateState();


    //Check if the state has been exited succesfully
    public virtual bool ExitState()
    {
        ExecutionState = ExecutionState.COMPLETED;
        return true;
    }

    public virtual void SetNavMeshAgent(NavMeshAgent navMeshAgent)
    {
        if (navMeshAgent != null)
        {
            p_NavMeshAgent = navMeshAgent;
        }
    }

    public virtual void SetExecutingNPC(NPC npc)
    {
        if (npc != null)
        {
            p_NPC = npc;
        }
    }

    public virtual void SetExecutingFSM(FiniteStateMachine finiteStateMachine)
    {
        if (finiteStateMachine != null)
        {
            p_FiniteStateMachine = finiteStateMachine;
        }
    }

}
