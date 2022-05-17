using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script defines the abstract class that includes the core functinoality that each node in the behaviour tree is required to implement.

[System.Serializable]
public abstract class Node
{
    protected NodeState p_NodeState;

    //A public enum declares the terminal state of each node. A single node can be succesful, unsuccesful or still running.
    public NodeState NodeState { get; }

    //Each node needs to be executed and return its result.
    public abstract NodeState Execute();
}


public enum NodeState
{
    RUNNING,
    SUCCESS,
    FAILURE,
}
