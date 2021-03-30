using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class StageController : MonoBehaviour
{
    [Header("From first stage (1) to last stage (5) ")]
    [SerializeField] int actualStage = 1;
    [SerializeField] int numberOfRooms;

    [SerializeField] List<Room> stageRooms;

    [SerializeField] SceneGroup[] sceneGroups; // 5 as length

    void Start()
    {
        SetRandomNumberOfRooms(actualStage);
    }
    private void SetRandomNumberOfRooms(int stage)
    {
        switch (stage)
        {
            case 1:
                numberOfRooms = Random.Range(5, 8);
                break;
            case 2:
                numberOfRooms = Random.Range(9, 13);
                break;
            case 3:
                numberOfRooms = Random.Range(14, 16);
                break;
            case 4:
                numberOfRooms = Random.Range(17, 23);
                break;
            case 5:
                numberOfRooms = Random.Range(24, 26);
                break;
        }
    }

    // Update is called once per frame
    void test()
    {
        // SceneManager.LoadScene(sceneGroups[0].GetScenes[0]);
    }
}


[System.Serializable]
public class SceneGroup
{
    public List<AssetReference> scenes;

    public List<AssetReference> GetScenes { get { return scenes; } }


    public SceneGroup(List<AssetReference> scenes)
    {
        this.scenes = scenes;
    }

}