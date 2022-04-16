using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script is responsible for defining the core functinoality for each node in the behaviour tree

[System.Serializable]
public abstract class Node
{
    protected NodeState p_NodeState;
    public NodeState NodeState { get; }


    public abstract NodeState Execute();
}

public enum NodeState
{
    RUNNING,
    SUCCESS,
    FAILURE,
}
