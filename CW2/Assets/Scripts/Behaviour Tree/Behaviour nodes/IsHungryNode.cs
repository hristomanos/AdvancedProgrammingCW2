using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHungryNode : Node
{   
    float m_Hunger;
    Prey m_Prey;

    public IsHungryNode(Prey prey)
    {
        m_Prey = prey;
    }

    public override NodeState Execute()
    {
        if (m_Prey.Hunger >= 25)
        {
            m_Prey.IsHungry = true;
            //Debug.Log("Is Hungry");
            return NodeState.SUCCESS;
        }
        else
        {
            m_Prey.Hunger += Time.deltaTime;
            return NodeState.FAILURE;
        }
    }
}
