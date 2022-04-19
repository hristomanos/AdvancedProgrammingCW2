using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverterNode : Node
{
    //A list of nodes.
    protected Node m_Node;

    public InverterNode(Node node)
    {
        m_Node = node;
    }

    public override NodeState Execute()
    {
        switch (m_Node.Execute())
        {
            case NodeState.RUNNING:
                p_NodeState = NodeState.RUNNING;
                break;

            //If the child is a success, the inverter makes it a failure
            case NodeState.SUCCESS:
                p_NodeState = NodeState.FAILURE;
                break;

            //Thus a failure becomes a success
            case NodeState.FAILURE:
                p_NodeState = NodeState.SUCCESS;
                break;
            default:
                break;
        }

        return p_NodeState;
    }
}
