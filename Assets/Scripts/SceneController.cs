using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

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


    public void LoadScene(string sceneName) { StartCoroutine(LoadAsync(sceneName)); }

    private IEnumerator LoadAsync(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone)
        {
            //pantalla de carga etc etc 
            yield return null;
        }
    }

    public void LoadSceneAssetReference(AssetReference scene) { StartCoroutine(LoadSceneAssetReferenceAsync(scene)); }

    private IEnumerator LoadSceneAssetReferenceAsync(AssetReference assetReference)
    {
        assetReference.LoadSceneAsync(LoadSceneMode.Additive);

        while (!assetReference.IsDone)
        {
            yield return new WaitForEndOfFrame();
        }
    }

}
