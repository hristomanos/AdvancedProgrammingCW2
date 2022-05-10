using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInRangeNode : Node
{
    Prey m_Prey;

    float m_Range;

    float m_DistanceFromTarget;

    public IsInRangeNode(Prey prey, float range)
    {
        m_Prey = prey;
        m_Range = range;
    }

    public override NodeState Execute()
    {
        if (m_Prey.Predator != null)
        {

            m_DistanceFromTarget = Vector3.Distance(m_Prey.transform.position, m_Prey.Predator.position);

            //Predator is on sight
            if(m_DistanceFromTarget <= m_Range)
            {
                Debug.Log("Hunter is in range!");
                return NodeState.SUCCESS;
            }
            else
            {
                return NodeState.FAILURE;
            }
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
