using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A sequence node needs all its children to be succesful in order for it to be  successful.
public class SequenceNode : Node
{
    //A list of nodes.
    List<Node> m_Nodes = new List<Node>();

    //A constructor that initialises the list of nodes.
    public SequenceNode(List<Node> nodes)
    {
        m_Nodes = nodes;
    }

    public override NodeState Execute()
    {
        //1.Go through each node.
        //2.If a node is running just flag it and move to the next node
        //3.If a node is successfull move on to the next node.
        //4.If a node is a failure then the whole sequence is a failure thus return failure.
        //5.If all nodes have been succesful the the whole sequence is succesful.
        bool isAnyChildRunning = false;
        foreach(Node node in m_Nodes)
        {
            // Each child evaluation will return a node state enum type
            switch (node.Execute())
            {
                case NodeState.RUNNING:
                    //Sets the node to running and moves on to the next child evaluation
                    isAnyChildRunning = true;
                    break;
                case NodeState.SUCCESS:
                    break;
                case NodeState.FAILURE:
                    //One child is a failure means that the whole sequence is a failure
                    p_NodeState = NodeState.FAILURE;
                    return p_NodeState;
                default:
                    break;
            }
        }
        //If we reached this points means that all children have been succesful.
        //Check if any child is running or else the sequence is a success
        p_NodeState = isAnyChildRunning ? NodeState.RUNNING : NodeState.SUCCESS;
        return p_NodeState;
    }
}
