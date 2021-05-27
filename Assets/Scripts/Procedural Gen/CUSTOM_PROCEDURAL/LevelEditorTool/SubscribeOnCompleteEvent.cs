using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubscribeOnCompleteEvent : MonoBehaviour
{
    private void OnEnable()
    {
        if (GetComponentInParent<Room>() != null)
            GetComponentInParent<Room>().onRoomCompleted += EventSub;
    }
    private void OnDisable()
    {
        if (GetComponentInParent<Room>() != null)
            GetComponentInParent<Room>().onRoomCompleted -= EventSub;
    }
    public virtual void EventSub()
    {

    }
}
