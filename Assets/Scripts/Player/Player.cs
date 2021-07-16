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
    [Header("Player particles")]
    public PlayerParticles particles;
    [Header("UI stats canvas")]
    public StatsCanvasController statsCanvasController;
    [Header("Player animator")]
    public Animator animator;




    private void Awake() { playerStats.ResetOnDeadEvent(); }
    private void Start()
    {
        // DataController.Instance.GetPlayerStats(playerStats);    

    }
    private void OnApplicationQuit()
    {
        if (DataController.Instance.newRun)
        {
            DataController.Instance.SetPlayerStats(playerStats);
            PlayerPrefs.Save();
        }
    }
    public void ResetForNewRun()
    {
        animator.SetBool("Invincible", false);
        playerStats.CurrentHp = playerStats.Hp;
        playerStats.SoulCoins = 0;
        statsCanvasController.gameObject.SetActive(true);
        currentItemsVisual.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        ResetForNewRun();
    }
}
