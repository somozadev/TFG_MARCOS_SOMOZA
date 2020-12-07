using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SceneItem : MonoBehaviour
{
    public Item item; 

    private void Awake() 
    {
        item.transform = this.transform;
    }
    
    
}
