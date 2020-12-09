using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SceneItem : MonoBehaviour
{
    public Item item;
    private AnimationWoldSpaceCanvas PopUpCanvas;

    private void Awake()
    {
        item.transform = this.transform;
        if (item.Action.Equals(ItemAction.INTERACT))
            PopUpCanvas = GetComponentInChildren<AnimationWoldSpaceCanvas>(true);
    }

    public void Contact()
    {
        if (item.Action.Equals(ItemAction.PICK))
        {
            item.PickAction();
            Destroy(gameObject);
        }
        else if (item.Action.Equals(ItemAction.INTERACT))
            EnablePopUpInteract();
    }
    public void Use()
    {
        if (item.Action.Equals(ItemAction.INTERACT))
        {
            item.InteractAction();
            DisablePopUpInteract();
        }
    }
    public void Leave()
    {
        if (item.Action.Equals(ItemAction.INTERACT))
        {
            DisablePopUpInteract();
        }
    }

    private void EnablePopUpInteract() => PopUpCanvas.Activate();
    private void DisablePopUpInteract() => PopUpCanvas.Deactivate();



}
