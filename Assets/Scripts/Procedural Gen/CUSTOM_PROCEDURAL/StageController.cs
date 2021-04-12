using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class StageController : MonoBehaviour
{
    // [Header("From first stage (1) to last stage (5) ")]
    [SerializeField] string seed;
    [SerializeField] int actualStage = 1;
    [SerializeField] int numberOfRooms;
    [SerializeField] int[] stages = new int[5];

    // [SerializeField] List<Room> stageRooms;

    [SerializeField] List<LevelGroup> sceneGroups = new List<LevelGroup>(); // 5 as length



    List<GameObject> instances = new List<GameObject>();

    private void Awake()
    {
        CreateThisRunSeed();
    }


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

        //LLENAR LOS SEED GROUPS
        FillUpSceneGropus(oneT, sceneGroups[0]);
        // FillUpSceneGropus(twoT, sceneGroups[1]);
        // FillUpSceneGropus(threeT, sceneGroups[2]);
        // FillUpSceneGropus(fourT, sceneGroups[3]);
        // FillUpSceneGropus(fiveT, sceneGroups[4]);

    }


    void Start()
    {
        numberOfRooms = SetRandomNumberOfRooms(actualStage); HandleLifeCycle();
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
    private void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.spaceKey.wasPressedThisFrame)
            HandleLifeCycle();
    }

    #region SCENES_LOADER_FROM_ADRESSEABLES_METHODS
    IEnumerator LoadAllAssetsByKey(int numberOf, LevelGroup currentGroup)
    {
        //Will load all objects that match the given key.
        //If this key is an Addressable label, it will load all assets marked with that label
        AsyncOperationHandle<IList<GameObject>> loadWithSingleKeyHandle = Addressables.LoadAssetsAsync<GameObject>("Stage" + (sceneGroups.IndexOf(currentGroup) + 1), asset =>
            {
                //Gets called for every loaded asset
                Debug.Log(asset.name);
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
            Debug.Log(stageAllScenesResult[i].name);
        }

    }
    private void FillUpSceneGropus(int numberOf, LevelGroup currentGroup)
    {
        StartCoroutine(LoadAllAssetsByKey(numberOf, currentGroup));
    }
    public void HandleLifeCycle()
    {
        bool hasSpawnedInstance = instances.Count > 0 ? true : false;
        // Debug.Log(hasSpawnedInstance);
        if (hasSpawnedInstance)
            Despawn();
        else
            Spawn();
    }
    private void Spawn()
    {
        // AsyncOperationHandle<GameObject> asyncOperationHandle = sceneGroups[actualStage - 1].LevelGroupScenes[0].InstantiateAsync(transform.position, Quaternion.identity, transform);
        // asyncOperationHandle.Completed += handle => instances.Add(handle.Result);

    }
    private void Despawn()
    {
        // foreach (GameObject instance in instances)
        // {
        // Addressables.ReleaseInstance(instance);
        // }
        // instances.Clear();
        // HandleLifeCycle();
    }
    #endregion
}


[System.Serializable]
public class LevelGroup
{
    public string levelName;
    public List<GameObject> LevelGroupScenes; //AssetReference is not supported by loadassetasync<assetreference>, fuck off Unity i dont wanna load Gameobjects

}