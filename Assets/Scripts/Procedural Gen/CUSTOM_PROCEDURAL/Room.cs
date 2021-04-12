using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Room : MonoBehaviour
{
    public Transform playerStartPos { get; private set; }
    public int numberOfRooms { get; private set; }
    [SerializeField] List<GameObject> enemiesList;
    [SerializeField] bool hasShop;
    [SerializeField] bool isCompleted;
    [SerializeField] bool isDroppingItem;

    [SerializeField] Room backtrackingRoom;


    private void OnEnable()
    {
        if (GetComponent<EditorTool.PrefabCreatorCleaner>() != null)
            Destroy(this.GetComponent<EditorTool.PrefabCreatorCleaner>());

        if (isCompleted)
            return;
        else
            Initialize();
    }


    private void Initialize()
    {

    }
}
