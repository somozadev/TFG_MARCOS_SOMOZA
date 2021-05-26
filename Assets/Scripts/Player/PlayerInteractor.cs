using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{

    public bool interacting;
    public bool contact;
    public bool stay;
    public bool leave;
    [SerializeField] private GameObject interactedObj;
    public GameObject InteractedObject { get { return interactedObj; } }


    private void OnTriggerEnter(Collider col)
    {
        if (!interacting)
        {
            if (col.CompareTag("Interactable"))
            {
                contact = true;
                leave = false;
                stay = false;
                interactedObj = col.transform.gameObject;
                col.GetComponent<SceneItem>().Contact();
                EnableGlowInteractable();
                interacting = true;
            }
        }

    }
    private void OnTriggerStay(Collider col)
    {
        interacting = GetComponentInParent<PlayerMovement>().IsInteracting;
        if (interacting)
        {
            if (col.CompareTag("Interactable"))
            {
                contact = false;
                leave = false;
                stay = true;
                interactedObj = col.transform.gameObject;
                if (interactedObj != null)
                {
                    InteractedObject.GetComponent<SceneItem>().Use();
                    if (!InteractedObject.GetComponent<SceneItem>().action.Equals(ItemAction.BUY))
                        InteractedObject.GetComponent<SceneItem>().canInteract = false;

                }
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        interacting = false;
        if (col.CompareTag("Interactable"))
        {
            contact = false;
            leave = true;
            stay = false;
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
