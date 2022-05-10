using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    Ray ray;
    RaycastHit raycastHit;
    [SerializeField] LayerMask m_MouseLayerMask;

    [SerializeField] GameObject m_PreyPrefab;
    [SerializeField] GameObject m_Predator;

    [SerializeField] GameObject m_Panel;

    [SerializeField] Slider m_HungerSlider;
    [SerializeField] Slider m_ThirstSlider;
    [SerializeField] Slider m_UrgeToReproduceSlider;

    [SerializeField] TextMeshProUGUI m_BehaviourText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TakeMousePosOnScreen();
        SelectObjects();
    }

    public void StartSimButton()
    {
        EnableAnimalScriptsInScene();
    }


    void EnableAnimalScriptsInScene()
    {
        GameObject[] predators =  GameObject.FindGameObjectsWithTag("Predator");
        GameObject[] preys     = GameObject.FindGameObjectsWithTag("Prey");

        //yield return new WaitForSeconds();

        foreach (GameObject predator in predators)
        {
            predator.GetComponent<FiniteStateMachine>().enabled = true;
        }

        foreach (GameObject prey in preys)
        {
            prey.GetComponent<Prey>().enabled = true;
        }

    }


    void TakeMousePosOnScreen()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out raycastHit, 1000, m_MouseLayerMask))
            {
                //Debug.DrawLine(ray.origin, raycastHit.point,Color.red);
                //Debug.Log("HIT! || " + raycastHit.point);
                //Debug.Log(raycastHit.collider.tag);

                Instantiate(m_PreyPrefab,raycastHit.point, Quaternion.identity);

            }
        }
        //else if (Input.GetMouseButtonDown(1))
        //{
        //    if (Physics.Raycast(ray, out raycastHit, 1000, m_MouseLayerMask))
        //    {
        //        Instantiate(m_Predator, raycastHit.point, Quaternion.identity);
        //    }
        //}

    }
     
    [SerializeField] LayerMask selectableLayer;
    GameObject selectedObject;
    GameObject highlightedObject;
    //Ray ray;
    RaycastHit hitData;
    Color m_SelectedObjectColor;
    Prey m_SelectedPrey;

    void SelectObjects()
    {
    
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hitData, 1000, selectableLayer))
        {
            highlightedObject = hitData.transform.gameObject;
            if (Input.GetMouseButtonDown(0))
            {
                if (selectedObject != null)
                {
                    selectedObject.GetComponent<Renderer>().material.color = m_SelectedObjectColor;
                }
                selectedObject = hitData.transform.gameObject;
                m_SelectedObjectColor = selectedObject.GetComponent<Renderer>().material.color;
                selectedObject.GetComponent<Renderer>().material.color = Color.red;
                m_Panel.SetActive(true);

                if (selectedObject.CompareTag("Prey"))
                {
                    m_SelectedPrey = selectedObject.GetComponent<Prey>();
                }

            }
        }
        else
        {

            highlightedObject = null;
            if (Input.GetMouseButtonDown(0))
            {
                if (selectedObject != null)
                {
                    selectedObject.GetComponent<Renderer>().material.color = m_SelectedObjectColor;
                }
                m_Panel.SetActive(false);
                selectedObject = null;
            }
        }

        if (selectedObject != null && m_SelectedPrey != null)
        {
            m_HungerSlider.value = m_SelectedPrey.Hunger;
            m_ThirstSlider.value = m_SelectedPrey.Thirst;
            m_UrgeToReproduceSlider.value = m_SelectedPrey.UrgeToReproduce;
            m_BehaviourText.text = m_SelectedPrey.CurrentBehaviour;
        }

    }

}
