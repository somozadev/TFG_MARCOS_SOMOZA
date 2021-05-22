using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class StageController : MonoBehaviour
{
    [SerializeField] string seed;
    [SerializeField] int actualStage = 1;
    [SerializeField] int numberOfRooms;
    [SerializeField] int[] stages = new int[5];

    // [SerializeField] List<Room> stageRooms;

    [SerializeField] List<LevelGroup> sceneGroups = new List<LevelGroup>(); // 5 as length



    List<GameObject> instances = new List<GameObject>();

    private void Start()
    {
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

        GameManager.Instance.player.gameObject.SetActive(true);
        GameManager.Instance.mainCamera.transform.parent.gameObject.SetActive(true);
        GameManager.Instance.playerEventSystem.gameObject.SetActive(true);
        GameManager.Instance.defaultEventSystem.gameObject.SetActive(false);

    }

    public void LoadRun(string loadedSeed) { DataController.Instance.DeserializeSeed(loadedSeed); }

    public void LoadNextScene(int actualStage)
    {
        Instantiate(sceneGroups[1].LevelGroupScenes[0], transform);
        GameManager.Instance.player.transform.position = sceneGroups[1].LevelGroupScenes[0].GetComponent<Room>().playerStartPos.position;
    }



    private int SetRandomNumberOfRooms(int stage)
    {
        int returner = 0;
        switch (stage)
        {
            case 1:
                returner = Random.Range(5, 8);
                break;
            case 2:
                returner = Random.Range(9, 13);
                break;
            case 3:
                returner = Random.Range(14, 16);
                break;
            case 4:
                returner = Random.Range(17, 23);
                break;
            case 5:
                returner = Random.Range(24, 26);
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
        LoadNextScene(actualStage);

    }
    private void FillUpSceneGroups(int numberOf, LevelGroup currentGroup)
    {
        StartCoroutine(DataController.Instance.LoadAllAssetsByKey(numberOf, currentGroup, (sceneGroups.IndexOf(currentGroup) + 1)));
    }


    // public void HandleLifeCycle()
    // {
    //     bool hasSpawnedInstance = instances.Count > 0 ? true : false;
    //     // Debug.Log(hasSpawnedInstance);
    //     if (hasSpawnedInstance)
    //         Despawn();
    //     else
    //         Spawn();
    // }
    // private void Spawn()
    // {
    //     AsyncOperationHandle<GameObject> asyncOperationHandle = sceneGroups[actualStage - 1].LevelGroupScenes[0].InstantiateAsync(transform.position, Quaternion.identity, transform);
    //     asyncOperationHandle.Completed += handle => instances.Add(handle.Result);

    // }
    // private void Despawn()
    // {
    //     foreach (GameObject instance in instances)
    //     {
    //     Addressables.ReleaseInstance(instance);
    //     }
    //     instances.Clear();
    //     HandleLifeCycle();
    // }
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