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
    [Space(10)]
    [Header("Controllers & Managers")]
    [Space(2)]

    public DataController dataController;
    public SoundManager soundManager;
    public DeviceController deviceController;
    public SceneController sceneController;  
    public IngamePause ingameCanvasController;
    [Space(10)]
    [Header("Extras")]
    [Space(10)]
    public StatsCanvasController statsCanvas;
    public StageController stageController;
    public GameObject mainCamera;
    public Player player;
    [SerializeField] bool deleteGame;
    public EventSystem defaultEventSystem,playerEventSystem;
    public XpController xpController;
    public DeviceChange deviceChanged;

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
