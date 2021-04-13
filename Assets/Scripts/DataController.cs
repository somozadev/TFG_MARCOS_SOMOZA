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

    public List<int> ids;

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

        string oneL = ToInt(seed[5]).ToString();
        List<int> idsOne = new List<int>();
        for (int i = 0; i < oneL.Length - 1; i++)
            idsOne.Add(oneL[i]);

        /*
        * TODO: COGER LA LISTA idsOne y buscar todos los gameobjects de addresseable(sabemos que son de label Stage1 en este caso)
        * que coincidan con los ids y guardarnos una lista ordenada igual que los ids con dichos gameobjects (que serian las escenas)
        */
        string twoL = ToInt(seed[6]).ToString();
        List<int> idsTwo = new List<int>();
        for (int i = 0; i < twoL.Length - 1; i++)
            idsTwo.Add(twoL[i]);

        string threeL = ToInt(seed[7]).ToString();
        List<int> idsThree = new List<int>();
        for (int i = 0; i < threeL.Length - 1; i++)
            idsThree.Add(threeL[i]);

        string fourL = ToInt(seed[8]).ToString();
        List<int> idsFour = new List<int>();
        for (int i = 0; i < fourL.Length - 1; i++)
            idsFour.Add(fourL[i]);

        string fiveL = ToInt(seed[9]).ToString();
        List<int> idsFive = new List<int>();
        for (int i = 0; i < fiveL.Length - 1; i++)
            idsFive.Add(fiveL[i]);




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
}
