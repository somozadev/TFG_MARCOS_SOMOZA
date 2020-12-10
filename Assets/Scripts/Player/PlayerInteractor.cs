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
        interacting = GetComponentInParent<PlayerMovement>().IsInteracting;
        if (!interacting)
        {
            if (col.CompareTag("Interactable"))
            {
                interactedObj = col.transform.gameObject;
                if (interactedObj != null)
                {
                    if(interacting)
                        GetComponentInChildren<PlayerInteractor>().InteractedObject.GetComponent<SceneItem>().item.InteractAction();
                }
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        interacting = false;
        if (col.CompareTag("Interactable"))
        {
            interactedObj = null;
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
