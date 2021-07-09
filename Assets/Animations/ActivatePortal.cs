using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePortal : MonoBehaviour
{    
    [SerializeField] GameObject portal;
    public void Activateportal()
    {
        portal.SetActive(true);
    }
}
