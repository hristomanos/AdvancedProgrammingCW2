using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{ 
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Prey"))
        {
            Prey prey = other.GetComponent<Prey>();
            if (prey.IsHungry)
            {

               // Debug.Log("Before: " + prey.Hunger);
                prey.Hunger -= 100;

                if (prey.Hunger <= 0)
                {
                    prey.Hunger = 0;
                    prey.IsHungry = false;
                }

               // Debug.Log("After: " + prey.Hunger);
                Destroy(gameObject);
            }
        }
    }

}
