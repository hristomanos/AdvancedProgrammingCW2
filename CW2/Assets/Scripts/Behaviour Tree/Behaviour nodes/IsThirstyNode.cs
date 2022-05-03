using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsThirstyNode : Node
{   
    float m_Hunger;
    Prey m_Prey;

    public IsThirstyNode(Prey prey)
    {
        m_Prey = prey;
    }

    public override NodeState Execute()
    {
        if (m_Prey.Thirst >= 10)
        {
            m_Prey.IsThirsty = true;
            Debug.Log("Is Thirsty");
            return NodeState.SUCCESS;
        }
        else
        {
            m_Prey.Thirst += Time.deltaTime;
            return NodeState.FAILURE;
        }
    }
}
