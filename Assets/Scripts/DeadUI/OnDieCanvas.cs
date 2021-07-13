using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDieCanvas : MonoBehaviour
{
    [SerializeField] Animator animator;


    public void OnEnable()
    {
        animator.SetTrigger("Open");
    }

    public void Exit()
    {

        SceneController.Instance.LoadAdresseableScene(SceneName.MenuScene, true);//GameManager.Instance.ExitGame();
        GameManager.Instance.player.playerMovement.enabled = true;
        GameManager.Instance.player.playerStats.CurrentHp = 100;
        GameManager.Instance.statsCanvas.AssignHp();

        GameManager.Instance.player.gameObject.SetActive(false);
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
