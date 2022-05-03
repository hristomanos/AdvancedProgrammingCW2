using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject m_Prefab;

    [Header("Circle spawner")]
    [SerializeField] int m_Radius;
    [SerializeField] int m_NumberOfSpawns;

    public float m_FoodRadius;
    public LayerMask m_FoodLayerMask;

    [Header("Terrain")]
    public LayerMask TerrainMask;


    void Start()
    {
        SpawnPrefabRandomLocation(m_Prefab);
    }


    void SpawnPrefabRandomLocation(GameObject prefab)
    {
        for (int i = 0; i < m_NumberOfSpawns; i++)
        {
            //Vector3 randomPosition = GetRandomPositionInACircle(transform.position, m_Radius);
            //GameObject gameObject = Instantiate(prefab,randomPosition, Quaternion.identity);

            InstantiateFood();

        }

    }

    Vector3 GetRandomPositionInACircle(Vector3 centre, int radius)
    {
        Vector2 unitCircle = Random.insideUnitCircle * radius;
        Vector3 randomPosition = centre + new Vector3(unitCircle.x, 0, unitCircle.y);

        return randomPosition;
    }

    private void InstantiateFood()
    {
        Vector3 position;
        int i = 0;
        const int MaxAttempts = 100;
        do
        {
            // Too many attempts!
            // Don't create a food
            if (i >= MaxAttempts)
            {
                Debug.Log("Too many attempts");
                return;
            }

            // insideUnitCircle returns (x,y), but we need (x,z)
            position = GetRandomPosition();


            //Vector2 unitCircle = Random.insideUnitCircle * Radius;
            //position = Centre.position +
            //    new Vector3(unitCircle.x, 0f, unitCircle.y);



            // Position is on the sky
            // We need to raycast down to find the terrain height
            position = TerrainHeightAt(position);
            i++;
        } while (IsTreeAt(position));

        Instantiate(m_Prefab, position, Quaternion.identity);

        //m_InstantiatedTrees.Add(Instantiate(prefab, position, rotation, transform));
    }

    private Vector3 TerrainHeightAt(Vector3 position)
    {
        RaycastHit hitInfo;
        bool hit = Physics.Raycast(position, Vector3.down, out hitInfo, 10f, TerrainMask);
        // Tree is not on top of the ground! (error?)
        if (!hit)
            return position;

        position.y = hitInfo.point.y;
        return position;
    }

    private Vector3 GetRandomPosition()
    {
        return transform.position + new Vector3
        (
            Random.Range(-m_Radius, +m_Radius),
            transform.position.y,
            Random.Range(-m_Radius, +m_Radius)
        );
    }

    private bool IsTreeAt(Vector3 position)
    {
        //Debug.DrawLine(position, position + Vector3.up * 10,
        //    Physics.CheckSphere(position, TreeRadius, TreeLayerMask) ?
        //    Color.red : Color.green, 10f
        //    );

        return Physics.CheckSphere(position, m_FoodRadius, m_FoodLayerMask);
    }


}
