using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Room : MonoBehaviour
{
    [SerializeField] int RoomId;
    public Vector3 playerStartPos;


    public event Action onRoomCompleted;



    [SerializeField] List<GameObject> enemiesList;
    [SerializeField] bool[] enemiesListDied;
    [SerializeField] bool hasShop;
    [SerializeField] bool isCompleted;
    [SerializeField] bool isDroppingItem;

    [SerializeField] bool isStartingRoom;
    [SerializeField] bool isBossRoom;

    [SerializeField] Room backtrackingRoom;

    public int SetId { set { RoomId = value; } }
    public int GetId { get { return RoomId; } }
    public bool SetHasShop { set { hasShop = value; } }
    public bool SetHasDrop { set { isDroppingItem = value; } }
    public bool SetisStartingRoom { set { isStartingRoom = value; } }
    public bool SetIsBossRoom { set { isBossRoom = value; } }
    public List<GameObject> SetEnemiesList { set { enemiesList = value; } }
    public bool[] SetEnemiesListDied { set { enemiesListDied = value; } }

    private void Awake()
    {
        ResetEvent();
    }




    public void EnemyDiedCall(GameObject enemy) { if (EnemyDied(enemy)) Complete(); }
    private bool EnemyDied(GameObject enemy)
    {
        bool shallComplete = true;
        int index = enemiesList.IndexOf(enemy);
        enemiesListDied[index] = true;
        foreach (bool enemyCond in enemiesListDied)
        {
            if (!enemyCond)
                shallComplete = false;
        }
        return shallComplete;
    }
    private void OnEnable()
    {
        if (GetComponentInParent<StageController>() != null)
            GetComponentInParent<StageController>().currentRoom = this;

        playerStartPos = GetComponent<EditorTool.PrefabCreatorCleaner>().playerPos.position;
        GetComponent<EditorTool.PrefabCreatorCleaner>().playerPos.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        if (GetComponent<EditorTool.PrefabCreatorCleaner>() != null)
            Destroy(this.GetComponent<EditorTool.PrefabCreatorCleaner>());

    }

    private void Start()
    {
        GameManager.Instance.playerEventSystem.gameObject.SetActive(true);

        GameManager.Instance.player.gameObject.transform.position = new Vector3(playerStartPos.x, GameManager.Instance.player.gameObject.transform.position.y, playerStartPos.z);

        GameManager.Instance.player.gameObject.SetActive(true);

        if (isStartingRoom)
            Complete();
    }

    public void ResetEvent() => onRoomCompleted = null;

    public void Complete()
    {
        isCompleted = true;
        if (onRoomCompleted != null)
            onRoomCompleted();
    }
}
