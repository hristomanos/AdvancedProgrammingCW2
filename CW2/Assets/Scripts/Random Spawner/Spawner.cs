using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject m_Prefab;

    [Header("Circle spawner")]
    [SerializeField] int m_Radius;
    [SerializeField] int m_NumberOfSpawns;

    void Start()
    {
        SpawnPrefabRandomLocation(m_Prefab);
    }


    void SpawnPrefabRandomLocation(GameObject prefab)
    {
        for (int i = 0; i < m_NumberOfSpawns; i++)
        {
            Vector3 randomPosition = GetRandomPositionInACircle(transform.position, m_Radius);
            GameObject gameObject = Instantiate(prefab,randomPosition, Quaternion.identity);
        }

    }

    Vector3 GetRandomPositionInACircle(Vector3 centre, int radius)
    {
        Vector2 unitCircle = Random.insideUnitCircle * radius;
        Vector3 randomPosition = centre + new Vector3(unitCircle.x, 0, unitCircle.y);

        return randomPosition;
    }


   

}
