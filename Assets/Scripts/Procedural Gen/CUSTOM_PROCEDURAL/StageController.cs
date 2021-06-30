using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using CustomAttributes.MinMaxSlider;

public class StageController : MonoBehaviour
{
    public Room currentRoom;

    [Header("Numero de rooms por nivel")]
    [Space]
    [MinMaxSlider(5, 8)]
    [SerializeField] Vector2Int stage1Range;
    [MinMaxSlider(9, 13)]
    [SerializeField] Vector2Int stage2Range;
    [MinMaxSlider(14, 16)]
    [SerializeField] Vector2Int stage3Range;
    [MinMaxSlider(17, 23)]
    [SerializeField] Vector2Int stage4Range;
    [MinMaxSlider(24, 26)]
    [SerializeField] Vector2Int stage5Range;

    [Header("Semilla")]
    [Space]
    [SerializeField] string seed;

    [Header("Current room & level")]
    [Space]
    [SerializeField] int actualStage = 1;
    [SerializeField] int actualRoom = 1;
    [HideInInspector] [SerializeField] int numberOfRooms;
    
    [Header("Seed stages numbers")]
    [Space]
    [SerializeField] int[] stages = new int[5];

    [Header("Total seed groups")]
    [Space]
    [SerializeField] List<LevelGroup> sceneGroups = new List<LevelGroup>(); // 5 as length



    List<GameObject> instances = new List<GameObject>();

    private void Start()
    {
        SceneController.Instance.stageController = this;
        if (DataController.Instance.newRun)
        {
            numberOfRooms = SetRandomNumberOfRooms(actualStage);
            CreateThisRunSeed();
            // HandleLifeCycle();
        }
        else
        {
            seed = DataController.Instance.currentGameData.seed;
            LoadRun(seed);
        }

        GameManager.Instance.mainCamera.transform.parent.gameObject.SetActive(true);
        GameManager.Instance.defaultEventSystem.gameObject.SetActive(false);

    }

    public void LoadRun(string loadedSeed) { DataController.Instance.DeserializeSeed(loadedSeed); }

    public void LoadNextScene()
    {
        if (actualRoom >= stages[actualStage - 1])
        {
            actualRoom = 1;
            actualStage++;
        }
        if (actualStage > 5)
        {
            //FINISHED GAME
            print("finished run");
        }

        if (currentRoom != null)
        {
            GameManager.Instance.player.gameObject.SetActive(false);
            Destroy(currentRoom.gameObject);
        }
        Instantiate(sceneGroups[actualStage - 1].LevelGroupScenes[actualRoom - 1], transform);
        actualRoom++;
    }



    private int SetRandomNumberOfRooms(int stage)
    {
        int returner = 0;
        switch (stage)
        {
            case 1:
                returner = Random.Range(stage1Range.x, stage1Range.y);
                break;
            case 2:
                returner = Random.Range(stage2Range.x, stage2Range.y);
                break;
            case 3:
                returner = Random.Range(stage3Range.x, stage3Range.y);
                break;
            case 4:
                returner = Random.Range(stage4Range.x, stage4Range.y);
                break;
            case 5:
                returner = Random.Range(stage5Range.x, stage5Range.y);
                break;
        }
        return returner;
    }


    #region SCENES_LOADER_FROM_ADRESSEABLES_METHODS_&&_SEEDS
    public void CreateThisRunSeed()
    {
        //NUMERO DE SALAS POR NIVEL PARA CREAR LA SEED
        int oneT = SetRandomNumberOfRooms(1);
        stages[0] = oneT;
        int twoT = SetRandomNumberOfRooms(2);
        stages[1] = twoT;
        int threeT = SetRandomNumberOfRooms(3);
        stages[2] = threeT;
        int fourT = SetRandomNumberOfRooms(4);
        stages[3] = fourT;
        int fiveT = SetRandomNumberOfRooms(5);
        stages[4] = fiveT;
        StartCoroutine(Waiter(oneT, twoT, threeT, fourT, fiveT));
        //LLENAR LOS SEED GROUPS
    }
    private IEnumerator Waiter(int oneT, int twoT, int threeT, int fourT, int fiveT)
    {
        yield return StartCoroutine(FillUpAllSceneGroups(oneT, twoT, threeT, fourT, fiveT));
        DoSeed(oneT, twoT, threeT, fourT, fiveT);
    }
    private IEnumerator FillUpAllSceneGroups(int oneT, int twoT, int threeT, int fourT, int fiveT)
    {
        FillUpSceneGroups(oneT, sceneGroups[0]);
        FillUpSceneGroups(twoT, sceneGroups[1]);
        FillUpSceneGroups(threeT, sceneGroups[2]);
        FillUpSceneGroups(fourT, sceneGroups[3]);
        FillUpSceneGroups(fiveT, sceneGroups[4]);
        yield return new WaitForSeconds(.4f);

    }
    private void DoSeed(int oneT, int twoT, int threeT, int fourT, int fiveT)
    {
        seed = DataController.Instance.SerializeSeed(oneT, twoT, threeT, fourT, fiveT,
        sceneGroups[0].LevelGroupScenes, sceneGroups[1].LevelGroupScenes, sceneGroups[2].LevelGroupScenes,
        sceneGroups[3].LevelGroupScenes, sceneGroups[4].LevelGroupScenes);
        LoadNextScene();

    }
    private void FillUpSceneGroups(int numberOf, LevelGroup currentGroup) //aquí es donde aparecería lo "procedural" en el stageNumber....
    {
        Debug.Log(currentGroup.levelName + ":" + sceneGroups.IndexOf(currentGroup) + 1);
        StartCoroutine(DataController.Instance.LoadAllAssetsByKey(numberOf, currentGroup, (sceneGroups.IndexOf(currentGroup) + 1)));
    }



    #endregion
}


[System.Serializable]
public class LevelGroup
{
    public string levelName;
    public List<GameObject> LevelGroupScenes; //AssetReference is not supported by loadassetasync<assetreference>, fuck off Unity i dont wanna load Gameobjects

    public LevelGroup()
    {
        LevelGroupScenes = new List<GameObject>();
    }
}