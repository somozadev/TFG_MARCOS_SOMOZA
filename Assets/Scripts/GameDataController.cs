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
                saveFiles[i].GetComponent<Button>().Select();
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
            GameManager.Instance.sceneThemeMusicSelector.SetScene = SCENES.MenuScene;
            GameManager.Instance.sceneThemeMusicSelector.CheckTheme();
            DataController.Instance.currentGameData = loadedGame;
            // SceneController.Instance.LoadScene(SceneName.MenuScene);
            // SceneController.Instance.LoadAdresseableScene(SceneName.MenuScene,true);
        }
    }

}

[System.Serializable]
public class GameData
{
    public string seed;
    public float gameCompletePercentaje;  //!falta hacer este sistema
    public PlayerStats playerStats;       // se actualiza si hay una current run en proceso, donde guarda las stats actuales. 
    public List<bool> itemsUnlocked; //!falta hacer este sistema
    public DataController.StupidButCoolStats coolStats; //ya se actualiza, en DataControler.instance.UpdateStupidButCoolGameStats(). llamada on death event player.


    public GameData() {  }//seed = PlayerPrefs.GetString("seed","null");
    public GameData(string seed, PlayerStats playerStats, float gameCompletePercentaje, List<bool> itemsUnlocked)
    {
        this.seed = seed;
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