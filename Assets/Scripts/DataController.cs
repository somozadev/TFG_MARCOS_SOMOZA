using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

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

    public List<int> ids;
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


    //SAVE THIS IDS AS WELL 
    public int GenerateId()
    {
        int id = 0;
        if (ids.Count <= 0)
            ids.Add(0);
        else
        {
            id = ids[ids.Count - 1] + 1;
            ids.Add(id);
        }

        return id;
    }
    public string SerializeSeed(int one, int two, int three, int four, int five, List<GameObject> oneList, List<GameObject> twoList, List<GameObject> threeList, List<GameObject> fourList, List<GameObject> fiveList)
    {

        int oneL = 0;
        foreach (GameObject g in oneList)
            if (oneL.ToString().Length == 1)
                oneL = int.Parse(g.GetComponent<Room>().GetId.ToString());
            else
                oneL = int.Parse(g.GetComponent<Room>().GetId.ToString() + oneL.ToString());
        int twoL = 0;
        foreach (GameObject g in twoList)
            if (twoL.ToString().Length == 1)
                twoL = int.Parse(g.GetComponent<Room>().GetId.ToString());
            else
                twoL = int.Parse(g.GetComponent<Room>().GetId.ToString() + twoL.ToString());
        int threeL = 0;
        foreach (GameObject g in threeList)
            if (threeL.ToString().Length == 1)
                threeL = int.Parse(g.GetComponent<Room>().GetId.ToString());
            else
                threeL = int.Parse(g.GetComponent<Room>().GetId.ToString() + threeL.ToString());
        int fourL = 0;
        foreach (GameObject g in fourList)
            if (fourL.ToString().Length == 1)
                fourL = int.Parse(g.GetComponent<Room>().GetId.ToString());
            else
                fourL = int.Parse(g.GetComponent<Room>().GetId.ToString() + fourL.ToString());
        int fiveL = 0;
        foreach (GameObject g in fiveList)
            if (fiveL.ToString().Length == 1)
                fiveL = int.Parse(g.GetComponent<Room>().GetId.ToString());
            else
                fiveL = int.Parse(g.GetComponent<Room>().GetId.ToString() + fiveL.ToString());

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

    private char ToChar(int inp) { return System.Convert.ToChar(inp + 65); }
    private int ToInt(char inp) { return System.Convert.ToInt16(inp - 65); }

    public void DeserializeSeed(string seed)
    {
        int oneC = ToInt(seed[0]);
        int twoC = ToInt(seed[1]);
        int threeC = ToInt(seed[2]);
        int fourC = ToInt(seed[3]);
        int fiveC = ToInt(seed[4]);
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

    IEnumerator WaitToFillGroup(int numberOf, LevelGroup currentGroup, int stageNumber, List<GameObject> floorGameobjects, List<int> ids)
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

    ///<sumary> 
    ///Will load all objects that match the given key.If this key is an Addressable label, it will load all assets marked with that label
    ///</sumary>
    public IEnumerator LoadAllAssetsByKey(int numberOf, LevelGroup currentGroup, int stageNumber)
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
