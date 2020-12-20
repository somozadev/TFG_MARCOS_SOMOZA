using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataController : MonoBehaviour
{

    #region Singleton
    private static DataController instance;
    public static DataController Instance { get { return instance; } }
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        DontDestroyOnLoad(gameObject);
    }
    #endregion


    public void SetPlayerStats(PlayerStats playerStats) { PlayerPrefs.SetString("PlayerStats", JsonUtility.ToJson(playerStats)); }
    public void GetPlayerStats(PlayerStats playerStats)
    {
        PlayerStats stats = new PlayerStats(0,100,0,100,50,1,5,1,1,1,0,new List<Item>());
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("PlayerStats",JsonUtility.ToJson(stats)), playerStats);
    }



    public void LoadGame()
    {

    }
    private void SaveGame()
    {

    }
    public void DeleteGame()
    {
        PlayerPrefs.DeleteAll();
        GameManager.Instance.player.playerStats = new PlayerStats(0,100,0,100,50,1,5,1,1,1,0,new List<Item>());
    }
}
