using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player current stats")]
    public PlayerStats playerStats;
    [Header("Player interactor")]
    public PlayerInteractor playerInteractor;
    [Header("Player movement")]
    public PlayerMovement playerMovement; 
}
