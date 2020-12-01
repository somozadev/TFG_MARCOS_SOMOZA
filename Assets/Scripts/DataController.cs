using UnityEngine;

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

    [SerializeField] private PlayerStats _playerStats;

    public void GetPlayerStats(PlayerStats playerStats) { PlayerPrefs.SetString("PlayerStats", JsonUtility.ToJson(playerStats)); }
    public void SetPlayerStats(PlayerStats playerStats) { JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("PlayerStats"), playerStats); }



    public void LoadGame()
    {
        SetPlayerStats(_playerStats);
    }
    public void SaveGame()
    {
        GetPlayerStats(_playerStats);
    }
}
