using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

[System.Serializable]
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


        stupidButCoolStats = new StupidButCoolStats(GetTotalRuns(), GetTotalDeaths(), GetTotalEnemiesKilled());

    }
    #endregion

    [Header("User game cool stats")]
    [Space(2)]
    public StupidButCoolStats stupidButCoolStats;

    [Space(20)]
    public bool newRun;
    public GameData currentGameData;
    public Ids ids;
    public List<GameObject> scenesFloorOne;
    public List<GameObject> scenesFloorTwo;
    public List<GameObject> scenesFloorThree;
    public List<GameObject> scenesFloorFour;
    public List<GameObject> scenesFloorFive;

    public void SetPlayerStats(PlayerStats playerStats) { PlayerPrefs.SetString("PlayerStats", JsonUtility.ToJson(playerStats)); }
    public void GetPlayerStats(PlayerStats playerStats)
    {
        PlayerStats stats = new PlayerStats(0, 100, 0, 100, 50, 1, 5, 1, 1, 1, 7, 0, new List<Item>());
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("PlayerStats", JsonUtility.ToJson(stats)), playerStats);
    }


    //mejor seria cambiarlo a una clase de stupid stats en json pero bue por ahora ta bien
    #region STUPID_BUT_COOL_GAME_STATS

    private int GetTotalRuns() { return PlayerPrefs.GetInt("totalRuns", 0); }
    public void AddAnotherRun() { int runs = GetTotalRuns(); PlayerPrefs.SetInt("totalRuns", runs+1); UpdateStupidButCoolGameStats(); }
    private int GetTotalDeaths() { return PlayerPrefs.GetInt("totalDeaths", 0); }
    public void AddAnotherDeath() { int deaths = GetTotalDeaths(); PlayerPrefs.SetInt("totalDeaths", deaths+1); UpdateStupidButCoolGameStats(); }
    private int GetTotalEnemiesKilled() { return PlayerPrefs.GetInt("totalEnemiesKilled", 0); }
    public void AddAnotherEnemiesKilled() { int killed = GetTotalEnemiesKilled(); PlayerPrefs.SetInt("totalEnemiesKilled", killed+1); UpdateStupidButCoolGameStats(); }
    public void UpdateStupidButCoolGameStats()
    {
        stupidButCoolStats.runs = GetTotalRuns();
        stupidButCoolStats.deaths = GetTotalDeaths();
        stupidButCoolStats.enemiesKilled = GetTotalEnemiesKilled();
    }
    #endregion


    #region SCENES_SEED_SAVE_LOAD
    public void SetIds(Ids idsList) { PlayerPrefs.SetString("SceneIds", JsonUtility.ToJson(idsList)); Debug.Log(JsonUtility.ToJson(idsList)); }
    public void GetIds(Ids idsList) { JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("SceneIds", JsonUtility.ToJson(new Ids(0))), idsList); }
    public int GenerateId()
    {
        int id = 0;
        if (ids.id.Count <= 0)
            ids.id.Add(0);
        else
        {
            id = ids.id[ids.id.Count - 1] + 1;
            ids.id.Add(id);
        }
        SetIds(ids);
        return id;
    }

    public string SerializeSeed(int one, int two, int three, int four, int five, List<GameObject> oneList, List<GameObject> twoList, List<GameObject> threeList, List<GameObject> fourList, List<GameObject> fiveList)
    {
        string oneL = "-1";
        foreach (GameObject g in oneList)
        {
            if (oneL == "-1")
                oneL = g.GetComponent<Room>().GetId.ToString("00");
            else
                oneL += g.GetComponent<Room>().GetId.ToString("00");
        }
        string twoL = "-1";
        foreach (GameObject g in twoList)
        {
            if (twoL == "-1")
                twoL = g.GetComponent<Room>().GetId.ToString("00");
            else
                twoL += g.GetComponent<Room>().GetId.ToString("00");
        }
        string threeL = "-1";
        foreach (GameObject g in threeList)
        {
            if (threeL == "-1")
                threeL = g.GetComponent<Room>().GetId.ToString("00");
            else
                threeL += g.GetComponent<Room>().GetId.ToString("00");
        }
        string fourL = "-1";
        foreach (GameObject g in fourList)
        {
            if (fourL == "-1")
                fourL = g.GetComponent<Room>().GetId.ToString("00");
            else
                fourL += g.GetComponent<Room>().GetId.ToString("00");
        }
        string fiveL = "-1";
        foreach (GameObject g in fiveList)
        {
            if (fiveL == "-1")
                fiveL = g.GetComponent<Room>().GetId.ToString("00");
            else
                fiveL += g.GetComponent<Room>().GetId.ToString("00");
        }
        if (one > oneList.Count)
            one = oneList.Count;
        if (two > twoList.Count)
            two = twoList.Count;
        if (three > threeList.Count)
            three = threeList.Count;
        if (four > fourList.Count)
            four = fourList.Count;
        if (five > fiveList.Count)
            five = fiveList.Count;
        string first = (one.ToString("00") + two.ToString("00") + three.ToString("00") + four.ToString("00") + five.ToString("00"));
        string second = (oneL + twoL + threeL + fourL + fiveL);

        print("seed:" + first + second);

        return (first + second);

    }
    public void DeserializeSeed(string seed)
    {
        string s1 = seed.Substring(0, 2);
        string s2 = seed.Substring(2, 2);
        string s3 = seed.Substring(4, 2);
        string s4 = seed.Substring(6, 2);
        string s5 = seed.Substring(8, 2);

        seed = seed.Substring(10);
        string lvl1 = seed.Substring(0, System.Convert.ToInt16(s1) * 2);
        seed = seed.Substring(System.Convert.ToInt16(s1) * 2);
        string lvl2 = seed.Substring(0, System.Convert.ToInt16(s2) * 2);
        seed = seed.Substring(System.Convert.ToInt16(s2) * 2);
        string lvl3 = seed.Substring(0, System.Convert.ToInt16(s3) * 2);
        seed = seed.Substring(System.Convert.ToInt16(s3) * 2);
        string lvl4 = seed.Substring(0, System.Convert.ToInt16(s4) * 2);
        seed = seed.Substring(System.Convert.ToInt16(s4) * 2);
        string lvl5 = seed.Substring(0, System.Convert.ToInt16(s5) * 2);

        print(lvl1);
        print(lvl2);
        print(lvl3);
        print(lvl4);
        print(lvl5);

        List<int> lvl1_int = new List<int>();
        int internalCounter = 1;
        int len = lvl1.Length;
        for (int i = 0; i < len; i++)
        {
            if (internalCounter == 2)
            {
                lvl1_int.Add(int.Parse(lvl1.Substring(0, internalCounter)));
                lvl1 = lvl1.Substring(internalCounter);
                internalCounter = 0;
            }
            internalCounter++;
        }
        LevelGroup groupLabelOne = new LevelGroup();
        scenesFloorOne = new List<GameObject>();
        StartCoroutine(WaitToFillGroup(long.Parse(s1), groupLabelOne, 1, scenesFloorOne, lvl1_int));
        print("lvl1int: " + lvl1_int);

        List<int> lvl2_int = new List<int>();
        internalCounter = 1;
        len = lvl2.Length;
        for (int i = 0; i < len; i++)
        {
            if (internalCounter == 2)
            {
                lvl2_int.Add(int.Parse(lvl2.Substring(0, internalCounter)));
                lvl2 = lvl2.Substring(internalCounter);
                internalCounter = 0;
            }
            internalCounter++;
        }
        LevelGroup groupLabelTwo = new LevelGroup();
        scenesFloorTwo = new List<GameObject>();
        StartCoroutine(WaitToFillGroup(long.Parse(s2), groupLabelTwo, 2, scenesFloorTwo, lvl2_int));
        print("lvl2int: " + lvl2_int);

        List<int> lvl3_int = new List<int>();
        internalCounter = 1;
        len = lvl3.Length;
        for (int i = 0; i < len; i++)
        {
            if (internalCounter == 2)
            {
                lvl3_int.Add(int.Parse(lvl3.Substring(0, internalCounter)));
                lvl3 = lvl3.Substring(internalCounter);
                internalCounter = 0;
            }
            internalCounter++;
        }
        LevelGroup groupLabelThree = new LevelGroup();
        scenesFloorThree = new List<GameObject>();
        StartCoroutine(WaitToFillGroup(long.Parse(s3), groupLabelThree, 3, scenesFloorThree, lvl3_int));
        print("lvl3int: " + lvl3_int);

        List<int> lvl4_int = new List<int>();
        internalCounter = 1;
        len = lvl4.Length;
        for (int i = 0; i < len; i++)
        {
            if (internalCounter == 2)
            {
                lvl4_int.Add(int.Parse(lvl4.Substring(0, internalCounter)));
                lvl4 = lvl4.Substring(internalCounter);
                internalCounter = 0;
            }
            internalCounter++;
        }
        LevelGroup groupLabelFour = new LevelGroup();
        scenesFloorFour = new List<GameObject>();
        StartCoroutine(WaitToFillGroup(long.Parse(s4), groupLabelFour, 4, scenesFloorFour, lvl4_int));
        print("lvl4int: " + lvl4_int);

        List<int> lvl5_int = new List<int>();
        internalCounter = 1;
        len = lvl5.Length;
        for (int i = 0; i < len; i++)
        {
            if (internalCounter == 2)
            {
                lvl5_int.Add(int.Parse(lvl5.Substring(0, internalCounter)));
                lvl5 = lvl5.Substring(internalCounter);
                internalCounter = 0;
            }
            internalCounter++;
        }
        LevelGroup groupLabelFive = new LevelGroup();
        scenesFloorFive = new List<GameObject>();
        StartCoroutine(WaitToFillGroup(long.Parse(s5), groupLabelFive, 5, scenesFloorFive, lvl5_int));
        print("lvl5int: " + lvl5_int);


    }

    IEnumerator WaitToFillGroup(long numberOf, LevelGroup currentGroup, int stageNumber, List<GameObject> floorGameobjects, List<int> ids)
    {
        yield return StartCoroutine(LoadAllAssetsByKey(numberOf, currentGroup, stageNumber));
        DoGameobjectsFillUp(currentGroup, floorGameobjects, ids);
        Debug.Log(currentGroup.LevelGroupScenes[0]);

    }
    private void DoGameobjectsFillUp(LevelGroup currentGroup, List<GameObject> floorGameobjects, List<int> ids)
    {

        foreach (int id in ids)
            Debug.LogWarning("ids:" + id);
        Debug.LogWarning(currentGroup.levelName);
        foreach (GameObject ob in currentGroup.LevelGroupScenes)
            Debug.LogWarning("currentGroup.LevelGroupScenes:" + ob);

        for (int i = 0; i < currentGroup.LevelGroupScenes.Count; i++)
        {
            for (int j = 0; j < ids.Count; j++)
            {
                if (currentGroup.LevelGroupScenes[i].GetComponent<Room>().GetId == ids[j])
                    floorGameobjects.Add(currentGroup.LevelGroupScenes[i]);
            }
        }

    }
    #endregion

    public void LoadGame()
    {

    }
    private void SaveGame()
    {

    }
    public void DeleteGame()
    {
        PlayerPrefs.DeleteAll();
        GameManager.Instance.player.playerStats = new PlayerStats(0, 100, 0, 100, 50, 4, 5, 1, 1, 2, 7, 0, new List<Item>());
        GameManager.Instance.player.extraStats.ElectricShots = true;
    }
    private void Start()
    {
        GetIds(ids);
        LoadGame();
    }
    private void OnApplicationQuit()
    {
        SetIds(ids);
        SaveGame();
    }
    ///<sumary> 
    ///Will load all objects that match the given key.If this key is an Addressable label, it will load all assets marked with that label
    ///</sumary>
    public IEnumerator LoadAllAssetsByKey(long numberOf, LevelGroup currentGroup, int stageNumber)
    {
        AsyncOperationHandle<IList<GameObject>> loadFirsts = Addressables.LoadAssetsAsync<GameObject>("StartRoom", asset => { });
        AsyncOperationHandle<IList<GameObject>> loadBosses = Addressables.LoadAssetsAsync<GameObject>("BossRoom", asset => { });
        AsyncOperationHandle<IList<GameObject>> loadWithSingleKeyHandle = Addressables.LoadAssetsAsync<GameObject>("Stage" + stageNumber, asset => { });
        yield return loadFirsts;
        yield return loadBosses;
        yield return loadWithSingleKeyHandle;
        IList<GameObject> stageFirstsResult = loadFirsts.Result;
        IList<GameObject> stageBossesResult = loadBosses.Result;
        IList<GameObject> stageAllScenesResult = loadWithSingleKeyHandle.Result;
        if (numberOf > stageAllScenesResult.Count)
            numberOf = stageAllScenesResult.Count;
        for (int i = 0; i < numberOf; i++)
        {

            if (currentGroup.LevelGroupScenes.Contains(stageAllScenesResult[i]))
                Debug.Log("ALREADY IN");
            else
                currentGroup.LevelGroupScenes.Add(stageAllScenesResult[i]);
        }
        GameObject firstScene, bossScene;
        foreach (GameObject first in stageFirstsResult)
        {
            if (currentGroup.LevelGroupScenes.Contains(first))
            {
                firstScene = first;
                currentGroup.LevelGroupScenes.Remove(first);
                currentGroup.LevelGroupScenes.Insert(0, firstScene);
                firstScene.GetComponent<Room>().SetisStartingRoom = true;
                break;
            }
        }
        foreach (GameObject boss in stageBossesResult)
        {
            if (currentGroup.LevelGroupScenes.Contains(boss))
            {
                bossScene = boss;
                currentGroup.LevelGroupScenes.Remove(boss);
                currentGroup.LevelGroupScenes.Add(bossScene);
                bossScene.GetComponent<Room>().SetIsBossRoom = true;
                break;
            }
        }
    }



}
[System.Serializable]
public class StupidButCoolStats
{

    public int runs;
    public int deaths;
    public int enemiesKilled;
    public int itemsUnlocked;

    public StupidButCoolStats(int runs, int deaths, int enemiesKilled)
    {
        this.runs = runs;
        this.deaths = deaths;
        this.enemiesKilled = enemiesKilled;
    }


}
[System.Serializable]
public class Ids
{
    public List<int> id = new List<int>();
    public Ids(int id)
    {
        this.id.Add(id);
    }
}