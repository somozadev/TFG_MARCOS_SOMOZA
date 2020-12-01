using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{

    public bool interacting;
    [SerializeField] private GameObject interactedObj;



    private void OnTriggerEnter(Collider col)
    {
        if (!interacting)
        {
            if (col.CompareTag("Interactable"))
            {
                interactedObj = col.transform.gameObject;
                col.GetComponent<SceneItem>().item.TriggerAction();
                Destroy(col.gameObject);
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
