using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SceneItem : MonoBehaviour
{
    public Item item;
    [SerializeField] private AnimationWoldSpaceCanvas PopUpCanvas;

    private void Awake()
    {
        item.transform = this.transform;
        PopUpCanvas = GetComponentInChildren<AnimationWoldSpaceCanvas>(true);
    }

    public void Contact()
    {
        if (item.Action.Equals(ItemAction.PICK))
            item.PickAction();
        else
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
        DisablePopUpInteract();
    }

    private void EnablePopUpInteract() => PopUpCanvas.Activate();
    private void DisablePopUpInteract() => PopUpCanvas.Deactivate();



}
