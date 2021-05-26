using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubscribeDropItemsObj : SubscribeOnCompleteEvent
{
    public override void EventSub()
    {
        gameObject.SetActive(true);
    }

}
