using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInRangeNode : Node
{
    Transform m_OriginPosition;
    Transform m_TargetPosition;
    float m_Range;

    float m_DistanceFromTarget;

    public IsInRangeNode(Transform originPosition, Transform targetPosition, float range)
    {
        m_OriginPosition = originPosition;
        m_TargetPosition = targetPosition;
        m_Range = range;
    }

    public override NodeState Execute()
    {
        m_DistanceFromTarget = Vector3.Distance(m_OriginPosition.position, m_TargetPosition.position);
        if (m_DistanceFromTarget <= m_Range)
        {
            Debug.Log("Hunter is in range!");
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
