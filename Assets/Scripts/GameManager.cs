using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    [SerializeField] bool deleteGame;
    public DeviceController deviceController;
    public StatsCanvasController statsCanvas;
    public GameObject mainCamera;
    public Player player;
    public EventSystem defaultEventSystem,playerEventSystem;
    public StageController stageController;
    public XpController xpController;
    public IngamePause ingamePause;

    private void Start()
    {
        Physics.IgnoreLayerCollision(10, 11, true);
        if(deleteGame) DataController.Instance.DeleteGame();
        print("Gamemanager started! ");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
