using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SceneItem : MonoBehaviour
{
    public Item item;
    [SerializeField] private GameObject PopUpCanvas;

    private void Awake()
    {
        item.transform = this.transform;
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

    private void EnablePopUpInteract() => PopUpCanvas.SetActive(true);
    private void DisablePopUpInteract() => PopUpCanvas.SetActive(false);



}
