using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GameDataController : MonoBehaviour
{

    [SerializeField] GameData loadedGame;
    [SerializeField] int ereaseIndex;
    [SerializeField] GameObject[] saveFiles;
    [SerializeField] Sprite noFile, yesFile, selected;
    [SerializeField] GameObject uSurePanel;
    [SerializeField] bool loaded;

    void Start()
    {
        ereaseIndex = -1;
        GetVisualsGameData();
        SelectFromStart();
    }
    // Selects first saveFile 
    private void SelectFromStart()
    {
        for (int i = 0; i < 3; i++)
        {
            if (saveFiles[i].GetComponent<Image>().sprite == yesFile)
            {
                saveFiles[i].GetComponent<Image>().sprite = selected;
                GameManager.Instance.defaultEventSystem.firstSelectedGameObject = saveFiles[i];
                ereaseIndex = i;
                break;
            }
        }
    }
    // Updates UI visuals.
    private void GetVisualsGameData()
    {
        for (int i = 0; i < 3; i++)
        {
            if (!CheckGameDataFile(i))
            {
                print("NoFile" + i);
                saveFiles[i].GetComponent<Image>().sprite = noFile;
            }
            else
            {
                print("YesFile" + i);
                if (saveFiles[i].GetComponent<Image>().sprite != selected)
                    saveFiles[i].GetComponent<Image>().sprite = yesFile;
            }
        }
    }
    // Saves an specific savefile.
    public void SetGameData(int id, GameData gameData)
    {
        string path = Application.persistentDataPath + "/savefile_" + id + ".data";
        File.WriteAllText(path, JsonUtility.ToJson(gameData));
    }
    // Checks if a specific savefile exists. 
    private bool CheckGameDataFile(int id)
    {
        bool checker = false;
        string path = Application.persistentDataPath + "/savefile_" + id + ".data";
        if (File.Exists(path))
            checker = true;
        return checker;
    }
    //  Returns savefile saved in localpath. If there is none, creates one.
    public void GetGameData(int id)
    {
        loaded = true;
        for (int i = 0; i < 3; i++)
        {
            if (i == id)
                saveFiles[i].GetComponent<Image>().sprite = selected;
            else if (!CheckGameDataFile(i))
                saveFiles[i].GetComponent<Image>().sprite = noFile;
            else
                saveFiles[i].GetComponent<Image>().sprite = yesFile;
        }
        GetVisualsGameData();
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
    }
    // Ereases the current selected gamefile. 
    public void Erease()
    {
        string path = Application.persistentDataPath + "/savefile_" + ereaseIndex + ".data";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        Cancel();
        GetVisualsGameData();

    }
    // Cancels are you sure erease panel.
    public void Cancel() { uSurePanel.SetActive(false); }
    // Opens the are you sure erease panel.
    public void EreaseGameData() { if (ereaseIndex != -1) uSurePanel.SetActive(true); }
    // Passes to the datacontroller selected gameData and loads next menu, unloading current.
    public void LoadSaveFile()
    {
        if (loaded)
        {
            DataController.Instance.currentGameData = loadedGame;
            SceneController.Instance.LoadAdresseableScene(SceneName.MenuScene, true);
        }
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