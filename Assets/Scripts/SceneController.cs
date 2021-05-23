using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneController : MonoBehaviour
{
    #region Singleton
    private static SceneController instance;
    public static SceneController Instance { get { return instance; } }
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    
    [SerializeField] private AsyncOperationHandle<SceneInstance> handle; //last adresseable scene loaded
    public StageController stageController;

    private void Start()
    {
        LoadAdresseableScene(SceneName.MainScene, false);
    }

    public void LoadScene(string sceneName) { StartCoroutine(LoadAsync(sceneName)); }
    public void LoadAdresseableScene(string sceneName, bool unloadLastScene){ if(unloadLastScene) Addressables.UnloadSceneAsync(handle, true); 
                            Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive, true).Completed += AdresseableSceneLoadComplete; }
    public void LoadSceneAssetReference(AssetReference scene) { StartCoroutine(LoadSceneAssetReferenceAsync(scene)); }


    public void LoadNextFloor()
    {
        stageController.LoadNextScene();
        Debug.Log("Next Room");
    }



    private IEnumerator LoadAsync(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone)
        {
            //pantalla de carga etc etc 
            yield return null;
        }
    }
    private void AdresseableSceneLoadComplete(AsyncOperationHandle<SceneInstance> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
            handle = obj;
            //Debug.Log(obj.Result.Scene.name + " loaded correctly.");
    }
    private IEnumerator LoadSceneAssetReferenceAsync(AssetReference assetReference)
    {
        assetReference.LoadSceneAsync(LoadSceneMode.Additive);

        while (!assetReference.IsDone)
        {
            yield return new WaitForEndOfFrame();
        }
    }

}


public static class SceneName
{
    public const string Essentials = "ESSENTIALS";
    public const string MainScene = "MainScene";
    public const string CurrentLevelScene = "CurrentLevelScene";
    public const string SaveFileScene = "SaveFileScene";
    public const string MenuScene = "MenuScene";

}