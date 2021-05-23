using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubscribeOpenDoor : SubscribeOnCompleteEvent
{
    [SerializeField] Animator doorAnim;
    [SerializeField] Collider exitCollider;
    public override void EventSub()
    {
        doorAnim.SetTrigger("Open");
        exitCollider.enabled = true;
    }
}
