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
    public void GetPlayerStats(PlayerStats playerStats) { JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("PlayerStats"), playerStats); }



    public void LoadGame()
    {

    }
    private void SaveGame()
    {

    }
    private void DeleteGame()
    {
        PlayerPrefs.DeleteAll();
        GameManager.Instance.player.playerStats = new PlayerStats(0,100,0,100,1,1,1,1,1,0,new List<Item>());
    }
}
