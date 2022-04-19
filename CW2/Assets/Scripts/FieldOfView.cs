using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float g_ViewRadius;
    
    [Range(0,360)] public float g_ViewAngle;

    public LayerMask g_TargetMask;
    public LayerMask g_ObstaclesMask;

    int m_HunterMask;
    int m_PrayMask;

    public static bool s_HunterOnsight = false;
    public static bool s_PreyOnsight = false;

    private void Start()
    {
        m_HunterMask = 7;
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

            //If it is within the field of view
            if (Vector3.Angle(transform.forward,dirToTarget) < g_ViewAngle /2 )
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);

                //No obstacles in the way thus we can see the target
                //Do something with it!
                //if target is not occluded by obstacles
                if (!Physics.Raycast(transform.position,dirToTarget,distToTarget,g_ObstaclesMask))
                {
                    if (g_TargetMask.value == (g_TargetMask | (1 << 7)))
                    {
                        Debug.Log("Hunter detected!");
                        //Switch to flee state
                        //Let is in range node know that hunter is in range
                        //A flag to true
                        s_HunterOnsight = true;
                    }


                    if (g_TargetMask.value == (g_TargetMask | ( 1 << 8)))
                    {
                        Debug.Log("Pray detected!");
                        //Switch to chase state
                        s_PreyOnsight = true;
                    }



                }
            }
        }

        if (targetsInViewRadius.Length == 0 && s_HunterOnsight == true)
        {
            Debug.Log("Hunter not overlaping with sphere");
            s_HunterOnsight = false;
        }
        
        if(targetsInViewRadius.Length == 0 && s_PreyOnsight == true)
        {
            Debug.Log("Prey not overlaping with sphere");
            s_PreyOnsight = false;
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
