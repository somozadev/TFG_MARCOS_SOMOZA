using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubscribeDropItem : SubscribeOnCompleteEvent
{
    public override void EventSub()
    {
        Collider[] c = GetComponents<Collider>();
        foreach (Collider col in c)
        {
            col.enabled = true;
            if (col.GetComponentsInChildren<Collider>().Length > 0)
            {
                foreach (Collider cc in col.GetComponentsInChildren<Collider>())
                {
                    cc.enabled = true;
                }
            }
        }

        StartCoroutine(gameObject.EnableAnim());
    }

}
