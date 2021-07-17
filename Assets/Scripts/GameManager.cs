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
    [Header("Main controllers")]
    [Space(2)]

    public DataController dataController;
    public SoundManager soundManager;
    public DeviceController deviceController;
    public SceneController sceneController;
    public SceneThemeMusicSelector sceneThemeMusicSelector;
    public StageController stageController;

    [Space(10)]
    [Header("Canvas controllers")]
    [Space(2)]

    public IngamePause ingameCanvasController;
    public StatsCanvasController statsCanvas;
    public OnDieCanvas  onDieCanvas;
    public DeviceChange deviceChanged;

    [Space(10)]
    [Header("Extra controllers")]
    [Space(2)]
    public XpController xpController;    
    [Space(10)]
    [Header("Refs")]
    [Space(2)]
    public GameObject mainCamera;
    public Player player;
    public EventSystem defaultEventSystem, playerEventSystem;
    [SerializeField] bool deleteGame;

    private void Start()
    {
        Physics.IgnoreLayerCollision(10, 11, true);
        Physics.IgnoreLayerCollision(11, 11, true);
        if (deleteGame) DataController.Instance.DeleteGame();
        print("Gamemanager started! ");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
