using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public abstract class AbstractFSMState : ScriptableObject
{
    public ExecutionState ExecutionState { get; protected set; }

    public virtual void OnEnable()
    {
        ExecutionState = ExecutionState.NONE;
    }


    //Check if the state has been entered succesfully
    public virtual bool EnterState()
    {
        ExecutionState = ExecutionState.ACTIVATE;
        return true;
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


}
