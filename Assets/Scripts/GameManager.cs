using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public StatsCanvasController statsCanvas;
    public GameObject mainCamera;
    public Player player;

    private void Start()
    {
        if(deleteGame) DataController.Instance.DeleteGame();
    }

}
