using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameDataController : MonoBehaviour
{

    [SerializeField] GameData loadedGame;
    [SerializeField] int ereaseIndex;


    // Update is called once per frame
    void start()
    {
        ereaseIndex = -1;
    }


    public void SetGameData(int id, GameData gameData)
    {
        string path = Application.persistentDataPath + "/savefile_" + id + ".data";
        File.WriteAllText(path, JsonUtility.ToJson(gameData));
    }
    public void GetGameData(int id)
    {
        ereaseIndex = id;
        string path = Application.persistentDataPath + "/savefile_" + id + ".data";
        Debug.Log(path);
        if (File.Exists(path))
        {
            Debug.Log("existe");
            loadedGame = (GameData)JsonUtility.FromJson(File.ReadAllText(path), typeof(GameData));
        }
        else
        {
            SetGameData(id, new GameData());
            GetGameData(id);
        }


        //UPDATE VISUALS 
    }

    public void EreaseGameData()
    {
        string path = Application.persistentDataPath + "/savefile_" + ereaseIndex + ".data";
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        //UPDATE VISUALS 


    }

}

[System.Serializable]
public class GameData
{
    public PlayerStats playerStats;
    public float gameCompletePercentaje;
    public List<bool> itemsUnlocked;

    public GameData() { }
    public GameData(PlayerStats playerStats, float gameCompletePercentaje, List<bool> itemsUnlocked)
    {
        this.playerStats = playerStats;
        this.gameCompletePercentaje = gameCompletePercentaje;
        this.itemsUnlocked = itemsUnlocked;
    }
    //procedural level ?? section and so 
    //procedural level seed
    //current playtimecounter
    //achievements
    //bosses discovered
    //enemies discovered
    

}