using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

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
        DontDestroyOnLoad(gameObject);
    }
    #endregion

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

    private char ToChar(long inp) { return System.Convert.ToChar(inp + 65); }
    private long ToInt(char inp) { return System.Convert.ToInt64(inp - 65); }
    public string SerializeSeed(int one, int two, int three, int four, int five, List<GameObject> oneList, List<GameObject> twoList, List<GameObject> threeList, List<GameObject> fourList, List<GameObject> fiveList)
    {
        //System.Numerics.BigInteger igual? 
        long oneL = -1;
        foreach (GameObject g in oneList)
        {
            if (oneL == -1)
                oneL = long.Parse(g.GetComponent<Room>().GetId.ToString());
            else
                oneL = long.Parse(oneL.ToString() + g.GetComponent<Room>().GetId.ToString());
        }
        long twoL = -1;
        foreach (GameObject g in twoList)
        {
            if (twoL == -1)
                twoL = long.Parse(g.GetComponent<Room>().GetId.ToString());
            else
                twoL = long.Parse(twoL.ToString() + g.GetComponent<Room>().GetId.ToString());
        }
        long threeL = -1;
        foreach (GameObject g in threeList)
        {
            if (threeL == -1)
                threeL = long.Parse(g.GetComponent<Room>().GetId.ToString());
            else
                threeL = long.Parse(threeL.ToString() + g.GetComponent<Room>().GetId.ToString());
        }
        long fourL = -1;
        foreach (GameObject g in fourList)
        {
            if (fourL == -1)
                fourL = long.Parse(g.GetComponent<Room>().GetId.ToString());
            else
                fourL = long.Parse(fourL.ToString() + g.GetComponent<Room>().GetId.ToString());
        }
        long fiveL = -1;
        foreach (GameObject g in fiveList)
        {
            if (fiveL == -1)
                fiveL = long.Parse(g.GetComponent<Room>().GetId.ToString());
            else
                fiveL = long.Parse(fiveL.ToString() + g.GetComponent<Room>().GetId.ToString());
        }

        char oneC = ToChar(one);
        char twoC = ToChar(two);
        char threeC = ToChar(three);
        char fourC = ToChar(four);
        char fiveC = ToChar(five);

        char oneListC = ToChar(oneL);
        char twoListC = ToChar(twoL);
        char threeListC = ToChar(threeL);
        char fourListC = ToChar(fourL);
        char fiveListC = ToChar(fiveL);


        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(oneC); sb.Append(twoC); sb.Append(threeC); sb.Append(fourC); sb.Append(fiveC);
        sb.Append(oneListC); sb.Append(twoListC); sb.Append(threeListC); sb.Append(fourListC); sb.Append(fiveListC);

        string seed = sb.ToString();
        Debug.Log("seed: " + seed);
        return seed;

    }
    public void DeserializeSeed(string seed)
    {
        Debug.Log(seed[0]);
        long oneC = ToInt(seed[0]);
        long twoC = ToInt(seed[1]);
        long threeC = ToInt(seed[2]);
        long fourC = ToInt(seed[3]);
        long fiveC = ToInt(seed[4]);
        #region  FloorOne
        string oneL = ToInt(seed[5]).ToString();
        List<int> idsOne = new List<int>();
        for (int i = 0; i < oneL.Length; i++)
            idsOne.Add(int.Parse(oneL[i].ToString()));

        LevelGroup groupLabelOne = new LevelGroup();
        scenesFloorOne = new List<GameObject>();
        StartCoroutine(WaitToFillGroup(oneC, groupLabelOne, 1, scenesFloorOne, idsOne));
        #endregion
        #region  FloorTwo
        string twoL = ToInt(seed[6]).ToString();
        List<int> idsTwo = new List<int>();
        for (int i = 0; i < twoL.Length; i++)
            idsTwo.Add(int.Parse(twoL[i].ToString()));


        LevelGroup groupLabelTwo = new LevelGroup();
        scenesFloorTwo = new List<GameObject>();
        StartCoroutine(WaitToFillGroup(twoC, groupLabelTwo, 2, scenesFloorTwo, idsTwo));
        #endregion
        #region  FloorThree
        string threeL = ToInt(seed[7]).ToString();
        List<int> idsThree = new List<int>();
        for (int i = 0; i < threeL.Length; i++)
            idsThree.Add(int.Parse(threeL[i].ToString()));


        LevelGroup groupLabelThree = new LevelGroup();
        scenesFloorThree = new List<GameObject>();
        StartCoroutine(WaitToFillGroup(threeC, groupLabelThree, 3, scenesFloorThree, idsThree));
        #endregion
        #region  FloorFour
        string fourL = ToInt(seed[8]).ToString();
        List<int> idsFour = new List<int>();
        for (int i = 0; i < fourL.Length; i++)
            idsFour.Add(int.Parse(fourL[i].ToString()));


        LevelGroup groupLabelFour = new LevelGroup();
        scenesFloorFour = new List<GameObject>();
        StartCoroutine(WaitToFillGroup(fourC, groupLabelFour, 4, scenesFloorFour, idsFour));
        #endregion
        #region  FloorTwo
        string fiveL = ToInt(seed[9]).ToString();
        List<int> idsFive = new List<int>();
        for (int i = 0; i < fiveL.Length; i++)
            idsFive.Add(int.Parse(fiveL[i].ToString()));


        LevelGroup groupLabelFive = new LevelGroup();
        scenesFloorFive = new List<GameObject>();
        StartCoroutine(WaitToFillGroup(fiveC, groupLabelFive, 5, scenesFloorFive, idsFive));
        #endregion

    }

    IEnumerator WaitToFillGroup(long numberOf, LevelGroup currentGroup, int stageNumber, List<GameObject> floorGameobjects, List<int> ids)
    {
        yield return StartCoroutine(LoadAllAssetsByKey(numberOf, currentGroup, stageNumber));
        DoGameobjectsFillUp(currentGroup, floorGameobjects, ids);
        Debug.Log(currentGroup.LevelGroupScenes[0]);

    }
    private void DoGameobjectsFillUp(LevelGroup currentGroup, List<GameObject> floorGameobjects, List<int> ids)
    {
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
        GameManager.Instance.player.playerStats = new PlayerStats(0, 100, 0, 100, 50, 1, 5, 1, 1, 1, 7, 0, new List<Item>());
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
        AsyncOperationHandle<IList<GameObject>> loadWithSingleKeyHandle = Addressables.LoadAssetsAsync<GameObject>("Stage" + stageNumber, asset =>
           {     //Gets called for every loaded asset
           });
        yield return loadWithSingleKeyHandle;
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