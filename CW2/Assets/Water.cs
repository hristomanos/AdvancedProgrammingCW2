using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Prey"))
        {

            Prey prey = other.GetComponent<Prey>();
            if (prey.IsThirsty)
            {

                Debug.Log("Thirst || Before: " + prey.Thirst);
                prey.Thirst -= 100;

                if (prey.Thirst <= 0)
                {
                    prey.Thirst = 0;
                    prey.IsThirsty = false;
                }

                Debug.Log("Thirst || After: " + prey.Thirst);

            }
        }
    }

}
