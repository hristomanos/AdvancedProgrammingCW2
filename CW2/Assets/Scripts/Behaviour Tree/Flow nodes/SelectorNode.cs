using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : Node
{
    //A list of nodes.
    List<Node> m_Nodes = new List<Node>();

    public SelectorNode(List<Node> nodes)
    {
        m_Nodes = nodes;
    }

    public override NodeState Execute()
    {
        foreach (Node node in m_Nodes)
        {
            switch (node.Execute())
            {
                case NodeState.RUNNING:
                    p_NodeState = NodeState.RUNNING;
                    return p_NodeState;
                case NodeState.SUCCESS:
                    p_NodeState = NodeState.SUCCESS;
                    return p_NodeState;
                case NodeState.FAILURE:
                    break;
                default:
                    break;
            }
        }
        //If we reached this points means that no child was succesful thus the how selector is a failure
        p_NodeState = NodeState.FAILURE;
        return p_NodeState;
    }
}
