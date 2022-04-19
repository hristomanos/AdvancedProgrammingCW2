using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectedWaypoint : MonoBehaviour
{

    [SerializeField] float m_ConectivityRadius = 10f;
    [SerializeField] float debugDrawRadius = 1.0f;

    List<ConnectedWaypoint> m_AdjacentWaypoints;

    // Start is called before the first frame update
    void Awake()
    {
        //Grab all waypoints in the scene.
        GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

        //Create a list of waypoints I can refer to later
        m_AdjacentWaypoints = new List<ConnectedWaypoint>();

        for (int i = 0; i < allWaypoints.Length; i++)
        {
            ConnectedWaypoint nextWaypoint = allWaypoints[i].GetComponent<ConnectedWaypoint>();

            //In other words, we found a waypoint
            if (nextWaypoint != null)
            {
                //If it is within the connectivity radius and is not self
                if (Vector3.Distance(transform.position,nextWaypoint.transform.position) <= m_ConectivityRadius && nextWaypoint != this)
                {
                    m_AdjacentWaypoints.Add(nextWaypoint);
                }
            }

        }

       

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, debugDrawRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, m_ConectivityRadius);
    }

    public ConnectedWaypoint NextWayPoint(ConnectedWaypoint previousWaypoint)
    {
        if (m_AdjacentWaypoints.Count == 0)
        {
            //No waypoints? return null and log an error.
            Debug.LogError("Insufficient waypoint count");
            return null;
        }
        else if (m_AdjacentWaypoints.Count == 1 && m_AdjacentWaypoints.Contains(previousWaypoint))
        {
            //Only adjacent way point is the previous one? send that.
            return previousWaypoint;
        }
        else //otherwise find a random one that is  not the previous one.
        {
            ConnectedWaypoint nextWaypoint;
            int nextIndex = 0;

            do
            {
                nextIndex = Random.Range(0, m_AdjacentWaypoints.Count);
                nextWaypoint = m_AdjacentWaypoints[nextIndex];
            } while (nextWaypoint == previousWaypoint);

            return nextWaypoint;

        }

    }


    // Update is called once per frame
    void Update()
    {
            
    }
}
