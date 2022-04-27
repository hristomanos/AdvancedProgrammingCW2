using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHungryNode : Node
{   
    float m_Hunger;

    public IsHungryNode()
    {
        m_Hunger = 0;
    }

    public override NodeState Execute()
    {
        Debug.Log("Hunger: " + m_Hunger);
        if (m_Hunger >= 20)
        {
            Debug.Log("Is Hungry");
            return NodeState.SUCCESS;
        }
        else
        {
           
            m_Hunger += Time.deltaTime;
            return NodeState.FAILURE;
        }
    }
}
