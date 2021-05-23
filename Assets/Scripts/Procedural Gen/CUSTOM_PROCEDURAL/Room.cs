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
    [SerializeField] bool hasShop;
    [SerializeField] bool isCompleted;
    [SerializeField] bool isDroppingItem;

    [SerializeField] Room backtrackingRoom;

    public int SetId { set { RoomId = value; } }
    public int GetId { get { return RoomId; } }


    private void Awake()
    {
        ResetEvent();
    }


    private void Update() {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Complete();
        }
    }


    private void OnEnable()
    {   
        if(GetComponentInParent<StageController>() != null)
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
    }

    public void ResetEvent() => onRoomCompleted = null;

    public void Complete()
    {
        isCompleted = true;
        if (onRoomCompleted != null)
            onRoomCompleted();
    }
}
