using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOnEnablePop : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.soundManager.Play("Pop");
    }
}
