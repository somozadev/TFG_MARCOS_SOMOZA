using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player extra stats")]
    public ExtraStats extraStats;
    [Header("Player current stats")]
    public PlayerStats playerStats;
    [Header("Player interactor")]
    public PlayerInteractor playerInteractor;
    [Header("Player movement")]
    public PlayerMovement playerMovement; 
    [Header("UI right side for current inventory")]
    public CurrentItemsVisual currentItemsVisual;

    private void Start() 
    {
        // DataController.Instance.GetPlayerStats(playerStats);    
    }
    private void OnApplicationQuit() 
    {
        DataController.Instance.SetPlayerStats(playerStats);   
    }

}
