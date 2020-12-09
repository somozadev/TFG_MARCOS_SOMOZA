using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTextFromScheme : MonoBehaviour
{
    //not working


    [SerializeField] TMP_Text text;
    void Start()
    {
        text = GetComponent<TMP_Text>();

        if(GameManager.Instance.player.playerMovement.PlayerInput.defaultControlScheme.Equals("Keyboard"))
            text.text = "E";
        
        else if(GameManager.Instance.player.playerMovement.PlayerInput.defaultControlScheme.Equals("Gamepad"))
            text.text = "A";


    }

}
