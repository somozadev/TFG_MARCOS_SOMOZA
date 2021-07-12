using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DebugScenes : MonoBehaviour
{

    public Text scenesList;
    public Text sceneName;

    void Update()
    {
        scenesList.text = "";
        sceneName.text = SceneManager.GetActiveScene().name;
        foreach (Scene s in SceneManager.GetAllScenes())
            scenesList.text += s.name + "\n";
    }
}
