using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{

    public bool interacting;
    [SerializeField] private GameObject interactedObj;
    public GameObject InteractedObject { get { return interactedObj; } }


    private void OnTriggerEnter(Collider col)
    {
        if (!interacting)
        {
            if (col.CompareTag("Interactable"))
            {
                interactedObj = col.transform.gameObject;
                col.GetComponent<SceneItem>().Contact();
                EnableGlowInteractable();

            }
        }

    }
    private void OnTriggerStay(Collider col)
    {
        if (!interacting)
        {
            if (col.CompareTag("Interactable"))
            {
                interactedObj = col.transform.gameObject;
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Interactable"))
        {
            interactedObj = col.transform.gameObject;
            col.GetComponent<SceneItem>().Leave();
            DisableGlowInteractable();
            interacting = false;
        }

    }

    private void EnableGlowInteractable()
    {

    }
    private void DisableGlowInteractable()
    {

    }


}
