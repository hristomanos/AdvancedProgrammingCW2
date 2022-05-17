using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrgeToReproduce : Node
{   
    Prey m_Prey;

    public UrgeToReproduce(Prey prey)
    {
        m_Prey = prey;
    }

    public override NodeState Execute()
    {
        if (m_Prey.UrgeToReproduce >= 20)
        {
            m_Prey.HasUrgeToReproduce = true;
            return NodeState.SUCCESS;
        }
        else
        {
            m_Prey.UrgeToReproduce += Time.deltaTime;
            return NodeState.FAILURE;
        }
    }
}
