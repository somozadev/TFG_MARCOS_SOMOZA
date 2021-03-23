using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider))]
public class SceneItem : MonoBehaviour
{
    public bool canInteract;
    public Item item;
    private AnimationWoldSpaceCanvas PopUpCanvas;
    [SerializeField] Collider colliderObject;


    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (item != null)
        {
            canInteract = true;
            // item.Transform = this.transform;
            if (item.Action.Equals(ItemAction.INTERACT))
                PopUpCanvas = GetComponentInChildren<AnimationWoldSpaceCanvas>(true);
            if (item.Action.Equals(ItemAction.BUY))
            {
                colliderObject.gameObject.SetActive(false);
            }
            if (item.Type.Equals(ItemType.XP)) { }
        }
        // XpScaler();
    }

    public void Contact()
    {
        if (canInteract)
        {
            if (item.Action.Equals(ItemAction.PICK))
            {
                item.PickAction();
                Destroy(gameObject);
            }
            else if (item.Action.Equals(ItemAction.INTERACT))
                EnablePopUpInteract();
            else if (item.Action.Equals(ItemAction.BUY))
            {
                Debug.Log("Shall animate item");
                GetComponentInParent<ShopSlot>().SelectedItem();
            }
        }
    }
    public void Use()
    {
        if (canInteract)
        {
            if (item.Action.Equals(ItemAction.INTERACT))
            {
                item.InteractAction();
                DisablePopUpInteract();
                // GameManager.Instance.player.playerInteractor.InteractedObject.gameObject.tag = "Untagged";
            }
            else if (item.Action.Equals(ItemAction.BUY))
            {
                item.BuyAction();
            }
        }
    }
    public void Leave()
    {
        if (canInteract)
        {
            if (item.Action.Equals(ItemAction.INTERACT))
            {
                DisablePopUpInteract();
            }
            else if (item.Action.Equals(ItemAction.BUY))
            {
                GetComponentInParent<ShopSlot>().DeselectedItem();
                Debug.Log("Shall finish animating item if didnt bought");
                Debug.Log("Shall finish animating item if bought");
            }
        }
    }

    private void EnablePopUpInteract() => PopUpCanvas.Activate();
    private void DisablePopUpInteract() => PopUpCanvas.Deactivate();



    // private void XpScaler()
    // {
    //     Vector3 aux = (new Vector3(1, 1, 1) * item.Cuantity) / 100;
    //     if (aux.magnitude >= new Vector3(0.1f, 0.1f, 0.1f).magnitude)
    //         item.Transform.localScale = aux;
    //     else
    //         item.Transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

    //     item.Transform.GetComponent<SphereCollider>().radius = (item.Transform.GetComponent<SphereCollider>().radius * item.Transform.localScale.x) / 0.1f;
    // }

}
