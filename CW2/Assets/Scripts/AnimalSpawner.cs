using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AnimalSpawner : MonoBehaviour
{
    //Singleton
    public static AnimalSpawner Instance { get; private set; }

    [SerializeField] List<GameObject> preyAnimalPrefabs;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

    }

    //Spawn random animal
    public void SpawnAnimal(Vector3 position)
    {
        int randomAnimal = Random.Range(0, preyAnimalPrefabs.Count);

        GameObject prey = Instantiate(preyAnimalPrefabs[randomAnimal], position, Quaternion.identity);
        prey.GetComponent<Prey>().enabled = true;
    }
}
