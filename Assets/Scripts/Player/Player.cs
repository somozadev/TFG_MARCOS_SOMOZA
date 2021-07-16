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
    [Header("Player rocks")]
    public PlayerRocks rocks;
    [Header("Player animator")]
    public Animator animator;




    private void Awake() { playerStats.ResetOnDeadEvent(); }
    private void Start()
    {
        playerStats.Level = PlayerPrefs.GetInt("level", 0);

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
        // playerStats.CurrentHp = playerStats.Hp;
        // playerStats.SoulCoins = 0;
        // playerStats.Spd = 5;
        // playerStats.Inventory.Clear();
        playerStats = playerStats.BaseStats();
        extraStats = extraStats.BaseStats();
        // rocks.ClearForLostRun();
        GameManager.Instance.player.rocks.ApplyForRun();

        statsCanvasController.gameObject.SetActive(true);
        currentItemsVisual.gameObject.SetActive(true);
        currentItemsVisual.ClearItemsVisuals();
    }

    //private void OnEnable()
    //{
    //    ResetForNewRun();
    //}
}
