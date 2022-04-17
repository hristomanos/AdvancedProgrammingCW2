using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float g_ViewRadius;
    
    [Range(0,360)] public float g_ViewAngle;

    public LayerMask g_TargetMask;
    public LayerMask g_ObstaclesMask;

    private void Start()
    {
        StartCoroutine("FindTargetsWithDelay", 0.2f);
    }


    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }


    void FindVisibleTargets()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, g_ViewRadius,g_TargetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward,dirToTarget) < g_ViewAngle /2 )
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position,dirToTarget,distToTarget,g_ObstaclesMask))
                {
                    //No obstacles in the way thus we can see the target
                    //Do something with it!
                    Debug.Log("Hunter detected!");
                }
            }
        }

    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
